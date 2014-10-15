using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syn.Utility.Function
{
    public enum MessageType
    {
        success,
        error,
        alert
    }
    public class MessageBox
    {
        /// <summary>
        /// Iframes the alert.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="content">The content.</param>
        public static void IframeAlert(System.Web.UI.Page page, String content, Boolean isZh)
        {
            if (isZh)
            {
                page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "parent.asyncbox.alert('" + content + "','提示信息');", true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "parent.asyncbox.alert('" + content + "','prompting message');", true);
            }
        }
        /// <summary>
        /// Alerts the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="content">The content.</param>
        public static void Alert(System.Web.UI.Page page, String content, Boolean isZh)
        {
            if (isZh)
            {
                page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "asyncbox.alert('" + content + "','提示信息');", true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "asyncbox.alert('" + content + "','Tip Information');", true);
            }
        }
        /// <summary>
        /// Tipses the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="content">The content.</param>
        public static void Tips(System.Web.UI.Page page, String content, MessageType type)
        {
            page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "asyncbox.tips('&nbsp;&nbsp;" + content + "','" + type + "',2000);", true);
        }
        /// <summary>
        /// Iframes the tips.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="content">The content.</param>
        /// <param name="type">The type.</param>
        public static void IframeTips(System.Web.UI.Page page, String content, MessageType type)
        {
            page.ClientScript.RegisterStartupScript(new object().GetType(), DateTime.Now.ToString(), "top.asyncbox.tips('&nbsp;&nbsp;" + content + "','" + type + "',2000);", true);
        }
    }
}
