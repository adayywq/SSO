using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syn.Utility.Function;

namespace Syn.SSO
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string devCode = RequestHelper.GetString("devCode");
            string returnUrl = RequestHelper.GetString("returnUrl");
            //new SSO.Controllers.SSOController().Login(devCode, returnUrl);
        }
    }
}