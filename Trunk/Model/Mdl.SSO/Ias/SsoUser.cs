using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Data;
using Syn.Utility.Function;

namespace Mdl.SSO.Ias
{
    public class SsoUser
    {
        /// <summary>
        /// 验证用户登录状态
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public string LoginVerify(string devCode, string sessionKey)
        {
            //string ssoUrl = ConfigurationManager.AppSettings["ConnUrl"].Trim('/');
            //string postUrl = ssoUrl + "/ias/fetchStatus";
            //string postData = "sysid=" + (ConfigurationManager.AppSettings["SysCode"] == null ? "" : ConfigurationManager.AppSettings["SysCode"]) + "&m=call";
            //string result = Syn.Special.General.Post(postUrl, postData, "utf-8");
            //if (result.Contains("1"))
            //{
            //    return "100|succeed|";
            //}
            //else
            //{
            //    return "200|failed|SessionKey不存在";
            //}

            string loginType = "";
            string loginId = "";
            if ((new TokenInfo()).IsExistsSk(devCode, sessionKey, ref loginType, ref loginId))
            {
                if (String.IsNullOrEmpty(loginType))
                {
                    return "201|failed|LoginType为空";
                }
                else if (String.IsNullOrEmpty(loginId))
                {
                    return "202|failed|LoginId为空";
                }
                else
                {
                    return "100|succeed|" + loginType + "#" + loginId;
                }
            }
            else
            {
                return "200|failed|SessionKey不存在";
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUser(string devCode, string sessionKey)
        {
            string cacheName = "ias_userinfo_" + sessionKey.ToLower();
            if (CacheHelper.IsExist(cacheName))
            {
                Mdl.Entity.SsoUser mdlSsoUser_Cache = CacheHelper.Get<Mdl.Entity.SsoUser>(cacheName);
                return mdlSsoUser_Cache;
            }

            string ssoUrl = ConfigurationManager.AppSettings["ConnUrl"].Trim('/');
            string postUrl = ssoUrl + "/ias/post";            
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.Append("<?xml version='1.0' encoding='utf-8'?>");
            sbPostData.Append("<syscontent>");
            sbPostData.Append("<id>" + Guid.NewGuid().ToString() + "</id>");
            sbPostData.Append("<type>post</type>");
            sbPostData.Append("<ssoticketid>" + sessionKey + "</ssoticketid>");
            sbPostData.Append("<sysid>" + (ConfigurationManager.AppSettings["SysCode"] == null ? "" : ConfigurationManager.AppSettings["SysCode"]) + "</sysid>");
            sbPostData.Append("</syscontent>");
            string postData = "msg=" + sbPostData.ToString();
            string result = Syn.Special.General.Post(postUrl, postData, "utf-8");
            if ((String.IsNullOrEmpty(result)) || (result == "0") || result.ToLower().Contains("userbase is null"))
            {
                return null;
            }
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(result);
            string tempSno = (xd.GetElementsByTagName("sno").Count > 0) ? xd.GetElementsByTagName("sno").Item(0).InnerText : "";
            string tempAccId = (xd.GetElementsByTagName("cardno").Count > 0) ? xd.GetElementsByTagName("cardno").Item(0).InnerText : "";
            string tempName = (xd.GetElementsByTagName("name").Count > 0) ? xd.GetElementsByTagName("name").Item(0).InnerText : "";
            string tempEmail = (xd.GetElementsByTagName("email").Count > 0) ? xd.GetElementsByTagName("email").Item(0).InnerText : "";
            string tempMobile = (xd.GetElementsByTagName("mobile").Count > 0) ? xd.GetElementsByTagName("mobile").Item(0).InnerText : "";
            string tempDeptCode = (xd.GetElementsByTagName("deptcode").Count > 0) ? xd.GetElementsByTagName("deptcode").Item(0).InnerText : "";
            string tempDept = (xd.GetElementsByTagName("deptcodename").Count > 0) ? xd.GetElementsByTagName("deptcodename").Item(0).InnerText : "";
            string tempClassCode = (xd.GetElementsByTagName("uclass").Count > 0) ? xd.GetElementsByTagName("uclass").Item(0).InnerText : "";
            //string tempClass = (xd.GetElementsByTagName("uclassname").Count > 0) ? xd.GetElementsByTagName("uclassname").Item(0).InnerText : "";
            string tempIdentCode = (xd.GetElementsByTagName("identity").Count > 0) ? xd.GetElementsByTagName("identity").Item(0).InnerText : "";
            string tempIdent = (xd.GetElementsByTagName("identityname").Count > 0) ? xd.GetElementsByTagName("identityname").Item(0).InnerText : "";
            //string tempStateCode = (xd.GetElementsByTagName("zt").Count > 0) ? xd.GetElementsByTagName("zt").Item(0).InnerText : "";
            //string tempState = (xd.GetElementsByTagName("ztname").Count > 0) ? xd.GetElementsByTagName("ztname").Item(0).InnerText : "";

            mdlSsoUser.UserId = "0";
            mdlSsoUser.Sno = tempSno;
            mdlSsoUser.AccountId = tempAccId;
            mdlSsoUser.Name = tempName;
            mdlSsoUser.Email = tempEmail;
            mdlSsoUser.Mobile = tempMobile;
            mdlSsoUser.Identity = tempClassCode + "#" + tempIdentCode + "#" + tempIdent;
            mdlSsoUser.Department = tempDeptCode + "#" + tempDept;
            mdlSsoUser.Job = "#";

            if (!String.IsNullOrEmpty(mdlSsoUser.Sno.Trim()))
            {
                DataTable dtSno = (new Mdl.SSO.SysUser()).GetBySno(mdlSsoUser.Sno);
                if (dtSno != null)
                {
                    mdlSsoUser.UserId = dtSno.Rows[0]["USERID"].ToString();
                }
            }
            else if (!String.IsNullOrEmpty(mdlSsoUser.AccountId.Trim()) && !mdlSsoUser.AccountId.Contains('|'))
            {
                DataTable dtAccount = (new Mdl.SSO.SysUser()).GetByAccId(mdlSsoUser.AccountId);
                if (dtAccount != null)
                {
                    mdlSsoUser.UserId = dtAccount.Rows[0]["USERID"].ToString();
                }
            }
            CacheHelper.Add<Mdl.Entity.SsoUser>(cacheName, mdlSsoUser, 3);
            return mdlSsoUser;
        }        
    }
}
