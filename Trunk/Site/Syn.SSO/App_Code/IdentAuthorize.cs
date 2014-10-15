using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syn.SSO
{
    public class IdentAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string authorizeExcept = String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthorizeExcept"]) ? "" : System.Configuration.ConfigurationManager.AppSettings["AuthorizeExcept"].ToLower();

            Dictionary<string, string> dicExcept = new Dictionary<string, string>();
            foreach (string grp in authorizeExcept.Split('|'))
            {
                if (String.IsNullOrEmpty(grp) || String.IsNullOrEmpty(grp.Split('#')[0]))
                {
                    continue;
                }
                dicExcept.Add(grp.Split('#')[0].Trim(), "," + grp.Split('#')[1].Trim(',').Trim() + ",");
            }

            string currController = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string currAction = filterContext.RouteData.Values["action"].ToString().ToLower();

            string allowActions = String.Empty;
            if (dicExcept.TryGetValue(currController, out allowActions))
            {
                if ((allowActions == ",all,") || allowActions.Contains("," + currAction + ","))
                {
                    return;
                }
            }

            //base.OnAuthorization(filterContext);
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
    }
}