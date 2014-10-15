using System;
using System.Web.UI;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 注册客户端JS的各类常用函数封装
    /// </summary>
    public class ClientScript
    {
        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">警告信息</param>
        public static void Alert(Page page, string alertInfo)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');",alertInfo), true);
        }

        /// <summary>
        /// 显示警告信息并后退
        /// </summary>
        /// <param name="page"></param>
        /// <param name="alertInfo"></param>
        public static void ScriptBlockAlertAndBack(Page page, string alertInfo)
        {
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), "alert('" + alertInfo + "');window.history.back();", true);
        }

        /// <summary>
        /// 显示提示信息，然后刷新当前页面
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">提示信息</param>
        public static void AlertAndRedirect(Page page, string alertInfo)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');window.location=document.URL;", alertInfo), true);
        }
       
        /// <summary>
        /// 页面加载完后马上显示警告信息
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">警告信息</param>
        public static void StartupAlert(Page page, string alertInfo)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", alertInfo), true);
        }

        /// <summary>
        /// 页面加载完后马上显示警告信息，并跳转到另一页
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">警告信息</param>
        /// <param name="redirectUrl">跳转页面URL</param>
        public static void StartupAlert(Page page, string alertInfo, string redirectUrl)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');window.location.href = \"{1}\";", alertInfo, redirectUrl), true);
        }

        /// <summary>
        /// 页面加载完后马上显示警告信息，并跳转到另一页
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">警告信息</param>
        /// <param name="redirectUrl">跳转页面URL</param>
        /// <param name="isTop">是否整个框架</param>
        public static void StartupAlert(Page page, string alertInfo, string redirectUrl, bool isTop)
        {
            string parent = isTop ? "top" : "window";
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');{1}.location.href = \"{2}\";", alertInfo, parent ,redirectUrl), true);
        }

        /// <summary>
        /// 页面跳转，局部框架下的页面跳转
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="redirectUrl">跳转页面URL</param>
        /// <param name="isTop">是否整个框架</param>
        public static void Redirect(Page page, string redirectUrl, bool isTop)
        {
            string parent = isTop ? "top" : "window";
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("{0}.location.href = \"{1}\";", parent, redirectUrl), true);
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        public static void Refresh(Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(),"window.location=document.URL;",true);
        }

        /// <summary>
        /// 页面输出脚本代码块
        /// </summary>
        /// <param name="page">调用页</param>
        /// <param name="scriptCode">脚本代码</param>
        public static void BlockJs(Page page, string scriptCode)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), scriptCode, true);
        }

        /// <summary>
        /// 页面加载完后马上执行脚本代码块
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="scriptCode">脚本代码块</param>
        public static void StartupScript(Page page, string scriptCode)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), scriptCode, true);
        }

        /// <summary>
        /// 页面输出JS文件
        /// </summary>
        /// <param name="page">调用页</param>
        /// <param name="jsUrl">JS文件路径</param>
        public static void IncludeJs(Page page, string jsUrl)
        {
            page.ClientScript.RegisterClientScriptInclude(page.GetType(), Guid.NewGuid().ToString(), jsUrl);
        }

        /// <summary>
        /// 确认提示框
        /// </summary>
        /// <param name="page">调用页</param>
        /// <param name="confirmContent">提示框内容，不能为空</param>
        /// <param name="okRedirectUrl">点确定转向的页面Url地址，不能为空</param>
        /// <param name="cancelRedirectUrl">点取消转向的页面地址或关闭本页，如果关闭本页则输入null</param>
        public static void Confirm(Page page, string confirmContent, string okRedirectUrl, string cancelRedirectUrl)
        {
            if (string.IsNullOrEmpty(confirmContent) || string.IsNullOrEmpty(okRedirectUrl)) return;
            string scriptBlock = "if(confirm('{0}')){{ window.location.href ='{1}' }}else {{{2};}} ";
            cancelRedirectUrl = string.IsNullOrEmpty(cancelRedirectUrl)
                                    ? "window.close()"
                                    : string.Format("window.location.href ='{0}'",cancelRedirectUrl);
            scriptBlock = string.Format(scriptBlock, confirmContent, okRedirectUrl, cancelRedirectUrl);

            BlockJs(page, scriptBlock);
        }

        /// <summary>
        /// 弹出新页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="url">要弹出的页面</param>
        public static void OpenWindow(Page page, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(),string.Format("window.open ('{0}');",url),true);
        }

        /// <summary>
        /// 弹出新页面（弹出指定的特殊小窗体）
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="url">要弹出的页面</param>
        /// <param name="param">所需参数</param>
        public static void OpenWindow(Page page, string url, string param)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("window.open ('{0}',{1});", url, param), true);
        }

        /// <summary>
        /// 带提示框的弹出新页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="url">要弹出的页面</param>
        /// <param name="alertInfo">警告信息</param>
        public static void OpenAlertWindow(Page page, string url, string alertInfo)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');window.open ('{1}');", alertInfo, url),true);
        }

        /// <summary>
        /// 调用脚本关闭本页，无提示;
        /// </summary>
        /// <param name="page"></param>
        public static void WindowClose( Page page)
        {
            const string js = "window.opener=null;window.open('','_self','');window.close();";
            StartupScript(page, js);
        }

        /// <summary>
        /// 调用脚本关闭本页，有提示;
        /// </summary>
        /// <param name="page"></param>
        /// <param name="alertMessage">关闭前提示信息</param>
        public static void WindowClose(Page page,string alertMessage)
        {
            if(!string.IsNullOrEmpty(alertMessage))
            {
                string js = string.Format("alert('{0}');window.opener=null;window.open('','_self','');window.close();",alertMessage);
                StartupScript(page, js);
            }
            else
            {
                WindowClose(page);
            }
        }

        /// <summary>
        /// 调用脚本关闭本页，有提示,并调用来源窗口js函数;
        /// </summary>
        /// <param name="page"></param>
        /// <param name="functionName"></param>
        /// <param name="alertMessage">关闭前提示信息</param>
        public static void WindowCloseAndInvokeOpenerFunction(Page page, string alertMessage, string functionName)
        {
            if (!string.IsNullOrEmpty(alertMessage))
            {
                string openerFunction = string.Empty;
                if (!string.IsNullOrEmpty(functionName))
                {
                    openerFunction = string.Format("window.opener.{0}();", functionName);
                }
                string js =
                    string.Format("alert('{0}');{1}window.opener=null;window.open('','_self','');window.close();",
                                  alertMessage, openerFunction);
                StartupScript(page, js);
            }
            else
            {
                WindowClose(page);
            }
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="control">WebControl</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl control, string msg)
        {
            control.Attributes.Add("onclick", string.Format("return confirm('{0}');",msg));
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="isTop">是否刷新整个框架</param>
        public static void Refresh(Page page, bool isTop)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(),
                                                    isTop
                                                        ? "top.location=document.URL;"
                                                        : "window.location=document.URL;", true);
        }

        /// <summary>
        /// 显示提示信息，页面跳转
        /// </summary>
        /// <param name="page">调用页面</param>
        /// <param name="alertInfo">提示信息</param>
        /// <param name="redirectUrl">跳转页面URL</param>
        /// <param name="isTop">是否整个框架</param>
        public static void AlertAndRedirect(Page page, string alertInfo, string redirectUrl, bool isTop)
        {
            string parent = isTop ? "top" : "window";
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');{1}.location.href = \"{2}\";", alertInfo, parent, redirectUrl), true);
        }

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="page"></param>
        public static void ScriptBlockBack(Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), "history.go(-1);", true);
        }

        /// <summary>
        /// 向前
        /// </summary>
        /// <param name="page"></param>
        public static void ScriptBlockForward(Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), "history.go(1);", true);
        }
    }
}
