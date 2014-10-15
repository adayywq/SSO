using System;
using System.Text;
using System.Text.RegularExpressions;
using Syn.Utility.Extense;
using System.Diagnostics;

namespace Syn.Utility.Function
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="onlyNum">是否仅为数字</param>
        /// <returns></returns>
        public static string CreateAuthStr(int len, bool onlyNum)
        {
            // 验证码生成的取值范围
            string[] verifycodeRange = 
            { 
                "0","1","2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f","g",
                "h","j","k","m","n",
                "p","q",    "r","s","t",
                "u","v","w",    "x","y"
            };

            // 生成验证码所使用的随机数发生器        
            var verifycodeRandom = new Random();

            var checkCode = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                int number = verifycodeRandom.Next(0, !onlyNum ? verifycodeRange.Length : 10);
                checkCode.Append(verifycodeRange[number]);
            }

            return checkCode.ToString();
        }

        /// <summary>
        /// 根据KB数得到对应的大小
        /// </summary>
        /// <param name="size">KB大小</param>
        /// <param name="roundCount">小数点后的位数</param>
        /// <returns>返回对应的大小</returns>
        public static string GetAutoSizeString(double size, int roundCount)
        {
            const double kbCount = 1024;
            const double mbCount = kbCount * 1024;
            const double gbCount = mbCount * 1024;
            const double tbCount = gbCount * 1024;

            if (kbCount > size)
            {
                return Math.Round(size, roundCount) + "B";
            }
            if (mbCount > size)
            {
                return Math.Round(size / kbCount, roundCount) + "KB";
            }
             if (gbCount > size)
            {
                return Math.Round(size / mbCount, roundCount) + "MB";
            }
             if (tbCount > size)
            {
                return Math.Round(size / gbCount, roundCount) + "GB";
            }
            {
                return Math.Round(size / tbCount, roundCount) + "TB";
            }
        }

        /// <summary>
        /// 得到正则编译参数设置
        /// </summary>
        /// <returns>参数设置</returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
            #if NET1
                return RegexOptions.Compiled;
            #else
                return RegexOptions.None;
            #endif
        }


        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }


        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            var strs1 = url.Split(new[] { '/' });
            return strs1[strs1.Length - 1].Split(new[] { '?' })[0];
        }

        /// <summary>
        /// 根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }

        
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIpArray(string ip, string[] iparray)
        {

            string[] userip =ip.ToArray(".");
            foreach (string t in iparray)
            {
                string[] tmpip = t.ToArray(".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 产生一个范围内的随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="f">范围</param>
        /// <returns>随机数</returns>
        public static int GetRandomNum(int minValue, int maxValue, int f)
        {
            var ra = new Random(f);
            int num = ra.Next(minValue, maxValue);
            return num;
        }

        /// <summary>
        /// Json特符字符过滤
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }


        #region Unicode序列化
        /// <summary>
        /// 获取字符串的Unicode编码
        /// </summary>
        /// <param name="orgString">原字符串</param>
        /// <returns></returns>
        public static string UnicodeEncode(string orgString)
        {
            if (orgString == null) throw new ArgumentNullException("orgString");
            string outStr = "";
            if (!string.IsNullOrEmpty(orgString))
            {
                foreach (var myChar in orgString)
                {
                    if (myChar > 255)
                    {
                        //将双字节字符转为10进制整数，然后转为16进制unicode字符
                        outStr += @"\u" + ((int)myChar).ToString("x");
                    }
                    else
                    {
                        outStr += myChar;
                    }
                }
            }
            return outStr;
        }

        /// <summary>
        /// 获取Unicode编码转换为原字符串
        /// </summary>
        /// <param name="unicodeString">原Unicode字符串</param>
        /// <returns></returns>
        public static string UnicodeDecode(string unicodeString)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(unicodeString))
            {
                string[] strlist = unicodeString.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }
            return outStr;
        }
        #endregion


        /// <summary>
        /// 获取web.config中的配置项
        /// </summary>
        /// <param name="param">配置字段名</param>
        /// <param name="defValue">默认值,获取异常.或取到空值将返回该默认值</param>
        /// <returns></returns>
        public static string GetConfigParam(string param, string defValue)
        {
            try
            {
                string result = System.Configuration.ConfigurationManager.AppSettings[param];
                if(string.IsNullOrEmpty(result))
                    result = defValue;
                return result;
            }
            catch (Exception)
            {
                return defValue;
            }
        }

        public sealed class AutoWatch : IDisposable
        {
            private Stopwatch watch;

            public AutoWatch(Stopwatch watch)
            {
                this.watch = watch;
                watch.Start();
            }

            void IDisposable.Dispose()
            {
                watch.Stop();
            }
        }
    }

}
