using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syn.SSO.Controllers
{
    public class SSOController : Controller
    {
        //
        // GET: /SSO/

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginCheck()
        {
            return View();
        }

    }
}
