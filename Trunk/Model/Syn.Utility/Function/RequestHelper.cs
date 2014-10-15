using System;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Syn.Utility.Extense;
using Syn.Utility.Security;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// Request操作类
    /// </summary>
    public class RequestHelper
    {
        #region 判断当前页面是否接收到了Post请求
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        #endregion

        #region 判断当前页面是否接收到了Get请求
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion

        #region 返回指定的服务器变量信息
        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            return HttpContext.Current.Request.ServerVariables[strName] ?? string.Empty;
        }

        #endregion

        #region 返回上一个页面的地址
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch (Exception)
            {
                retVal = null;
            }

            return retVal ?? string.Empty;
        }
        #endregion

        #region 得到当前完整主机头
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns>返回主机名和端口号(如果有)</returns>
        public static string GetCurrentFullHost()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                return !request.Url.IsDefaultPort ? string.Format("{0}:{1}", request.Url.Host, request.Url.Port) : request.Url.Host;
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 得到主机头
        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns>返回当前主机的名称,但不包括端口号</returns>
        public static string GetHost()
        {
            try
            {
                return HttpContext.Current.Request.Url.Host;
            }
            catch (Exception)
            {
                return "";
            }

        }
        #endregion

        #region 获得当前完整Url地址
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            try
            {
                return HttpContext.Current.Request.Url.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion


        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] browserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            return browserName.Any(t => curBrowser.IndexOf(t) >= 0);
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] searchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            return searchEngine.Any(t => tmpReferrer.IndexOf(t) >= 0);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, false);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }

            string reslut = HttpContext.Current.Request.QueryString[strName];
            if (sqlSafeCheck)
            {
                reslut = HtmlHelper.CheckStr(reslut);
            }

            return reslut;
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return GetFormString(strName, false);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            string reslut = HttpContext.Current.Request.Form[strName];
            if (sqlSafeCheck)
            {
                reslut = HtmlHelper.CheckStr(reslut);
            }

            return reslut;
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            string result = GetQueryString(strName);
            if ("".Equals(result))
                result = GetFormString(strName);
            return result;
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return HttpContext.Current.Request.QueryString[strName].ToInt(defValue);
        }

        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return HttpContext.Current.Request.Form[strName].ToInt(defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            int result = GetQueryInt(strName, defValue);
            if (result == defValue)
                result = GetFormInt(strName, defValue);
            return result;
        }


        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return HttpContext.Current.Request.QueryString[strName].ToFloat(defValue);
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return HttpContext.Current.Request.Form[strName].ToFloat(defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            float result = GetQueryFloat(strName, defValue);
            if (result == defValue)
                result = GetFormFloat(strName, defValue);
            return result;
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIp()
        {
            string result;
            try
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_CIP"];

                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }

                if (string.IsNullOrEmpty(result) || !Valid.IsIp(result))
                {
                    result = "127.0.0.1";
                }
            }
            catch (Exception)
            {
                result = "";
            }
            return result;

        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool IsCrossSitePost()
        {
            // 如果不是提交则为true
            return !IsPost() || IsCrossSitePost(GetUrlReferrer(), GetHost());
        }

        /// <summary>
        /// 判断是否是跨站提交
        /// </summary>
        /// <param name="urlReferrer">上个页面地址</param>
        /// <param name="host">论坛url</param>
        /// <returns>bool</returns>
        public static bool IsCrossSitePost(string urlReferrer, string host)
        {
            if (urlReferrer.Length < 7)
            {
                return true;
            }
            var u = new Uri(urlReferrer);
            return u.Host != host;
        }

        /// <summary>
        /// 获取虚拟目录对应的物理路径 
        /// 需预先配置站点的IIS信息，格式（127.0.0.1:80:autohome.com.cn）
        /// 需预先配置站点IIS的虚拟目录名称
        /// </summary>
        /// <returns></returns>
        public static string GetVirtualDirectory()
        {
            //获取网站IIS信息
            string portNumber = System.Configuration.ConfigurationManager.AppSettings["IISSiteInfo"];
            if (String.IsNullOrEmpty(portNumber))
            {
                return "";
            }
            string name = System.Configuration.ConfigurationManager.AppSettings["IISVirtualDirectory"];
            if (String.IsNullOrEmpty(name))
            {
                return "";
            }
            //获取IIS虚拟目录名称
            // 获取网站的标识符，默认为1
            string identifier = null;
            var root = new DirectoryEntry("IIS://LOCALHOST/W3SVC");
            foreach (DirectoryEntry e in from DirectoryEntry e in root.Children where e.SchemaClassName == "IIsWebServer" select e)
            {
                if (e.Properties["ServerBindings"].Cast<object>().Contains(portNumber))
                {
                    identifier = e.Name;
                }

                if (identifier != null)
                {
                    break;
                }
            }

            if (identifier == null)
            {
                identifier = "1";
            }

            var de = new DirectoryEntry("IIS://LOCALHOST/W3SVC/" + identifier + "/ROOT/" + name);
            var path = (string)de.Properties["Path"].Value;
            return path;
        }

        /// 得到网站的物理路径
        /// <param name="rootEntry">网站节点</param>
        /// <returns></returns>
        public static string GetWebsitePhysicalPath(DirectoryEntry rootEntry)
        {
            string physicalPath = "";
            foreach (DirectoryEntry childEntry in
                rootEntry.Children.Cast<DirectoryEntry>().Where(childEntry => (childEntry.SchemaClassName == "IIsWebVirtualDir") && (childEntry.Name.ToLower() == "root")))
            {
                physicalPath = childEntry.Properties["Path"].Value != null ? childEntry.Properties["Path"].Value.ToString() : "";
            }
            return physicalPath;
        }

        /// <summary>
        /// 获得指定Url参数的Int64类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的Int64类型值</returns>
        public static long GetQueryInt64(string strName, long defValue)
        {
            return HttpContext.Current.Request.QueryString[strName].ToInt64(defValue);
        }

        /// <summary>
        /// 获得指定表单参数的Int64类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的Int64类型值</returns>
        public static long GetFormInt64(string strName, long defValue)
        {
            return HttpContext.Current.Request.Form[strName].ToInt64(defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的Int64类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的Int64类型值</returns>
        public static long GetInt64(string strName, long defValue)
        {
            long result = GetQueryInt64(strName, defValue);
            if (result == defValue)
            {
                result = GetFormInt64(strName, defValue);
            }
            return result;
        }

        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            return GetSourceTextByUrl(url, Encoding.UTF8);
        }

        /// <summary>
        /// 根据URL获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <param name="encodeing">编码</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url, Encoding encodeing)
        {
            WebRequest request = WebRequest.Create(url);
            request.Timeout = 20000;//20秒超时
            WebResponse response = request.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, encodeing);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {   
            string s = HttpUtility.UrlEncode(str);
            s = s.Replace("+", "%20");
            return s;
        }

        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
    }
}
