using System.Text;
using System.Web.UI;
using System.Data;

namespace Syn.Utility.Extense
{
    public static class DataSetExt 
    {
        /// <summary>
        /// 将dataset里的数据导出为Excel.
        /// </summary>
        /// <param name="fileName">导出时保存的文件名.</param>
        /// <param name="page">页对象.</param>
        /// <param name="dataset">数据集.</param>
        public static void ToExcel(string fileName, Page page, DataSet dataset) 
        {
            page.Response.Clear();
            page.Response.Buffer = false;
            page.Response.Charset = "GB2312";
            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            page.Response.ContentEncoding = Encoding.GetEncoding("GB2312");//设置输出流为简体中文
            page.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            page.EnableViewState = false;
            page.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=gb2312>");
            page.Response.Write(ToHtml(dataset));
            page.Response.Flush();
        }

        /// <summary>
        /// 将DataSet中的表输出成HTML格式的表格字符串
        /// </summary>
        /// <param name="dataset">要输出的DataSet</param>
        /// <returns>Html的表格字符串(可被Excel认识)</returns>
        public static string ToHtml(DataSet dataset)
        {
            if (dataset == null)
                return string.Empty;

            var strBuilder = new StringBuilder();
            foreach (DataTable table in dataset.Tables)
            {
                strBuilder.AppendLine("<table border=1 cellspacing=0 cellpadding=5 rules=all>");

                //输出表头.
                strBuilder.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                foreach (DataColumn headColumn in table.Columns)
                {
                    strBuilder.AppendLine("<td>" + headColumn.ColumnName + "</td>");
                }
                strBuilder.AppendLine("</tr>");

                //输出数据.
                foreach (DataRow row in table.Rows)
                {
                    strBuilder.AppendLine("<tr>");
                    foreach (DataColumn dataColumn in table.Columns)
                    {
                        strBuilder.AppendLine("<td>");
                        strBuilder.AppendLine(row[dataColumn].ToString());
                        strBuilder.AppendLine("</td>");
                    }
                    strBuilder.AppendLine("</tr>");
                }
                strBuilder.AppendLine("</table>");
            }
            return strBuilder.ToString();
        }
    }
}
