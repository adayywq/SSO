using System.Text;
using System.Web;

namespace Syn.Utility.Extense
{
    public class Pagination
    { 
        /// <summary>
        /// 获得伪静态页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="expname"></param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            return GetStaticPageNumbers(curPage, countPage, url, expname, extendPage, 0);
        }

        /// <summary>
        /// 获得伪静态页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="expname"></param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="forumrewrite">当前版块是否使用URL重写</param>
        /// <returns>页码html</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage, int forumrewrite)
        {
            var startPage = 1;
            int endPage;

            var t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            var t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (forumrewrite == 1)
            {
                t1 = "<a href=\"" + url + "/1/list" + expname + "\">&laquo;</a>";
                t2 = "<a href=\"" + url + "/" + countPage + "/list" + expname + "\">&raquo;</a>";
            }

            if (forumrewrite == 2)
            {
                t1 = "<a href=\"" + url + "/1/\">&laquo;</a>";
                t2 = "<a href=\"" + url + "/" + countPage + "/\">&raquo;</a>";
            }

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            var s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    if (forumrewrite == 1)
                    {
                        s.Append(url);
                        s.Append("/");
                        s.Append(i);
                        s.Append("/list");
                        s.Append(expname);
                    }
                    else if (forumrewrite == 2)
                    {
                        s.Append(url);
                        s.Append("/");
                        s.Append(i);
                        s.Append("/");
                    }
                    else
                    {
                        s.Append(url);
                        s.Append("-");
                        s.Append(i);
                        s.Append(expname);
                    }
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }

        /// <summary>
        /// 获得帖子的伪静态页码显示链接
        /// </summary>
        /// <param name="expname"></param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int startPage = 1;
            int endPage;
            const int curPage = 1;

            string t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            var s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                s.Append("<a href=\"");
                s.Append(url);
                s.Append("-");
                s.Append(i);
                s.Append(expname);
                s.Append("\">");
                s.Append(i);
                s.Append("</a>");
            }
            s.Append(t2);

            return s.ToString();
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="recordCount">总记录</param>
        ///  <param name="pageSize">一页几行</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int recordCount, int pageSize, string url, int extendPage)
        {
            int pageCount;
            if (recordCount <= pageSize)
            {
                pageCount = 1;
            }
            else
            {
                pageCount = (recordCount / pageSize) + 1;
            }
            return GetPageNumbers(curPage, pageCount, url, extendPage);
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <param name="anchor">锚点</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (countPage <= 1)
            { return ""; }
            if (pagetag == "")
                pagetag = "page";
            int startPage = 1;
            int endPage;

            if (url.IndexOf("?") > 0)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }

            var t1 = new StringBuilder();
            t1.Append("<a href=\"");
            t1.Append(url);
            t1.Append("&");
            t1.Append(pagetag);
            t1.Append("=1\" class=\"f10\" >&lt;&lt;</a> ");
            t1.Append("<a href=\"");
            t1.Append(url);
            t1.Append("&");
            t1.Append(pagetag);
            t1.Append("=");
            t1.Append(curPage - 1);
            t1.Append("\" class=\"f10\">&lt;</a> ");
            var t2 = new StringBuilder();
            t2.Append("<a href=\"");
            t2.Append(url);
            t2.Append("&");
            t2.Append(pagetag);
            t2.Append("=");
            t2.Append(curPage + 1);
            t2.Append("\" class=\"f10\"  >&gt;</a> <a href=\"");
            t2.Append(url);
            t2.Append("&");
            t2.Append(pagetag);
            t2.Append("=");
            t2.Append(countPage);
            t2.Append("\" class=\"f10\"  >&gt;&gt;</a>");

            if (anchor != null)
            {
                t1.Append(anchor);
                t2.Append(anchor);
            }
            if (curPage == 1)
            {
                t1.Remove(0, t1.Length);
                t1.Append("  ");
            }
            if (curPage == countPage)
            {
                t2.Remove(0, t2.Length);
                t2.Append("  ");
            }



            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        // t2 = " ";
                    }
                }
                else
                {
                    endPage = extendPage;
                    //t1 = " ";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                //t1 = " ";
                //t2 = " ";
            }

            var s = new StringBuilder("");

            s.Append(t1.ToString());
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span class=\"cur\">");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append(" <a href=\"");
                    s.Append(url);
                    s.Append(pagetag);
                    s.Append("=");
                    s.Append(i);
                    if (anchor != null)
                    {
                        s.Append(anchor);
                    }
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a> ");
                }
            }
            s.Append(t2.ToString());

            return s.ToString();
        }


        /// <summary>
        /// 获取ajax形式的分页链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="callback">回调函数</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns></returns>
        public static string GetAjaxPageNumbers(int curPage, int countPage, string callback, int extendPage)
        {
            const string pagetag = "page";
            int startPage = 1;
            int endPage;

            string t1 = "<a href=\"###\" onclick=\"" + string.Format(callback, "&" + pagetag + "=1");
            string t2 = "<a href=\"###\" onclick=\"" + string.Format(callback, "&" + pagetag + "=" + countPage);

            t1 += "\">&laquo;</a>";
            t2 += "\">&raquo;</a>";

            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            var s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"###\" onclick=\"");
                    s.Append(string.Format(callback, pagetag + "=" + i));
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }



        /// <summary>
        /// 向页面输出xml内容
        /// </summary>
        /// <param name="xmlnode">xml内容</param>
        public static void ResponseXml(string xmlnode)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "Text/XML";
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Write("<?xml version=\"1.0\"?>\r\n");
            HttpContext.Current.Response.Write("<info><![CDATA[");
            HttpContext.Current.Response.Write(xmlnode);
            HttpContext.Current.Response.Write("]]></info>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出纯字符格式的xml
        /// </summary>
        /// <param name="xmlnode"></param>
        public static void NewResponseXml(string xmlnode)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "Text/XML";
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Write(xmlnode);
            HttpContext.Current.Response.End();
        }
    }
}
