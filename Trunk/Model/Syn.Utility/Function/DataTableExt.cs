using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace Syn.Utility.Function
{
    /// <summary>
    /// 针对DataTable操作的一些通用方法
    /// </summary> 
    public static class DataTableExt
    {
        /// <summary>
        /// 根据filter把符合条件的DataRowCollection导入到另外的一个DataTable中
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static DataTable InsertFilterRowCollectionToDataTable(this DataTable dt, string filter)
        {
            DataTable dtResult = dt.Clone();
            DataRow[] drArray = dt.Select(filter);
            InsertDataRowArrayToDataTable(dtResult, drArray);
            return dtResult;
        }

        /// <summary>
        /// 把一个DataRow数组插入到一个与之对应的DataTable中
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="drArray">DataRow数组</param>
        public static void InsertDataRowArrayToDataTable(DataTable dt, DataRow[] drArray)
        {
            foreach (DataRow t in drArray)
            {
                dt.ImportRow(t);
            }
        }

        /// <summary>
        /// 把一个DataRow插入到一个与之对应的DataTable中
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dr">一个DataRow</param>
        public static void InsertDataRowToDataTable(DataTable dt, DataRow dr)
        {
            dt.ImportRow(dr);
        }


        /// <summary>
        /// 将List集合转化成DataTable
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合
            var pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            var dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                T item1 = item;
                pList.ForEach(p => row[p.Name] = p.GetValue(item1, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable 转换为集合
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>集合</returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
            Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            var oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                var ob = new T();
                //找到对应的数据  并赋值
                DataRow row1 = row;
                prlist.ForEach(p => { if (row1[p.Name] != DBNull.Value) p.SetValue(ob, row1[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }


        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTableTow(IList list)
        {
            var result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                foreach (object t in list)
                {
                    var tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable(list, null);
        }


        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            var propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            var result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                foreach (T t in list)
                {
                    var tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(t, null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(t, null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dtDispose"></param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(DataTable dt, bool dtDispose)
        {
            return DataTableToJson(dt, dtDispose, false);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dtDispose"></param>
        /// <param name="isOutput"></param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(DataTable dt, bool dtDispose,bool isOutput)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            //数据表字段名和类型数组
            var dtField = new string[dt.Columns.Count];
            var dtFieldType = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype;
            foreach (DataColumn dc in dt.Columns)
            {
                dtField[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                dtFieldType[i] = fieldtype;
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
            {
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号
            }
            formatStr += "}},";

            i = 0;
            var objectArray = new object[dtField.Length];
            foreach (DataRow dr in dt.Rows)
            {
#pragma warning disable 168
                foreach (string fieldname in dtField)
#pragma warning restore 168
                {   //对 \ , ' 符号进行转换 
                    string objStr = string.Empty;
                    objectArray[i] = objStr;
                    if (dr[dtField[i]] != null)
                    {
                        if (dtFieldType[i].IndexOf("datetime") > 0)
                        {
                            DateTime.Parse(dr[dtField[i]].ToString()).ToString("yyyy-MM-dd HH:mm");
                        }
                        else
                        {
                            objStr = isOutput ? dr[dtField[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r\n", "\\r\\n").Replace("\n","\\n").OutputText() : dr[dtField[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r\n", "\\r\\n").Replace("\n", "\\n");
                            objectArray[i] = objStr.ToHex();
                            //objectArray[i] = objStr;
                        }
                    }
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号
            }

            if (dtDispose)
            {
                dt.Dispose();
            }
            return stringBuilder.Append("]");
        }

        /// <summary>
        /// 执行DataTable中的查询返回新的DataTable
        /// </summary>
        /// <param name="dt">源数据DataTable</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static DataTable GetNewDataTable(DataTable dt, string condition)
        {
            DataTable newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition);
            foreach (DataRow t in dr)
            {
                newdt.ImportRow(t);
            }
            return newdt;//返回的查询结果
        }

        /// <summary>
        /// 执行DataTable中的查询返回新的DataTable,并排序
        /// </summary>
        /// <param name="dt">源数据DataTable</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns></returns>
        public static DataTable GetNewDataTableAndSort(DataTable dt, string condition, string sort)
        {
            DataTable newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition, sort);
            foreach (DataRow t in dr)
            {
                newdt.ImportRow(t);
            }
            return newdt;//返回的查询结果
        }
    }
}
