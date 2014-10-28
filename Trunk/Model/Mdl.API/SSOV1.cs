using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syn.Utility.Function;

namespace Mdl.API
{
    public class SSOV1
    {
        //1号版本
        public string SsoApiV1(string cmd)
        {
            string rlt = "";
            switch (cmd)
            {
                case "login":
                    string loginType = String.IsNullOrEmpty(RequestHelper.GetString("lt")) ? "1" : RequestHelper.GetString("lt");
                    string loginId = RequestHelper.GetString("name");
                    string password = RequestHelper.GetString("pwd");
                    rlt = UserLogin(loginType, loginId, password);
                    break;
                case "verify":  //验证SessionKey是否有效，即判断用户是否登录
                    string sessionKey2 = RequestHelper.GetString("sk");
                    string clientIp2 = RequestHelper.GetString("ip");
                    rlt = LoginVerify(sessionKey2,clientIp2);
                    break;
                case "getuser": //根据SessionKey获取用户信息
                    string sessionKey3 = RequestHelper.GetString("sk");
                    string clientIp3 = RequestHelper.GetString("ip");
                    rlt = GetUser(sessionKey3, clientIp3);
                    break;
                case "logout":  //退出登录
                    rlt = Logout(RequestHelper.GetString("sk"));
                    break;
            }
            return rlt;
        }

        /// <summary>
        /// 用户登录
        /// "100|SessionKey"：登录成功
        /// "400"：登录失败
        /// </summary>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string UserLogin(string loginType, string loginId, string password)
        {
            string result = new Mdl.SSO.SsoUser().UserLogin("GLPT", loginType, loginId, password);
            if (result.Contains("100|succeed|"))
            {
                return result.Split('|')[0] + "|" + result.Split('|')[2];
            }
            return "400";
        }

        /// <summary>
        /// 验证用户是否登录
        /// "1"：在线
        /// "0"：不在线
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public string LoginVerify(string sk, string clientIP)
        {
            string result = new Mdl.SSO.SsoUser().LoginVerify("GLPT", sk);
            if (result.Contains("100|succeed|"))
            {
                return "1";
            }
            return "0";
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public string GetUser(string sk, string clientIP)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.SSO.SsoUser().GetUser("GLPT", sk);
            if (mdlSsoUser == null)
            {
                return "";
            }
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sbXml.Append("<UserInfo>");
            sbXml.Append("<UserId>" + mdlSsoUser.UserId + "</UserId>");
            sbXml.Append("<Sno>" + mdlSsoUser.Sno + "</Sno>");
            sbXml.Append("<AccountId>" + mdlSsoUser.AccountId + "</AccountId>");
            sbXml.Append("<Name>" + mdlSsoUser.Name + "</Name>");
            sbXml.Append("<Email>" + mdlSsoUser.Email + "</Email>");
            sbXml.Append("<Department>" + mdlSsoUser.Department + "</Department>");
            sbXml.Append("<Job>" + mdlSsoUser.Job + "</Job>");
            sbXml.Append("<Identity>" + mdlSsoUser.Identity + "</Identity>");
            sbXml.Append("</UserInfo>");
            return sbXml.ToString();
        }

        /// <summary>
        /// 用户登出
        /// "100"：登出成功
        /// "500"：登出失败
        /// </summary>
        /// <param name="sk"></param>
        /// <returns></returns>
        public string Logout(string sk)
        {
            string result = new Mdl.SSO.SsoUser().UserLogout("GLPT", sk);
            if (result.Contains("100|succeed"))
            {
                return "100";
            }
            return "500";
        }
    }
}
