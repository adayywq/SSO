using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syn.Utility.Function;

namespace Syn.SSO
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string devCode = RequestHelper.GetString("devCode");
            string returnUrl = RequestHelper.GetString("returnUrl");
            string sk = RequestHelper.GetString("sk");
            new SSO.Controllers.SSOController().Logout(devCode, returnUrl, sk);
        }
    }
}