using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;

namespace Syn.SSO.Controllers
{
    public class ControlController : Controller
    {
        //
        // GET: /Control/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult footer()
        {
            ViewData["Title"] = ConfigurationManager.AppSettings["Title"].ToString();
            ViewData["Copyright"] = ConfigurationManager.AppSettings["Copyright"].ToString();
            ViewData["SubCopyright"] = ConfigurationManager.AppSettings["SubCopyright"].ToString();
            return View();
        }
        public ActionResult header()
        {
            //Dal.Basic.Login dallog = new Dal.Basic.Login();
            //Mdl.Electric.SsoUser mdl = dallog.GetUser();
            //
            //Syn.Special.Pagination fenyepage = new Syn.Special.Pagination();
            //int count = fenyepage.fenyecount("Message  M inner join Usermessage U on M.MessID=U.MessID ", "m.messid", " M.State=1 and U.State =1 and Recipient='" + mdl.UserId + "'");//表名  字段  
            ViewData["Headname"] = System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2];
            ViewData["titname"] = ConfigurationManager.AppSettings["Title"].ToString();
            ViewData["SubTitle"] = ConfigurationManager.AppSettings["SubTitle"].ToString();
            //
            //string logintype = ConfigurationManager.AppSettings["RunType"].ToString().Trim().ToLower();
            //
            //if (logintype == "freedom" || logintype == "card")
            //{
            //    if (count > 0)
            //    {
            //        ViewData["headmessage"] = "我的消息<span style='color:red;'>(" + count + ")</span>";
            //    }
            //    else
            //    {
            //        ViewData["headmessage"] = "我的消息";
            //    }
            //}
            //else
            //{
            //    ViewData["headmessage"] = "";
            //}
            //
            //
            //if (logintype == "freedom")
            //{
            //    ViewData["pwdurl"] = "<a rel=\"show-u-list\" id=\"ucenter\" href=\"/Account/SetPwd/\" target=\"frame_content\" class=''><img alt=\"主页\" class=\"icon ico-ucenter-user\" src=\"/themes/default/images/blank.gif\"/>密码修改</a>";
            //}
            //else
            //{
            //    ViewData["pwdurl"] = "";
            //}
            return View();
        }
        public ActionResult menu()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("<h3 class=\"togglezj\" >服务管理</h3><ul  class=\"togglezj-ex\">");
            strb.Append("<li ><a target=\"frame_content\" href=\"/Manage/DevAccreditList/\">开发授权</a></li>");            
            ViewData["menu"] = strb.ToString();
            return View();
        }
    }
}
