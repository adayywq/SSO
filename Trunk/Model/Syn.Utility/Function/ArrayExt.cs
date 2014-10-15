using System;
using System.Collections;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 字符串数组操作类
    /// </summary> 
    public static class ArrayExt
    {
        #region 判断指定字符串在指定字符串数组中的位置

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetIndex(this string[] stringArray, string strSearch, bool caseInsensetive)
        {
            int length = stringArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        #endregion

        #region 判断指定字符串在指定字符串数组中的位置

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetIndex(this string[] stringArray, string strSearch)
        {
            return stringArray.GetIndex(strSearch, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string[] stringArray, string strSearch, bool caseInsensetive)
        {
            return stringArray.GetIndex(strSearch, caseInsensetive) >= 0;
        }

        #endregion

        #region 判断指定字符串是否属于指定字符串数组中的一个元素

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string[] stringarray, string strSearch)
        {
            return stringarray.InArray(strSearch, false);
        }

        #endregion

        #region 清除字符串数组中的重复项

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(this string[] strArray, int maxElementLength)
        {
            var h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            var result = new string[h.Count];

            h.Keys.CopyTo(result, 0);

            return result;
        }

        #endregion

        #region 清除字符串数组中的重复项

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(this string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        #endregion

        #region 过滤字符串数组中每个元素为合适的大小

        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] PadStringArray(this string[] strArray, int minLength, int maxLength)
        {
            if (strArray == null) throw new ArgumentNullException("strArray");
            if (minLength > maxLength)
            {
                int t = maxLength;
                maxLength = minLength;
                minLength = t;
            }

            int iMiniStringCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                    continue;
                }
                if (strArray[i].Length > maxLength)
                {
                    strArray[i] = strArray[i].Substring(0, maxLength);
                }
                iMiniStringCount++;
            }

            var result = new string[iMiniStringCount];
            for (int i = 0, j = 0; i < strArray.Length && j < result.Length; i++)
            {
                if (string.IsNullOrEmpty(strArray[i])) continue;
                result[j] = strArray[i];
                j++;
            }
            return result;
        }

        #endregion
    }
}