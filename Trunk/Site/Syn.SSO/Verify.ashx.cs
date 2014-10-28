using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Syn.Utility.Function;

namespace Syn.SSO
{
    /// <summary>
    /// Verify 的摘要说明
    /// </summary>
    public class Verify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string cmd = RequestHelper.GetString("cmd").Trim().ToLower();
            string rlt = new Mdl.API.SSOV1().SsoApiV1(cmd);

            context.Response.ContentType = "text/plain";
            context.Response.Write(rlt);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}