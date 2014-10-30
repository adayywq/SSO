using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace Mdl.SSO
{
    public class SsoUser
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string UserLogin(string devCode, string loginType, string loginId, string password)
        {
            /* 100|succeed|sk
             * 200|failed|系统未知错误
             * 201|failed|请选择登录方式
             * 202|failed|请正确填写登录ID
             * 203|failed|请正确填写密码
             * 204|failed|登录方式有误
             * 205|failed|SIOS初始化失败
             * 206|failed|令牌生成失败
             * 207|failed|服务导入失败
             * 210|failed|SSO用户不存在
             * 211|failed|SSO密码错误
             * 220|failed|SNO用户不存在
             * 221|failed|SNO密码错误
             * 222|failed|SNO信息导入失败
             * 230|failed|Account用户不存在
             * 231|failed|Account密码错误
             * 232|failed|Account信息导入失败
             * 240|failed|Email用户不存在
             * 241|failed|Email用户关联错误
             * 250|failed|Mobile用户不存在
             * 251|failed|Mobile用户关联错误
             * 260|failed|Merc用户不存在
             * 261|failed|Merc密码错误
             * 262|failed|Merc信息导入失败
             */
            string result = "";
            string runType = ConfigurationManager.AppSettings["RunType"] == null ? "card" : ConfigurationManager.AppSettings["RunType"].ToString().ToLower();
            switch (runType)
            {
                case "freedom":                    
                    result = (new Mdl.SSO.Freedom.SsoUser()).UserLogin(devCode, loginType, loginId, password);
                    break;
                case "card":
                    result = (new Mdl.SSO.Card.SsoUser()).UserLogin(devCode, loginType, loginId, password);
                    break;
                default:
                    result = (new Mdl.SSO.Card.SsoUser()).UserLogin(devCode, loginType, loginId, password);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public string LoginVerify(string devCode, string sessionKey)
        {
            /* 100|succeed|LoginType#LoginId
             * 200|failed|SessionKey不存在
             * 201|failed|LoginType为空
             * 202|failed|LoginId为空
             */
            string result = "";
            string runType = ConfigurationManager.AppSettings["RunType"] == null ? "card" : ConfigurationManager.AppSettings["RunType"].ToString().ToLower();
            switch (runType)
            {
                case "freedom":
                    result = (new Mdl.SSO.Freedom.SsoUser()).LoginVerify(devCode, sessionKey);
                    break;
                case "card":
                    result = (new Mdl.SSO.Card.SsoUser()).LoginVerify(devCode, sessionKey);
                    break;
                case "ias":
                    result = (new Mdl.SSO.Ias.SsoUser()).LoginVerify(devCode, sessionKey);
                    break;
                default:
                    result = (new Mdl.SSO.Card.SsoUser()).LoginVerify(devCode, sessionKey);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取用于单点登录的用户信息
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUser(string devCode, string sessionKey)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            string runType = ConfigurationManager.AppSettings["RunType"] == null ? "card" : ConfigurationManager.AppSettings["RunType"].ToString().ToLower();
            switch (devCode)
            {
                case "freedom":
                    mdlSsoUser = (new Mdl.SSO.Freedom.SsoUser()).GetUser(devCode, sessionKey);
                    break;
                case "card":
                    mdlSsoUser = (new Mdl.SSO.Card.SsoUser()).GetUser(devCode, sessionKey);
                    break;
                case "ias":
                    mdlSsoUser = (new Mdl.SSO.Ias.SsoUser()).GetUser(devCode, sessionKey);
                    break;
                default:
                    mdlSsoUser = (new Mdl.SSO.Card.SsoUser()).GetUser(devCode, sessionKey);
                    break;
            }
            return mdlSsoUser;
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public string UserLogout(string devCode, string sessionKey)
        {
            /* 100|succeed
             * 200|failed
             */

            //删除数据库的SessionKey
            if (!string.IsNullOrEmpty(sessionKey))
            {
                if (!new Mdl.SSO.TokenInfo().DeleteSk(devCode, sessionKey))
                {
                    return "200|failed";
                }
            }

            //删除cookie
            HttpCookie cookieUser = HttpContext.Current.Request.Cookies["sso_user"];
            if (cookieUser != null)
            {
                cookieUser.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(cookieUser);
            }

            FormsAuthentication.SignOut();
            
            return "100|succeed";
        }

        private string GetConnType(string useScene)
        {
            string connType = "card";
            switch (useScene.Trim().ToLower())
            {
                case "fore":
                    connType = ConfigurationManager.AppSettings["ForeConnType"] == null ? "card" : ConfigurationManager.AppSettings["ForeConnType"].ToString();
                    break;
                case "back":
                    connType = ConfigurationManager.AppSettings["BackConnType"] == null ? "card" : ConfigurationManager.AppSettings["BackConnType"].ToString();
                    break;
                default:
                    connType = "card";
                    break;
            }
            return connType.Trim().ToLower();
        }
    }
}
