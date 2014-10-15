using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Enumerable = System.Linq.Enumerable;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 字符串操作类
    /// </summary> 
    public static class StringExt
    {
        #region 获取符合正则的字符串

        /// <summary>
        /// 获取符合正则的字符串
        /// </summary>
        /// <param name="regexStr">正则表达式</param>
        /// <param name="content">源字符串</param>
        /// <returns>匹配后的字符串</returns>
        public static string GetResultByRegex(this string content, string regexStr)
        {
            var regex = new Regex(regexStr);
            Match m = regex.Match(content);
            string result = string.Empty;
            if (m.Success)
            {
                result = m.Groups[1].Value;
            }
            return result;
        }

        #endregion

        #region 判断指定字符串是否属于指定字符串数组中的一个元素

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string stringArray, string strSearch)
        {
            return stringArray.ToArray(",").InArray(strSearch, false);
        }

        #endregion

        #region 判断指定字符串是否属于指定字符串数组中的一个元素

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">内部以分隔符分割单词的字符串</param>
        /// <param name="strSplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string stringArray, string strSearch, string strSplit)
        {
            return stringArray.ToArray(strSplit).InArray(strSearch, false);
        }

        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <param name="strSplit">分隔符</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] ToArray(this string strContent, string strSplit)
        {
            if (!String.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                {
                    string[] tmp = {strContent};
                    return tmp;
                }
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            return new string[] {};
        }

        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <param name="strSplit">分隔符</param>
        /// <returns>分隔后的字符串数组</returns>
        /// <returns></returns>
        public static string[] ToArray(this string strContent, string strSplit, int count)
        {
            var result = new string[count];

            string[] splited = ToArray(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] ToArray(this string strContent, string strSplit, bool ignoreRepeatItem,
                                       int maxElementLength)
        {
            string[] result = ToArray(strContent, strSplit);

            return ignoreRepeatItem ? result.DistinctStringArray(maxElementLength) : result;
        }

        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="minElementLength">单个元素最小长度</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] ToArray(this string strContent, string strSplit, bool ignoreRepeatItem,
                                       int minElementLength, int maxElementLength)
        {
            string[] result = ToArray(strContent, strSplit);
            if (ignoreRepeatItem)
            {
                result = result.DistinctStringArray();
            }
            return result.PadStringArray(minElementLength, maxElementLength);
        }

        #endregion

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] ToArray(this string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return ToArray(strContent, strSplit, ignoreRepeatItem, 0);
        }

        #endregion

        #region 去掉字符串中的所有空格

        /// <summary>
        /// 去掉字符串中的所有空格
        /// </summary>
        /// <param name="strContent">原字符串</param>
        /// <returns>去掉空格后的字符串</returns>
        public static string DropBlanks(this string strContent)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                var reg = new Regex(@"[\s|　]", RegexOptions.Singleline | RegexOptions.Compiled);
                return reg.Replace(strContent, string.Empty).Replace("&nbsp;", " ");
            }
            return string.Empty;
        }

        #endregion

        #region 是否包括stringarray.split(strsplit)[i]

        /// <summary>
        /// 是否包括stringTarget.split(strSplit)[i]中的任何一个
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="stringTarget">以strSplit为分割的字符串</param>
        /// <param name="strSplit">分割符号</param>
        /// <returns>true or false</returns>
        public static bool IsCompriseStr(this string strContent, string stringTarget, string strSplit)
        {
            if (string.IsNullOrEmpty(stringTarget) || string.IsNullOrEmpty(strSplit))
            {
                return false;
            }
            strContent = strContent.ToLower();
            string[] stringArray = stringTarget.ToArray(strSplit);
            return Enumerable.Any(stringArray, t => strContent.IndexOf(t) > -1);
        }

        #endregion

        #region 判断指定字符串是否属于指定字符串数组中的一个元素

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">内部以分割字符串分割单词的字符串</param>
        /// <param name="strSplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string stringArray, string strSearch, string strSplit, bool caseInsensetive)
        {
            return stringArray.ToArray(strSplit).InArray(strSearch, caseInsensetive);
        }

        #endregion

        #region 替换文本

        /// <summary>
        /// 替换文本中的空格、大于号、小于号以及软回车，自动转换Email、HTTP地址
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string EncodeText(this string strContent)
        {
            strContent = strContent.Replace("\"", "&quot;"); //处理双引号
            strContent = strContent.Replace("'", "‘"); //处理单引号
            strContent = strContent.Replace(" ", "&nbsp;"); //处理空格   
            strContent = strContent.Replace("<", "&lt;"); //处理小于号   
            strContent = strContent.Replace(">", "&gt;"); //处理大于号   
            strContent = strContent.Replace("\n", "<br />"); //处理换行   
            var urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = urlregex.Replace(strContent,
                                          "<a href=\"" + urlregex.Match(strContent) + "\" target=\"_blank\">" +
                                          urlregex.Match(strContent) + "</a>");
            var emailregex = new Regex(@"([a-zA-Z_0-9.-]+\@[a-zA-Z_0-9.-]+\.\w+)",
                                       RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = emailregex.Replace(strContent,
                                            "<a href=\"mailto:" + emailregex.Match(strContent) + "\">" +
                                            emailregex.Match(strContent) + "</a>");
            return strContent;
        }

        #endregion

        #region 截断字符串

        /// <summary>
        /// 截断字符串，截断结果的长度将不会超过制定的最大长度
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <param name="intMaxLength">截取的最大长度</param>
        /// <returns>截取结果字符串</returns>
        public static string CompressString(this string strContent, int intMaxLength)
        {
            return CompressString(strContent, intMaxLength, true);
        }

        #endregion

        #region 截断字符串

        /// <summary>
        /// 截断字符串，截断结果的长度将不会超过制定的最大长度，并且可以指定是否添加省略号
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <param name="intMaxLength">截取的最大长度</param>
        /// <param name="appendSuffix">是否添加省略号</param>
        /// <returns>截取结果字符串</returns>
        public static string CompressString(this string strContent, int intMaxLength, bool appendSuffix)
        {
            if (string.IsNullOrEmpty(strContent))
                return string.Empty;
            int intUnicodeLength = 0;
            int intCharCount = 0;
            for (int i = 0; i < strContent.Length && intUnicodeLength < intMaxLength; i++)
            {
                if (strContent[i] > 255)
                    intUnicodeLength += 2;
                else
                    intUnicodeLength++;

                intCharCount++;
            }
            if (intUnicodeLength >= intMaxLength)
                if (appendSuffix)
                    return strContent.Substring(0, intCharCount) + "…";
                else
                    return strContent.Substring(0, intCharCount);
            return strContent;
        }

        #endregion

        #region 判断是否为base64字符串

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="strContent">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsBase64String(this string strContent)
        {
            return Regex.IsMatch(strContent, @"[A-Za-z0-9\+\/\=]");
        }

        #endregion

        #region 通用编码

        private static readonly String[] Hex =
            {
                "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
                "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
                "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
                "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
                "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
                "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
                "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
                "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
                "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
                "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
                "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
                "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
                "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
                "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
                "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
                "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"
            };

        private static readonly byte[] Val =
            {
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F,
                0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F, 0x3F
            };

        #endregion

        #region 对字符串进行编码

        /// <summary>
        /// 对字符串进行编码
        /// </summary>
        /// <param name="strContent">要编码的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string Escape(this string strContent)
        {
            var sbuf = new StringBuilder();
            int len = strContent.Length;
            for (int i = 0; i < len; i++)
            {
                int ch = strContent[i];
                /*if (ch == ' ') {                        // space : map to '+'
                    sbuf.Append("+");
                } else*/
                if ('A' <= ch && ch <= 'Z')
                {
                    // 'A'..'Z' : as it was
                    sbuf.Append((char) ch);
                }
                else if ('a' <= ch && ch <= 'z')
                {
                    // 'a'..'z' : as it was
                    sbuf.Append((char) ch);
                }
                else if ('0' <= ch && ch <= '9')
                {
                    // '0'..'9' : as it was
                    sbuf.Append((char) ch);
                }
                else if (ch == '-' || ch == '_' // unreserved : as it was
                         || ch == '.' || ch == '!'
                         || ch == '~' || ch == '*'
                         || ch == '\'' || ch == '('
                         || ch == ')')
                {
                    sbuf.Append((char) ch);
                }
                else if (ch <= 0x007F)
                {
                    // other ASCII : map to %XX
                    sbuf.Append('%');
                    sbuf.Append(Hex[ch]);
                }
                else
                {
                    // unicode : map to %uXXXX
                    sbuf.Append('%');
                    sbuf.Append('u');
                    int cht = ch;
                    sbuf.Append(Hex[(ch >> 8)]);
                    sbuf.Append(Hex[(0x00FF & cht)]);
                }
            }
            return sbuf.ToString();
        }

        #endregion

        #region 对字符串进行解码

        /// <summary>
        /// 对字符串进行解码
        /// </summary>
        /// <param name="strContent">要进行解码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string Unescape(this string strContent)
        {
            var sbuf = new StringBuilder();
            int i = 0;
            int len = strContent.Length;
            while (i < len)
            {
                int ch = strContent[i];
                /*if (ch == '+') {                        // + : map to ' '
                    sbuf.Append(' ');
                } else*/
                if ('A' <= ch && ch <= 'Z')
                {
                    // 'A'..'Z' : as it was
                    sbuf.Append((char) ch);
                }
                else if ('a' <= ch && ch <= 'z')
                {
                    // 'a'..'z' : as it was
                    sbuf.Append((char) ch);
                }
                else if ('0' <= ch && ch <= '9')
                {
                    // '0'..'9' : as it was
                    sbuf.Append((char) ch);
                }
                else switch (ch)
                {
                    case ')':
                    case '(':
                    case '\'':
                    case '*':
                    case '~':
                    case '!':
                    case '.':
                    case '_':
                    case '-':
                        sbuf.Append((char) ch);
                        break;
                    case '%':
                        {
                            int cint = 0;
                            if ('u' != strContent[i + 1])
                            {
                                // %XX : map to ascii(XX)
                                cint = (cint << 4) | Val[strContent[i + 1]];
                                cint = (cint << 4) | Val[strContent[i + 2]];
                                i += 2;
                            }
                            else
                            {
                                // %uXXXX : map to unicode(XXXX)
                                cint = (cint << 4) | Val[strContent[i + 2]];
                                cint = (cint << 4) | Val[strContent[i + 3]];
                                cint = (cint << 4) | Val[strContent[i + 4]];
                                cint = (cint << 4) | Val[strContent[i + 5]];
                                i += 5;
                            }
                            sbuf.Append((char) cint);
                        }
                        break;
                }
                i++;
            }
            return sbuf.ToString();
        }

        #endregion

        #region 返回字符串真实长度

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <returns>字符长度</returns>
        public static int GetStringLength(this string strContent)
        {
            return Encoding.Default.GetBytes(strContent).Length;
        }

        #endregion

        #region 清理字符串

        /// <summary>
        /// 清理字符串
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <returns></returns>
        public static string CleanInput(this string strContent)
        {
            return Regex.Replace(strContent.Trim(), @"[^\w\.@-]", "");
        }

        #endregion

        #region 替换回车换行符为HTML换行符

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        /// <param name="strContent">源字符串</param>
        /// <returns></returns>
        public static string StrFormat(string strContent)
        {
            string str2;

            if (strContent == null)
            {
                str2 = "";
            }
            else
            {
                strContent = strContent.Replace("\r\n", "<br />");
                strContent = strContent.Replace("\n", "<br />");
                str2 = strContent;
            }
            return str2;
        }

        #endregion

        #region 与字符串清除有关的操作

        #region 删除字符串尾部的回车/换行/空格

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <returns>结果字符串</returns>
        public static string RTrim(this string strContent)
        {
            for (int i = strContent.Length; i >= 0; i--)
            {
                if (strContent[i].Equals(" ") || strContent[i].Equals("\r") || strContent[i].Equals("\n"))
                {
                    strContent.Remove(i, 1);
                }
            }
            return strContent;
        }

        #endregion

        #region 清除给定字符串中的回车及换行符

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="strContent">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBr(this string strContent)
        {
            var regexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
            //Regex r = null;
            Match m;

            //r = new Regex(@"(\r\n)",RegexOptions.IgnoreCase);
            for (m = regexBr.Match(strContent); m.Success; m = m.NextMatch())
            {
                strContent = strContent.Replace(m.Groups[0].ToString(), "");
            }

            return strContent;
        }

        #endregion

        #endregion

        #region 截取字符串有关的操作

        #region 从字符串的指定位置截取指定长度的子字符串

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="strContent">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(this string strContent, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length*-1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }
                if (startIndex > strContent.Length)
                {
                    return "";
                }
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                if (length + startIndex > 0)
                {
                    length = length + startIndex;
                    startIndex = 0;
                }
                else
                {
                    return "";
                }
            }

            if (strContent.Length - startIndex < length)
            {
                length = strContent.Length - startIndex;
            }

            return strContent.Substring(startIndex, length);
        }

        #endregion

        #region 从字符串的指定位置开始截取到字符串结尾的了符串

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="strContent">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(this string strContent, int startIndex)
        {
            return CutString(strContent, startIndex, strContent.Length);
        }

        #endregion

        #region 字符串如果操过指定长度则将超出的部分用指定字符串代替

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="pSrcString">要检查的字符串</param>
        /// <param name="pLength">指定长度</param>
        /// <param name="pTailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(this string pSrcString, int pLength, string pTailString)
        {
            if (pTailString == null) return "";
            return GetSubString(pSrcString, 0, pLength, pTailString);
        }

        #endregion

        #region 取指定长度的字符串

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="pSrcString">要检查的字符串</param>
        /// <param name="pStartIndex">起始位置</param>
        /// <param name="pLength">指定长度</param>
        /// <param name="pTailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(this string pSrcString, int pStartIndex, int pLength, string pTailString)
        {
            if (pSrcString == null) return "";

            string myResult = pSrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(pSrcString);
            if (Enumerable.Any(Encoding.UTF8.GetChars(bComments),
                               c => (c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3')))
            {
                if (pStartIndex >= pSrcString.Length)
                {
                    return "";
                }
                return pSrcString.Substring(pStartIndex,
                                            ((pLength + pStartIndex) > pSrcString.Length)
                                                ? (pSrcString.Length - pStartIndex)
                                                : pLength);
            }

            if (pLength >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(pSrcString);

                //当字符串长度大于起始位置
                int pEndIndex = bsSrcString.Length;
                if (bsSrcString.Length > pStartIndex)
                {
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (pStartIndex + pLength))
                    {
                        pEndIndex = pLength + pStartIndex;
                    }
                    else
                    {
                        //当不在有效范围内时,只取到字符串的结尾

                        pLength = bsSrcString.Length - pStartIndex;
                        pTailString = "";
                    }

                    int nRealLength = pLength;
                    var anResultFlag = new int[pLength];

                    int nFlag = 0;
                    for (int i = pStartIndex; i < pEndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[pEndIndex - 1] > 127) && (anResultFlag[pLength - 1] == 1))
                    {
                        nRealLength = pLength + 1;
                    }

                    var bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, pStartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + pTailString;
                }
            }

            return myResult;
        }

        #endregion

        #region 截取指定长度的Unicode字符串

        /// <summary>
        /// 截取指定长度的Unicode字符串
        /// </summary>
        /// <param name="strContent">要截取的字符串</param>
        /// <param name="len">截取长度</param>
        /// <param name="pTailString">结束符号</param>
        /// <returns>截取后的字符串</returns>
        public static string GetUnicodeSubString(this string strContent, int len, string pTailString)
        {
            string result = string.Empty; // 最终返回的结果
            int byteLen = Encoding.Default.GetByteCount(strContent); // 单字节字符长度
            int charLen = strContent.Length; // 把字符平等对待时的字符串长度
            int byteCount = 0; // 记录读取进度
            int pos = 0; // 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(strContent.ToCharArray()[i]) > 255) // 按中文字符计算加2
                        byteCount += 2;
                    else // 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len) // 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    if (byteCount == len) // 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = strContent.Substring(0, pos) + pTailString;
            }
            else
                result = strContent;

            return result;
        }

        #endregion

        #region 截断字符串后用指定字符串结尾

        /// <summary>
        /// 截断字符串后用指定字符串结尾
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="cutLength">保留的字符串长度</param>
        /// <param name="chars">以chars结束</param>
        /// <returns></returns>
        public static string CutString(this string inputString, int cutLength, string chars)
        {
            if (inputString == null) return "";

            if (inputString.Length > cutLength)
            {
                inputString = inputString.Substring(0, cutLength) + chars;
            }
            return inputString;
        }

        #endregion

        #region 截取英文字符串，不截断单词

        /// <summary>
        /// 截取英文字符串，不截断单词。
        /// </summary>
        /// <param name="strContent">源字串</param>
        /// <param name="count">截取长度</param>
        /// <returns></returns>
        public static string CutEnglishString(this string strContent, int count)
        {
            string msg;
            if (strContent.Length <= count)
            {
                msg = strContent;
            }
            else
            {
                int position = count; //要截取的位置
                bool flag = false;
                while (!flag)
                {
                    if (position + 1 >= strContent.Length)
                    {
                        break;
                    }
                    string str = strContent.Substring(position, 1); //要截取的位置的字符
                    byte a = Encoding.ASCII.GetBytes(str)[0];
                    if (a < 65 || a > 122) //判断中英文
                    {
                        flag = true;
                    }
                    else
                    {
                        position += 1;
                    }
                }
                if (position + 1 == strContent.Length)
                {
                    msg = strContent.Substring(0, position + 1);
                }
                else
                {
                    msg = strContent.Substring(0, position + 1) + "...";
                }
            }
            return msg;
        }

        #endregion

        #region 截取英文字符串，不截断单词

        /// <summary>
        /// 截取英文字符串，不截断单词。
        /// </summary>
        /// <param name="strContent">源字串</param>
        /// <param name="count">截取长度</param>
        /// <param name="endstr">结束符</param>
        /// <returns></returns>
        public static string CutEnglishString(this string strContent, int count, string endstr)
        {
            string msg;
            if (strContent.Length <= count)
            {
                msg = strContent;
            }
            else
            {
                int position = count; //要截取的位置
                bool flag = false;
                while (!flag)
                {
                    if (position + 1 >= strContent.Length)
                    {
                        break;
                    }
                    string str = strContent.Substring(position, 1); //要截取的位置的字符
                    byte a = Encoding.ASCII.GetBytes(str)[0];
                    if (a < 65 || a > 122) //判断中英文
                    {
                        flag = true;
                    }
                    else
                    {
                        position += 1;
                    }
                }
                if (position + 1 == strContent.Length)
                {
                    msg = strContent.Substring(0, position + 1);
                }
                else
                {
                    msg = strContent.Substring(0, position + 1) + endstr;
                }
            }
            return msg;
        }

        #endregion

        #region 截短字串的函数

        /// <summary>
        /// 截短字串的函数
        /// </summary>
        /// <param name="strContent">要加工的字串</param>
        /// <param name="byteCount">长度</param>
        /// <returns>被加工过的字串</returns>
        public static string CutStrFromLeft(this string strContent, int byteCount)
        {
            if (byteCount < 1) return strContent;

            if (Encoding.Default.GetByteCount(strContent) <= byteCount)
            {
                return strContent;
            }
            byte[] txtBytes = Encoding.Default.GetBytes(strContent);
            var newBytes = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                newBytes[i] = txtBytes[i];
            }

            return Encoding.Default.GetString(newBytes)+"...";
        }

        #endregion

        #region 截取字符串

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputString">源字串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string CutStringFromLeft(this string inputString, int length)
        {
            int tempLen = 0;
            string tempString = "";            
            char[] cc = inputString.ToCharArray();

            for (int i = 0; i < cc.Length; i++)
            {
                if (cc[i] >= 255)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen >= length)
                {
                    if (tempLen > length)
                    {
                        tempString = tempString.Substring(0, tempString.Length - 1);
                    }

                    if (cc[i] < 128)
                    {
                        int lastSpace = tempString.LastIndexOf(' ');
                        if (lastSpace != -1)
                        {
                            tempString = tempString.Substring(0, lastSpace);
                        }
                    }
                    break;
                }
            }

            if (tempString.Length < inputString.Length)
                tempString += "...";

            return tempString;
        }

        #endregion

        #endregion

        #region 合并字符

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(this string source, string target)
        {
            return MergeString(source, target, ",");
        }

        #endregion

        #region 合并字符

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(this string source, string target, string mergechar)
        {
            if (String.IsNullOrEmpty(target))
            {
                target = source;
            }
            else
            {
                target += mergechar + source;
            }
            return target;
        }

        #endregion

        #region 自定义的替换字符串函数

        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="searchString">匹配的正则</param>
        /// <param name="replaceString">要替换成的字符串</param>
        /// <param name="isCaseInsensetive">是否忽略大小写</param>
        /// <returns>匹配后的字符串</returns>
        public static string ReplaceString(this string sourceString, string searchString, string replaceString,
                                           bool isCaseInsensetive)
        {
            return Regex.Replace(sourceString, Regex.Escape(searchString), replaceString,
                                 isCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        #endregion

        #region 为脚本替换特殊字符串

        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(this string strContent)
        {
            strContent = strContent.Replace("\\", "\\\\");
            strContent = strContent.Replace("'", "\\'");
            strContent = strContent.Replace("\"", "\\\"");
            return strContent;
        }

        #endregion

        #region 判断字符串是否是yy-mm-dd字符串

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="strContent">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(this string strContent)
        {
            return Regex.IsMatch(strContent, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        #endregion

        #region 删除最后一个字符

        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string ClearLastChar(this string strContent)
        {
            if (string.IsNullOrEmpty(strContent))
                return string.Empty;
            return strContent.Substring(0, strContent.Length - 1);
        }

        #endregion

        #region 检测字符串长度是否符合要求

        /// <summary>
        /// 检测字符串长度是否符合要求
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="max">最大长度</param>
        /// <param name="min">最小长度</param>
        /// <returns>true 在此范围内</returns>
        public static bool CheckStringLength(this string strContent, int max, int min)
        {
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }
            int iLen = Encoding.Default.GetBytes(strContent).Length;
            if (min > 0 && iLen < min && iLen > max)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 判断字符串是否符合正则表达式

        /// <summary>
        /// 判断字符串是否符合正则表达式
        /// </summary>
        /// <param name="inputString">要判断的字符串</param>
        /// <param name="pattern">正则</param>
        /// <returns></returns>
        public static bool IsMatch(this string inputString, string pattern)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(inputString);
        }

        #endregion

        #region 提取满足某正则式的字符串

        /// <summary>
        /// 提取满足某正则式的字符串
        /// </summary>
        /// <param name="inputString">字符串</param>
        /// <param name="pattern">正则式</param>
        /// <returns>string<Link/></returns>
        public static string GetStringByText(this string inputString, string pattern)
        {
            var reg = new Regex(pattern);
            Match match = reg.Match(inputString);
            string str = string.Empty;
            if (match != null)
            {
                str = match.Groups[0].Value;
            }
            return str;
        }

        #endregion

        #region 返回无英文字符串

        /// <summary>
        /// 返回无英文字符串
        /// </summary>
        public static string StrReplaceEnglish(this string strContent)
        {
            var r = new Regex("[a-zA-Z]");
            return r.Replace(strContent, "");
        }

        #endregion

        #region 高亮显示字符串

        /// <summary>
        /// 将字符串标记为高亮
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="keyword">高亮的关键字</param>
        public static string HighLightString(this string inputString, string keyword)
        {
            return HighLightString(inputString, keyword, "#FF0000");
        }

        /// <summary>
        /// 将字符串标记为高亮
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="cutLength">保留的字符串长度</param>
        /// <param name="keyword">高亮的关键字</param>
        public static string HighLightString(this string inputString, int cutLength, string keyword)
        {
            if (inputString.Length > cutLength)
            {
                inputString = inputString.Substring(0, cutLength) + "...";
            }
            return HighLightString(inputString, keyword, "#FF0000");
        }

        /// <summary>
        /// 将字符串标记为高亮
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="keyWord">高亮的关键字</param>
        /// <param name="highLightColor">高亮的颜色</param>
        /// <returns></returns>
        public static string HighLightString(this string inputString, string keyWord, string highLightColor)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                return inputString.Replace(keyWord, "<span style='color:" + highLightColor + "'>" + keyWord + "</span>");
            }
            return inputString;
        }

        #endregion

        #region 构造正文字符串,去掉文章内容中的HTML标签

        /// <summary>
        /// 构造正文字符串,去掉文章内容中的HTML标签
        /// </summary>
        /// <param name="strContent">原正文字符串</param>
        /// <returns></returns>
        public static string InitContent(this string strContent)
        {
            var regex1 = new Regex(@"<.*?>|&nbsp;|\r\n|≮.*?≯|㊣"); //去掉HTML代码,回车换行符,及参数配置特殊字符
            var regex2 = new Regex(" {2,}"); //替换两个以上的连续空格为一个空格

            strContent = regex1.Replace(strContent, "");
            strContent = regex2.Replace(strContent, "");

            return strContent;
        }

        #endregion

        #region 判断是否只为字母数字

        /// <summary>
        /// 判断是否只为字母数字
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static bool Valiate(this string strContent)
        {
            bool r = false;
            char[] arrChar = strContent.ToCharArray(0, strContent.Trim().Length);

            foreach (char char1 in arrChar)
            {
                if (char.IsLetterOrDigit(char1))
                {
                    if (char1.IsChinese())
                    {
                        return false;
                    }
                    r = true;
                }
                else
                {
                    return false;
                }
            }
            return r;
        }

        #endregion

        #region 替换字符串中指定的字符(忽视大小写)

        /// <summary>
        /// 替换字符串中指定的字符(忽视大小写)
        /// </summary>
        /// <param name="strContent">原字符串的内容</param>
        /// <param name="isReplaced">要替换的字段内容</param>
        /// <param name="replaced">替换后的字段内容</param>
        /// <param name="fromReplaced">从第几位开始替换(注意默认为1)</param>
        /// <param name="replacedViews">替换的次数(-1表示所有)</param>
        /// <returns>被替换使用指定字符替换过的新字符换</returns>
        public static string StringReplace(this string strContent, string isReplaced, string replaced, int fromReplaced,
                                           int replacedViews)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                return Strings.Replace(strContent, isReplaced, replaced, fromReplaced, replacedViews);
            }
            return "";
        }

        #endregion
    }
}