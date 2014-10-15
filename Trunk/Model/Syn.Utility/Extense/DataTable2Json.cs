using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Syn.Utility.Extense
{
    public class DataTable2Json
    { /// <summary>
        /// 反回JSON数据到前台
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>JSON字符串</returns>   
        /// <author>william</author>
        /// <createtime>2013-4-27</createtime>
        /// <remarks></remarks>
        public static string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                string rowData = string.Empty;
                JsonString.Append("[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rowData = dt.Rows[i][j].ToString();
                        rowData = rowData.Replace("'", "\'").Replace("\\", "\\\\").Replace("\"","\\\"").Replace("\n","\\n").Replace("\r","\\r");//替换特殊字符
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + rowData + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + rowData + "\"");
                        }
                    }


                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
