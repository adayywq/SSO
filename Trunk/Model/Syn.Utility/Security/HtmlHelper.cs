using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Syn.Utility.Function;

namespace Syn.Utility.Security
{
    /// <summary>
    /// Author:AutoTech
    /// HTML代码过滤
    /// 主要用于过滤被禁用HTML-JAVASCRIPT
    /// </summary>
    public class HtmlHelper
    {
        #region 过滤字符中包含的空格
        /// <summary>
        /// 过滤字符中包含的空格
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string FilterWhiteSpace(string inStr)
        {
            return String.IsNullOrEmpty(inStr) ? string.Empty : Regex.Replace(inStr, @"\s", "");
        }
        #endregion

        #region 过滤非法禁用SCRIPT块

        /// <summary>
        /// 过滤非法禁用SCRIPT块
        /// </summary>
        /// <returns>过滤后的字符串</returns>
        public static string FilterScript(string inStr)
        {
            return Regex.Replace(inStr, @"<script(.|\n)+</script>", "", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 过滤非法样式标记

        /// <summary>
        /// 过滤非法样式标记
        /// </summary>
        /// <returns>过滤后的字符串</returns>
        public static string FilterStyle(string inStr)
        {
            return Regex.Replace(inStr, @"<style(.|\n)+</style>", "", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 过滤非法HTML标签

        /// <summary>
        /// 过滤非法HTML
        /// </summary>
        /// <returns>过滤后的字符串</returns>
        public static string FilterHtml(string inStr)
        {
            if (String.IsNullOrEmpty(inStr)) return string.Empty;
            inStr = Regex.Replace(inStr, @"<HTML>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"<HEAD>(.|\n)>+</HEAD>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"<META[^>]+>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"<title(.|\n)>+</title>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"<body>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"</body>", "", RegexOptions.IgnoreCase);
            inStr = Regex.Replace(inStr, @"</HTML>", "", RegexOptions.IgnoreCase);
            return inStr;
        }
        #endregion

        #region 过滤HTML标签
        /// <summary>
        /// 过滤Html"标签"
        /// &lt;P&gt;dagsd&lt;/P&gt;
        /// </summary>
        /// <param name="htmlText">源字符串</param>
        /// <returns>去除Html"标签"后的字符串</returns>
        public static string FilterHtmlTags(string htmlText)
        {
            
            if (htmlText != null)
            {
                if (htmlText != "")
                {
                    htmlText = htmlText.Replace("&lt;", "<").Replace("&gt;", ">");
                    if (string.IsNullOrEmpty(htmlText))
                        return string.Empty;
                    var reg = new Regex(@"<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
                    htmlText = reg.Replace(htmlText, string.Empty);

                    return htmlText;
                }
                return "";
            }
            return "";
        }
        #endregion

        #region 移除HTML标记
        /// <summary>
        /// 移除HTML标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            const string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 过滤所有客户端元素(HTML,SCRIPT)
        /// <summary>
        /// 过滤所有客户端元素(HTML,SCRIPT)
        /// </summary>
        /// <param name="inStr">原字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterHtmlAndScriptTag(string inStr)
        {
            inStr = Regex.Replace(inStr, "<script[^>]+>[\\s\\S]*?/script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            inStr = Regex.Replace(inStr, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return inStr.Trim();
        }
        #endregion

        #region 转换为静态HTML
        /// <summary>
        /// 转换为静态html
        /// </summary>
        public static void TransHtml(string path, string outpath)
        {
            var page = new System.Web.UI.Page();

            var writer = new StringWriter();
            page.Server.Execute(path, writer);
            FileStream fs;
            if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
            {
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            byte[] bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }
        #endregion

        #region 过滤HTML中的不安全标签
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return content;
            }

            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        #endregion

        #region 过滤不安全的字符
        /// <summary>
        /// 过滤不安全的字符
        /// </summary>
        /// <param name="html">要过滤的字符串</param>
        /// <returns></returns>
        public static string CheckStr(string html)
        {
            if (String.IsNullOrEmpty(html))
            {
                return html;
            }

            var regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            var regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            var regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            var regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            var regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);

            var regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            var regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);

            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace(">", "").Replace("\\", "").Replace("'", "").Replace("\"", "");

            string[] aryReg = { "'", "<", ">", "%", "\"\"", ">=", "=<", ";", "||", "[", "]", "&", "/", "|" };

            return aryReg.Aggregate(html, (current, t) => current.Replace(t, string.Empty));
        }
        #endregion

        #region 是否包含XSS攻击字符
        /// <summary>
        /// 是否包含XSS攻击字符
        /// </summary>
        /// <param name="inText"></param>
        /// <returns></returns>
        public static bool IsXssString(string inText)
        {
            if (inText == null || "".Equals(inText))
                return false;
            var regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            var regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            var regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            var regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            var regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            var regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            var regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);
            var regex9 = new Regex(@"<iframe[\s\S] *", RegexOptions.IgnoreCase);
            if (regex1.IsMatch(inText) || regex2.IsMatch(inText) || regex3.IsMatch(inText) || regex4.IsMatch(inText) || regex5.IsMatch(inText) || regex6.IsMatch(inText) || regex7.IsMatch(inText) || regex8.IsMatch(inText) || regex9.IsMatch(inText))
                return true;
            return false;
        }
        #endregion

        #region 将用户组Title中的font标签去掉
        /// <summary>
        /// 将用户组Title中的font标签去掉
        /// </summary>
        /// <param name="title">用户组Title</param>
        /// <returns></returns>
        public static string RemoveFontTag(string title)
        {
            var regexFont = new Regex(@"<font color=" + "\".*?\"" + @">([\s\S]+?)</font>", Utils.GetRegexCompiledOptions());
            var m = regexFont.Match(title);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            return title;
        }
        #endregion 

        #region 从HTML中获取文本
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetTextFromHtml(string html)
        {
            if (html == null) throw new ArgumentNullException("html");
            var regEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);

            return regEx.Replace(html, "");
        }
        #endregion

        #region 生成指定数量的HTML空格符号
        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < spacesCount; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }
        #endregion

    }
}


