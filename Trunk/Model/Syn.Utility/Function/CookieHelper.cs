using System;
using System.Web;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// Cookie操作
    /// </summary>
    public static class CookieHelper
    {
        //public static string appPrefix = "Auto_";//Cookie前缀
        public static string AppPrefix = System.Configuration.ConfigurationManager.AppSettings["AppPrefix"]??"";//Cookie前缀

        #region 设置Cookie值
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        public static void SetCookies(string keystr, string values, DateTime timeout)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[keystr];
                if (cookie != null)
                {
                    cookie.Value = values;
                    cookie.Expires = timeout;
                }
                //response.Cookies[keystr].Domain = "/";
            }
        }
        #endregion

        #region 获取Cookie值
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[AppPrefix + name];
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
                return cookie != null ? cookie.Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 设置Cookie值
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(AppPrefix + name);
        }
        #endregion

        #region 保存Cookie值
        /// <summary>
        ///  保存Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        public static void Save(HttpCookie cookie)
        {
            const string domain = "autohome.com.cn";
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (domain != host)
            {
                cookie.Domain = domain;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 移除Cookie值
        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new DateTime(1980, 1, 1);
                Save(cookie);
            }
        }
        #endregion

        #region 移除Cookie值
        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            Remove(Get(name));
        }
        #endregion
    }
}
