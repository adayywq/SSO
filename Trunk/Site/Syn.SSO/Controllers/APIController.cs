using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syn.Utility.Function;
using Syn.Special;

namespace Syn.SSO.Controllers
{
    public class APIController : Controller
    {
        ContentResult cr = new ContentResult();

        public ActionResult SSO(int ver,string cmd,string dt)
        {
            ver = ver==0 ? RequestHelper.GetInt("v", 1) : ver;
            cmd = String.IsNullOrEmpty(cmd) ? RequestHelper.GetString("cmd").Trim().ToLower() : cmd.Trim().ToLower();
            dt = String.IsNullOrEmpty(dt) ? (String.IsNullOrEmpty(RequestHelper.GetString("dt")) ? "json" : RequestHelper.GetString("dt").Trim().ToLower()) : dt.Trim().ToLower();
            switch (ver)
            {
                case 1:
                    cr.Content=new Mdl.API.SSOV1().SsoApiV1(cmd);
                    break;
                case 2:
                    cr.Content=new Mdl.API.SSOV2().SsoApiV2(cmd, dt);
                    break;
                default:
                    cr.Content = ApiHelper.ReturnApi(dt, "SSO0051", "", "", "");
                    break;
            }
            return cr;
        }
    }
}
