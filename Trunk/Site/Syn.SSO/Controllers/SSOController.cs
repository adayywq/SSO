using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syn.Utility.Function;
using Mdl.SSO;
using System.Data;
using System.Configuration;
using System.Web.Security;

namespace Syn.SSO.Controllers
{
    public class SSOController : Controller
    {
        //登录
        public ActionResult Login(string devCode,string returnUrl)
        {
            devCode = String.IsNullOrEmpty(devCode) ? "sso" : devCode;
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/Manage/Main";

                DataTable dtData = Developer.GetByDevCode(devCode.ToUpper());
                if ((dtData != null) && !String.IsNullOrEmpty(dtData.Rows[0]["CallbackUrl"].ToString()) && (dtData.Rows[0]["CallbackUrl"].ToString() != "#"))
                {
                    returnUrl = dtData.Rows[0]["SiteUrl"].ToString().Trim('/') + "/" + dtData.Rows[0]["CallbackUrl"].ToString().Trim('/');
                }
            }
            Session["ReturnUrl"] = returnUrl;

            string userInfo = HttpContext.User.Identity.Name;
            //HttpCookie cookieUser = (Request.Cookies["sso_user"] == null) ? new HttpCookie("sso_user") : Request.Cookies["sso_user"];
            //if (!String.IsNullOrEmpty(cookieUser.Values["sk"]))
            if (!String.IsNullOrEmpty(userInfo))
            {
                //string sk = cookieUser.Values["sk"];
                string sk = userInfo.Split('|')[1];
                string loginState = (new Mdl.SSO.SsoUser()).LoginVerify(devCode, sk);
                if (loginState.Contains("100|succeed"))
                {
                    Mdl.Entity.SsoUser mdlSsoUser = new Mdl.SSO.SsoUser().GetUser(devCode, sk);
                    FormsAuthentication.SetAuthCookie(mdlSsoUser.UserId + "|" + mdlSsoUser.Name, false);

                    returnUrl = (returnUrl.IndexOf("?") >= 0) ? (returnUrl + "&sk=" + sk) : (returnUrl + "?sk=" + sk);
                    return new RedirectResult(returnUrl);
                }
            }

            string connType = ConfigurationManager.AppSettings["RunType"] == null ? "freedom" : ConfigurationManager.AppSettings["RunType"].ToString().Trim().ToLower();
            if (connType != "freedom")
            {
                string connUrl = ConfigurationManager.AppSettings["ConnUrl"] == null ? "" : ConfigurationManager.AppSettings["ConnUrl"].ToString().Trim('/');
                string currUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                switch (connType)
                {
                    case "ias":
                        string sysCode = ConfigurationManager.AppSettings["SysCode"] == null ? "" : ConfigurationManager.AppSettings["SysCode"].ToString().Trim();
                        connUrl = connUrl + "/ias/prelogin?sysid=" + sysCode + "&continueurl=" + currUrl + "/SSO/IasTrans";
                        break;
                }                
                Response.Redirect(connUrl, true);
            }

            ViewData["Title"] = ConfigurationManager.AppSettings["Title"].ToString();
            ViewData["Copyright"] = ConfigurationManager.AppSettings["Copyright"].ToString();
            ViewData["SubCopyright"] = ConfigurationManager.AppSettings["SubCopyright"].ToString();
            return View();
        }

        public void ValidateCode()
        {
            Syn.Special.General.CreateCheckCodeImage(Syn.Special.General.GenerateCheckCode());
        }

        public ActionResult LoginCheck()
        {
            string loginType = RequestHelper.GetFormString("loginType");
            string loginId = RequestHelper.GetFormString("loginId");
            string loginPwd = RequestHelper.GetFormString("loginPwd");
            string validateCode = RequestHelper.GetFormString("validateCode");

            if (Request.Cookies["ValidateCode"] != null)
            {
                if (string.Compare(validateCode.ToLower(), Request.Cookies["ValidateCode"].Value.ToLower(), true) == 0)
                {
                    if (loginId.Trim().ToLower() == "admin")
                        loginType = "sso";

                    string r = (new Mdl.SSO.SsoUser()).UserLogin("SSO",loginType,loginId, loginPwd);
                    if (r.Contains("100|succeed"))
                    {
                        string sk = r.Split('|')[2].Trim();
                        Mdl.Entity.SsoUser mdlSsoUser = new Mdl.SSO.SsoUser().GetUser("sso", sk);

                        HttpCookie cookieCode = Response.Cookies["ValidateCode"];
                        if (cookieCode != null)
                        {
                            cookieCode.Value = null;
                            cookieCode.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Set(cookieCode);
                        }
                        
                        //HttpCookie cookieUser = new HttpCookie("sso_user");
                        //cookieUser.Values.Add("sk", sk);
                        //Response.Cookies.Add(cookieUser);

                        FormsAuthentication.SetAuthCookie(mdlSsoUser.UserId + "|" + sk + "|" + mdlSsoUser.Name, false);
                        return (new RedirectResult("/Manage/Main"));
                    }
                    else
                    {                        
                        ViewData["msgInfo"] = "<span style='color: Red; margin-left:58px; font-size: 15px; display: inline-block; width: 160px; border: 1px solid #FEC7C7; padding: 3px; background-color: #FFECEC; text-align:center;'>用户名或密码错误</span>";
                    }
                }
                else
                {
                    ViewData["msgInfo"] = "<span style='color: Red; margin-left:58px; font-size: 15px; display: inline-block; width: 160px; border: 1px solid #FEC7C7; padding: 3px; background-color: #FFECEC; text-align:center;'>验证码错误</span>";
                }
            }
            else
            {
                ViewData["msgInfo"] = "<span style='color: Red; margin-left:58px; font-size: 15px; display: inline-block; width: 160px; border: 1px solid #FEC7C7; padding: 3px; background-color: #FFECEC; text-align:center;'>验证码错误</span>";
            }

            ViewData["Title"] = ConfigurationManager.AppSettings["Title"].ToString();
            ViewData["Copyright"] = ConfigurationManager.AppSettings["Copyright"].ToString();
            ViewData["SubCopyright"] = ConfigurationManager.AppSettings["SubCopyright"].ToString();
            return View("/Views/SSO/Login.cshtml");
        }

        //退出登录
        public ActionResult Logout(string devCode, string returnUrl, string sessionKey)
        {
            devCode = String.IsNullOrEmpty(devCode) ? "sso" : devCode;

            string cookieName = "sso_user";
            System.Web.HttpCookie cookieUser = null;
            if (string.IsNullOrEmpty(sessionKey))
            {
                cookieUser = Request.Cookies[cookieName];
                if ((cookieUser != null) && (!String.IsNullOrEmpty(cookieUser.Values["sk"])))
                {
                    sessionKey = cookieUser.Values["sk"];
                }
            }

            (new Mdl.SSO.SsoUser()).UserLogout(devCode, sessionKey);

            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/SSO/Login";

                DataTable dtData = Developer.GetByDevCode(devCode.ToUpper());
                if ((dtData != null) && !String.IsNullOrEmpty(dtData.Rows[0]["LogoutUrl"].ToString()) && (dtData.Rows[0]["LogoutUrl"].ToString() != "#"))
                {
                    returnUrl = dtData.Rows[0]["SiteUrl"].ToString().Trim('/') + "/" + dtData.Rows[0]["LogoutUrl"].ToString().Trim('/');
                }
            }

            ViewBag.connUrl = returnUrl;
            return View();
        }
    }
}
