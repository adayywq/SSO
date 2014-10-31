using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text;
using System.Data;
using Syn.Utility.Function;
using Syn.Utility.Extense;
using System.Security.Cryptography;
using sg = System.Guid;

namespace Syn.SSO.Controllers
{
    public class ManageController : Controller
    {
        Mdl.SSO.DevAccredit appDal = new Mdl.SSO.DevAccredit();
        
        //
        // GET: /Manage/

        public ActionResult Index()
        {
            ViewData["Title"] = ConfigurationManager.AppSettings["Title"].ToString();
            ViewData["Date"] = DateTime.Now.ToLongDateString();
            ViewData["Copyright"] = "版权所有&copy; " + ConfigurationManager.AppSettings["Copyright"].ToString() + "<br/>Copyright &copy;" + ConfigurationManager.AppSettings["SubCopyright"].ToString();
            ViewData["webcome"] = "欢迎使用" + ConfigurationManager.AppSettings["Title"].ToString();
            return View();
        }
        public ActionResult Main()
        {
            ViewData["Title"] = ConfigurationManager.AppSettings["Title"];
            return View();
        }
        public string GetMenu()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("<h3 class=\"togglezj\" >服务管理</h3><ul id=\"me_syglul\" class=\"togglezj-ex\">");
            strb.Append("<li><h4><a target=\"frame_content\" href=\"/Backend/Service/DevAccreditList.aspx\"><img src=\"/Backend/themes/default/images/blank.gif\" class=\"icon ico-arrow-2\" alt='' />开发授权</a></h4></li>");
            return strb.ToString();
        }
        /// <summary>
        /// 获得空模板
        /// </summary>
        /// <returns>json格式字符串</returns>
        public string GetNullTemplate()
        {
            return "{\"page\":0,\"total\":0}";
        }
        public ActionResult DevAccreditList()
        {
            return View();
        }
        public ActionResult ADevAccredit(string type)
        {
            ViewBag.Title = type == "add" ? "添加" : "编辑";
            return View();
        }
        /// <summary>
        /// 获取开发者列表
        /// </summary>
        /// <param name="curPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public string GetDevList(int page, int rp)
        {
            int totalCount = 0;

            DataTable dt = appDal.SelectAppList(ref page, rp, out totalCount);

            if (totalCount == 0) return GetNullTemplate();

            DataTable dtAppList = new DataTable();
            dtAppList.Columns.Add("DEVID", Type.GetType("System.Int32"));
            dtAppList.Columns.Add("ACCCODE", Type.GetType("System.String"));
            dtAppList.Columns.Add("DEVNAME", Type.GetType("System.String"));
            dtAppList.Columns.Add("DEVCODE", Type.GetType("System.String"));
            dtAppList.Columns.Add("LINKMAN", Type.GetType("System.String"));
            dtAppList.Columns.Add("MOBILE", Type.GetType("System.String"));
            dtAppList.Columns.Add("STATE", Type.GetType("System.String"));
            dtAppList.Columns.Add("CALLBACKURL", Type.GetType("System.String"));
            dtAppList.Columns.Add("LOGOUTURL", Type.GetType("System.String"));
            dtAppList.Columns.Add("EDITROW", Type.GetType("System.String"));
            dtAppList.Columns.Add("DELROW", Type.GetType("System.String"));
            dtAppList.Columns.Add("EMAIL", Type.GetType("System.String"));

            //权限
            //sw.HttpCookie cookie = sw.HttpContext.Current.Request.Cookies["back_purview"];
            // string co = sw.HttpUtility.UrlDecode(cookie.Value);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow newRow;
                newRow = dtAppList.NewRow();
                newRow["DEVID"] = dt.Rows[i]["DEVID"];
                newRow["ACCCODE"] = dt.Rows[i]["ACCCODE"];
                newRow["DEVNAME"] = RequestHelper.UrlEncode(dt.Rows[i]["DEVNAME"].ToString());
                newRow["DEVCODE"] = RequestHelper.UrlEncode(dt.Rows[i]["DEVCODE"].ToString());
                newRow["LINKMAN"] = RequestHelper.UrlEncode(dt.Rows[i]["LINKMAN"].ToString());
                newRow["EMAIL"] = RequestHelper.UrlEncode(dt.Rows[i]["EMAIL"].ToString());
                newRow["MOBILE"] = dt.Rows[i]["MOBILE"];
                newRow["CALLBACKURL"] = RequestHelper.UrlEncode(dt.Rows[i]["CALLBACKURL"].ToString());
                newRow["LOGOUTURL"] = RequestHelper.UrlEncode(dt.Rows[i]["LOGOUTURL"].ToString());
                if (Convert.ToInt32(dt.Rows[i]["STATE"]) == 1)
                {
                    newRow["STATE"] = "<select class='textinput' style='margin:0px;padding:0px;' onchange='ChangeState(this," + dt.Rows[i]["DEVID"].ToString() + ")'><option value='0'>停用</option><option value='1' selected>启用</option></select>";
                }
                else
                {
                    newRow["STATE"] = "<select class='textinput' style='margin:0px;padding:0px;' onchange='ChangeState(this," + dt.Rows[i]["DEVID"].ToString() + ")'><option value='0' selected>停用</option><option value='1'>启用</option></select>";
                }
                //if (co.IndexOf("KFSQ_编辑") > 0)
                //{
                //    if (Convert.ToInt32(dt.Rows[i]["STATE"]) == 1)
                //    {
                //        newRow["STATE"] = "<select class='textinput' style='margin:0px;padding:0px;' onchange='ChangeState(this," + dt.Rows[i]["DEVID"].ToString() + ")'><option value='0'>停用</option><option value='1' selected>启用</option></select>";
                //    }
                //    else
                //    {
                //        newRow["STATE"] = "<select class='textinput' style='margin:0px;padding:0px;' onchange='ChangeState(this," + dt.Rows[i]["DEVID"].ToString() + ")'><option value='0' selected>停用</option><option value='1'>启用</option></select>";
                //    }
                //}
                //else
                //{
                //    newRow["STATE"] = "";
                //}

                newRow["EDITROW"] = "<a href='javascript:void(0);' title='编辑' onclick='Edit(" + dt.Rows[i]["DEVID"].ToString() + ")'><img src='/themes/default/images/blank.gif' class='icon ico-edit' alt='编辑' /></a>";

                //if (co.IndexOf("KFSQ_编辑") > 0)
                //{
                //    newRow["EDITROW"] = "<a href='javascript:void(0);' title='编辑' onclick='Edit(" + dt.Rows[i]["DEVID"].ToString() + ")'><img src='../../backend/themes/default/images/blank.gif' class='icon ico-edit' alt='编辑' /></a>";
                //}
                //else
                //{
                //    newRow["EDITROW"] = "";
                //}

                newRow["DELROW"] = "<a href='javascript:void(0);' title='删除' onclick='Del(" + dt.Rows[i]["DEVID"].ToString() + ")'><img src='/themes/default/images/blank.gif' class='icon ico-del' alt='删除' /></a>";

                //if (co.IndexOf("KFSQ_删除") > 0)
                //{
                //    newRow["DELROW"] = "<a href='javascript:void(0);' title='删除' onclick='Del(" + dt.Rows[i]["DEVID"].ToString() + ")'><img src='../../backend/themes/default/images/blank.gif' class='icon ico-del' alt='删除' /></a>";
                //}
                //else
                //{
                //    newRow["DELROW"] = "";
                //}
                dtAppList.Rows.Add(newRow);
            }

            return "{\"page\":" + page + ",\"total\":" + totalCount + ",\"rows\":" + DataTable2Json.CreateJsonParameters(dtAppList) + "}";
        }
        public int AddApp(string dev,string devcode,string siteurl,string callbackurl,string logouturl,string linkman,string mobile,string email,string memo)
        {
            Mdl.Entity.DEVELOPER devp = new Mdl.Entity.DEVELOPER();
            devp.ACCCODE = MakeUID();
            devp.CALLBACKURL = callbackurl;
            devp.CREATDATE = DateTime.Now;
            devp.CREATOR = -1;
            devp.DEVCODE = devcode;
            devp.DEVNAME = dev;
            devp.EMAIL = email;
            devp.LINKMAN = linkman;
            devp.LOGOUTURL = logouturl;
            devp.MEMO = memo;
            devp.MOBILE = mobile;
            devp.MODIFIER = -1;
            devp.MODIFYDATE = DateTime.Now;
            devp.SITEURL = siteurl;
            devp.STATE = 1;
            int newDevId;
            int ret = appDal.InsertDevAccredit(devp, out newDevId);
            return ret;
        }
        public int EditApp(int devid, string dev, string devcode, string siteurl, string callbackurl, string logouturl, string linkman, string mobile, string email, string memo)
        {
            Mdl.Entity.DEVELOPER devp = new Mdl.Entity.DEVELOPER();
            //devp.ACCCODE = MakeUID();
            devp.DEVID = devid;
            devp.CALLBACKURL = callbackurl;
            //devp.CREATDATE = DateTime.Now;
            //devp.CREATOR = -1;
            devp.DEVCODE = devcode;
            devp.DEVNAME = dev;
            devp.EMAIL = email;
            devp.LINKMAN = linkman;
            devp.LOGOUTURL = logouturl;
            devp.MEMO = memo;
            devp.MOBILE = mobile;
            devp.MODIFIER = -1;
            devp.MODIFYDATE = DateTime.Now;
            devp.SITEURL = siteurl;
            devp.STATE = 1;
            //int newDevId;
            int ret = appDal.UpdateDevAccredit(devp);
            return ret;
        }
        /// <summary>
        /// 删除一条开发者信息
        /// </summary>
        /// <param name="context">上下文环境</param>
        /// <returns>0表示成功，数据不存在返回1，出错返回-1</returns>
        public string DelDevAccredit(int devId)
        {
            int ret = appDal.DeleteDevAccredit(devId);
            if (ret == 0)
            {
                return "0";
            }
            else if (ret == 1)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }
        public JsonResult GetAppById(int devId)
        {
            return Json(appDal.SelectDevAccreditByDevId(devId));
        }
        /// <summary>
        /// 保存授权
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="appIds"></param>
        /// <returns></returns>
        public int SaveDevAccredit(int devId, string appIds)
        {
            return appDal.SaveDevAccredit(devId, appIds.Split('|'));            
        }
        /// <summary>
        /// 根据开发id获得所有应用及授权状态
        /// </summary>
        /// <param name="context">上下文环境</param>
        /// <returns></returns>
        public string GetAppByDevId(int devId)
        {            
            DataTable dt = appDal.SelectAppByDevId(devId);
            string json = "[";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                json += "{ id:" + dt.Rows[i]["APPID"].ToString() + ", pId:" + dt.Rows[i]["PID"].ToString() + ", name:'" + dt.Rows[i]["APPNAME"].ToString() + "', data:{ code:'" + dt.Rows[i]["APPCODE"].ToString() + "', ordernum:" + dt.Rows[i]["ORDERNUM"].ToString() + " },checked:'" + (dt.Rows[i]["CHECKED"].ToString() == "1" ? "true" : "false") + "' }";
                if (i != dt.Rows.Count - 1)
                {
                    json += ",";
                }
            }

            json += "]";
            return json;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="context">上下文环境</param>
        /// <returns>0表示成功</returns>
        public string EditAppState(int devId,int state)
        {
            int ret = appDal.UpdateState(devId, state);
            if (ret > 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        /// <summary>
        /// 生成16为唯一开发授权码
        /// </summary>
        /// <returns></returns>
        private string MakeUID()
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string uid = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(sg.NewGuid().ToString())), 4, 8);
            uid = uid.Replace("-", "");
            return uid;
        }
    }
}
