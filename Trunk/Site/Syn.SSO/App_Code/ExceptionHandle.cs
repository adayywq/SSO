using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syn.SSO
{
    public class ExceptionHandle : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            
            Syn.Utility.Log.WriteErrorLog(filterContext.Exception, "GP Team", "系统未捕捉异常");

            base.OnException(filterContext);
        }
    }
}