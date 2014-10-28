using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syn.Special;
using System.Web;
using System.Data;
using Syn.Utility.Function;

namespace Mdl.API
{
    public class SSOV2
    {
        string rlt = "";
        //2号版本
        public string SsoApiV2(string cmd, string dt)
        {
            string sk = RequestHelper.GetString("sk");
            switch (cmd)
            {
                case "verify":
                    rlt = ApiHelper.CheckApi("sk", "sign");
                    if (rlt == "succeed")
                    {
                        rlt = Verify(sk, dt);
                    }
                    break;
                case "getuser":
                    rlt = ApiHelper.CheckApi("sk", "sign");
                    if (rlt == "succeed")
                    {
                        rlt = GetUser(sk, dt);
                    }
                    break;
                case "logout":
                    rlt = ApiHelper.CheckApi("sk", "sign");
                    if (rlt == "succeed")
                    {
                        rlt = Logout(sk, dt);
                    }
                    break;
                default:
                    rlt = ApiHelper.ReturnApi(dt, "sp0052", "", "", "");
                    break;
            }
            return rlt;
        }

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Verify(string sk,string dt)
        {
            try
            {
                StringBuilder sbData = new StringBuilder();
                string ret = new Mdl.SSO.SsoUser().LoginVerify("FWPT", sk);                
                switch (dt)
                {
                    case "xml":
                        sbData.Append("<islogin>" + ((ret.Contains("100|succeed|")) ? "true" : "false") + "</islogin>");
                        break;
                    default:
                        sbData.Append("{\"islogin\":\"" + ((ret.Contains("100|succeed|")) ? "true" : "false") + "\"}");
                        break;
                }
                return ApiHelper.ReturnApi(dt, "SSO0000", "", "", sbData.ToString());
            }
            catch (Exception ex)
            {
                return ApiHelper.ReturnApi(dt, "SSO0050", "", "", "");
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetUser(string sk, string dt)
        {
            try
            {
                StringBuilder sbData = new StringBuilder();
                Mdl.Entity.SsoUser mdlSsoUser = new Mdl.SSO.SsoUser().GetUser("FWPT", sk);
                if (mdlSsoUser == null)
                {
                    return ApiHelper.ReturnApi(dt, "SSO0100", "", "", "");
                }
                switch (dt)
                {
                    case "xml":
                        sbData.Append("<UserId>" + mdlSsoUser.UserId + "</UserId>");
                        sbData.Append("<Sno>" + mdlSsoUser.Sno + "</Sno>");
                        sbData.Append("<AccId>" + mdlSsoUser.AccountId + "</AccId>");
                        sbData.Append("<Name>" + mdlSsoUser.Name + "</Name>");
                        sbData.Append("<Mobile>" + mdlSsoUser.Mobile + "</Mobile>");
                        sbData.Append("<Email>" + mdlSsoUser.Email + "</Email>");
                        sbData.Append("<Dept>" + mdlSsoUser.Department + "</Dept>");
                        sbData.Append("<Job>" + mdlSsoUser.Job + "</Job>");
                        sbData.Append("<Ident>" + mdlSsoUser.Identity + "</Ident>");
                        break;
                    default:
                        sbData.Append("{");
                        sbData.Append("\"userid\":\"" + mdlSsoUser.UserId + "\",");
                        sbData.Append("\"sno\":\"" + mdlSsoUser.Sno + "\",");
                        sbData.Append("\"accid\":\"" + mdlSsoUser.AccountId + "\",");
                        sbData.Append("\"name\":\"" + mdlSsoUser.Name + "\",");
                        sbData.Append("\"mobile\":\"" + mdlSsoUser.Mobile + "\",");
                        sbData.Append("\"email\":\"" + mdlSsoUser.Email + "\",");
                        sbData.Append("\"dept\":\"" + mdlSsoUser.Department + "\",");
                        sbData.Append("\"job\":\"" + mdlSsoUser.Job + "\",");
                        sbData.Append("\"ident\":\"" + mdlSsoUser.Identity + "\"");
                        sbData.Append("}");
                        break;
                }
                return ApiHelper.ReturnApi(dt, "SSO0000", "", "", sbData.ToString());
            }
            catch (Exception ex)
            {
                return ApiHelper.ReturnApi(dt, "SSO0050", "", "", "");
            }
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Logout(string sk, string dt)
        {
            try
            {
                StringBuilder sbData = new StringBuilder();
                string ret = new Mdl.SSO.SsoUser().UserLogout("FWPT", sk);
                switch (dt)
                {
                    case "xml":
                        sbData.Append("<issucceed>" + ((ret.Contains("100|succeed")) ? "true" : "false") + "</issucceed>");
                        break;
                    default:
                        sbData.Append("{\"issucceed\":\"" + ((ret.Contains("100|succeed")) ? "true" : "false") + "\"}");
                        break;
                }
                return ApiHelper.ReturnApi(dt, "SSO0000", "", "", sbData.ToString());
            }
            catch (Exception ex)
            {
                return ApiHelper.ReturnApi(dt, "SSO0050", "", "", "");
            }
        }
    }
}
