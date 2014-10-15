using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Xml.Linq;
using Syn.Utility;
using System.Xml.XPath;
using System.Web;
using Syn.Utility.Function;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Syn.Special
{
    public static class General
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="isSucceed"></param>
        /// <returns></returns>
        public static string ReturnValue(bool isSucceed)
        {
            return ReturnValue(isSucceed, "");
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="isSucceed"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ReturnValue(bool isSucceed, string msg)
        {
            return "{'result':" + isSucceed.ToString().ToLower() + ",'msg':'" + msg.ToString() + "'}";
        }

        /// <summary>
        /// SIOS初始化
        /// </summary>
        /// <returns></returns>
        public static bool SiosInit()
        {
            string SiosIP = ConfigurationManager.AppSettings["SiosIP"] == null ? "127.0.0.1" : ConfigurationManager.AppSettings["SiosIP"].ToString();
            short SiosPort = ConfigurationManager.AppSettings["SiosPort"] == null ? Convert.ToInt16(8500) : Convert.ToInt16(ConfigurationManager.AppSettings["SiosPort"].ToString());
            ushort SiosSysCode = ConfigurationManager.AppSettings["SiosSysCode"] == null ? Convert.ToUInt16(0) : Convert.ToUInt16(ConfigurationManager.AppSettings["SiosSysCode"].ToString());
            ushort SiosTerminalNo = ConfigurationManager.AppSettings["SiosTerminalNo"] == null ? Convert.ToUInt16(1) : Convert.ToUInt16(ConfigurationManager.AppSettings["SiosTerminalNo"].ToString());
            bool ProxyOffline;
            uint MaxJnl;
            bool ret = Syn.GDSC2.AIOAPI.TA_Init(SiosIP, SiosPort, SiosSysCode, SiosTerminalNo, out ProxyOffline, out MaxJnl);
            return ret;
        }

        /// <summary>
        /// 服务平台系统类型编号检查
        /// </summary>
        /// <returns></returns>
        public static string CheckSpNode()
        {
            string ret = "succeed";
            if (SiosInit())
            {
                ushort nodeId;
                Syn.GDSC2.AIOAPI.TA_GetNodeByType(76, out nodeId);
                if (nodeId == 0)
                {
                    ret = "未检测到服务平台节点,请确认是否已购买！";
                }
            }
            else
            {
                ret = "初始化第三方代理服务器失败,请重新配置！";
            }
            return ret;
        }

        /// <summary>
        /// 获取设备最大数量
        /// </summary>
        /// <returns></returns>
        public static int GetDeviceCount()
        {
            return 188;

            //int rlt = 0;
            //Syn.GDSC2.ENCARD_CONFIG_ALL config;
            //int result = Syn.GDSC2.AIOAPI.EC_GetEnCardCfg(out config);
            //if (int.Parse(config.NodeID.ToString()) == 0)
            //{
            //    //没有找到加密卡
            //}
            //else
            //{
            //    if (int.Parse(config.SysType.ToString()) != 40)
            //    {
            //        //系统类型没有注册加密卡
            //    }
            //    rlt = int.Parse(config.UserCapacity.Tostring());
            //}
            //return rlt;
        }


        /// <summary>
        /// 提交数据到指定HTTP接口
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postData"></param>
        /// <param name="chars_set"></param>
        /// <returns></returns>
        public static string Post(string postUrl, string postData, string chars_set)
        {
            Encoding encoding = Encoding.GetEncoding(chars_set);
            Uri uri = new Uri(postUrl);
            if ("https" == uri.Scheme)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);//验证服务器证书回调自动验证
            }
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(postUrl);
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.AllowAutoRedirect = true;
            //Request.KeepAlive = false;
            //Request.ProtocolVersion = Request.ProtocolVersion;
            //Request.CookieContainer = new CookieContainer();
            //Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            byte[] postdata = encoding.GetBytes(postData);
            using (Stream newStream = Request.GetRequestStream())
            {
                newStream.Write(postdata, 0, postdata.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            {

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, encoding, true))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        /// <summary>
        /// 使用Get方式 ，获取指定http接口的数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static string GetHttpSource(string url, string codeType)
        {
            Encoding wCode; //判断网页编码
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(@url);
            wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215;)";
            Match a = Regex.Match(url, @"(http://).[^/]*[?=/]", RegexOptions.IgnoreCase);
            string url1 = a.Groups[0].Value.ToString();
            wReq.Referer = url1;
            wReq.Method = "GET";
            wReq.Timeout = 30000;   //设置超时时间为30秒
            CookieContainer cc = new CookieContainer();
            wReq.CookieContainer = cc;
            HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
            Stream respStream = wResp.GetResponseStream();
            wCode = Encoding.UTF8;
            StreamReader reader = new StreamReader(respStream, wCode);
            string strWebData = reader.ReadToEnd();
            reader.Close();
            return strWebData;
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;   // 总是接受
        }

        public static string ReplaceXMLString(this string obj)
        {
            return obj.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        public static string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ValidateCode", checkCode));
            return checkCode;
        }

        public static void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
