using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Syn.Utility.Function
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class TypeExt
    {
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="expression">对象</param>
        /// <returns></returns>
        public static bool IsNumeric(this object expression)
        {
            if (expression != null)
            {
                return IsNumeric(expression.ToString());
            }
            return false;
        }

        /// <summary>
        /// 判断字符串是否为Int32类型的数字
        /// </summary>
        /// <param name="expression">字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(this string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 验证字符串是否为正整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsInt(this string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        /// <summary>
        /// 判断对象是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(this object expression)
        {
            if (expression != null)
            {
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        ///// <summary>
        ///// 判断字符串对象是否为null或者空(包括'').
        ///// </summary>
        ///// <param name="value">字符串对象.</param>
        ///// <returns>若为null或空,返回true,否则返回false.</returns>        
        //public static bool IsEmpty(this string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        if (value.Length > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    return true;
        //}

        /// <summary>
        /// 是否为数值串列表，各数值间用","间隔
        /// </summary>
        /// <param name="numList"></param>
        /// <returns></returns>
        public static bool IsNumericList(this string numList)
        {
            if (numList == "")
                return false;
            return numList.Split(',').All(IsNumeric);
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(this string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            return strNumber.All(IsNumeric);
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ToBool(this object expression, bool defValue)
        {
            if (expression != null)
            {
                return ToBool(expression, defValue);
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ToBool(this string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                {
                    return true;
                }
                if (string.Compare(expression, "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ToInt(this object expression, int defValue)
        {
            if (expression != null)
            {
                return ToInt(expression.ToString(), defValue);
            }
            return defValue;
        }

        /// <summary>
        /// 将string转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ToInt(this string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;
            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;
            return defValue;
        }



        /// <summary>
        /// object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ToFloat(this object strValue, float defValue)
        {
            if ((strValue == null))
            {
                return defValue;
            }

            return ToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ToFloat(this string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            var isFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (isFloat)
            {
                float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int ToInt32(this long objNum)
        {
            string strNum = objNum.ToString();
            if (IsNumeric(strNum))
            {

                if (strNum.Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    return int.MaxValue;
                }
                return Int32.Parse(strNum);
            }
            return 0;
        }

        /// <summary>
        /// 对象转换为Int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>int32</returns>
        public static int ToInt32(this object value)
        {
            if (value != null && value != DBNull.Value)
                return Convert.ToInt32(value);
            return 0;
        }

        /// <summary>
        /// 对象转换为字符串.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string</returns>
        public static string Tostring(this object value)
        {
            if (value != null && value != DBNull.Value)
                return value.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="sbcCase"></param>
        /// <returns></returns>
        public static string SbcCaseToNumberic(this string sbcCase)
        {
            char[] c = sbcCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(this string color)
        {
            int red, green, blue;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0] + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1] + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2] + rgb[2].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0] + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2] + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4] + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(color);

            }
        }

        /// <summary>
        /// 把Unicode中文 转码成16进制格式。
        /// </summary>
        /// <param name="str">传入字符串</param>
        /// <returns></returns>
        public static string ToHex(this string str)
        {
            var sb = new StringBuilder();
            byte[] bs = Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < bs.Length; i++)
            {
                byte lobyte = bs[i];
                byte upbyte = bs[++i];

                if (upbyte == 0)
                    sb.Append((char)lobyte);
                else
                {
                    string temp = @"\u";
                    temp += (upbyte).ToString("x2");
                    temp += (lobyte).ToString("x2");
                    sb.Append(temp);
                }
            }
            return sb.ToString();
        }

        ///<summary>
        ///转半角的函数(DBC case)
        ///</summary>
        ///<param name="input">任意字符串</param>
        ///<returns>半角字符串</returns>
        public static string ToDbc(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 字符内特殊数字串转换数字，如：①"、"②
        /// </summary>
        /// <param name="str">内容</param>
        /// <returns></returns>
        public static string CheckStringToNum(this string str)
        {
            string[] numList1 = { "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨" };
            string[] numList2 = { "⑴", "⑵", "⑶", "⑷", "⑸", "⑹", "⑺", "⑻", "⑼" };
            string[] numList3 = { "㈠", "㈡", "㈢", "㈣", "㈤", "㈥", "㈦", "㈧", "㈨" };
            string[] numList4 = { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            for (int i = 0; i < numList1.Length; i++)
            {
                str = str.Replace(numList1[i], (i + 1).ToString());
                str = str.Replace(numList2[i], (i + 1).ToString());
                str = str.Replace(numList3[i], (i + 1).ToString());
                str = str.Replace(numList4[i], (i + 1).ToString());
            }
            return str;
        }

        /// <summary>
        /// 对存入数据库的字符串进行转换
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string InputText(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                inputString = inputString.Trim();
                //inputString = inputString.Replace("&", "&#38;");
                //inputString = inputString.Replace("#", "&#35;");
                //inputString = inputString.Replace(";", "&#59;");
                inputString = inputString.Replace(" ", "&#32;");
                inputString = inputString.Replace("!", "&#33;");
                inputString = inputString.Replace("\"", "&#34;");
                inputString = inputString.Replace("$", "&#36;");
                inputString = inputString.Replace("%", "&#37;");
                inputString = inputString.Replace("\'", "&#39;");
                inputString = inputString.Replace("(", "&#40;");
                inputString = inputString.Replace(")", "&#41;");
                inputString = inputString.Replace("*", "&#42;");
                inputString = inputString.Replace("+", "&#43;");
                inputString = inputString.Replace(",", "&#44;");
                inputString = inputString.Replace("-", "&#45;");
                inputString = inputString.Replace(".", "&#46;");
                //inputString = inputString.Replace("/", "&#47;");
                inputString = inputString.Replace(":", "&#58;");
                inputString = inputString.Replace("<", "&#60;");
                inputString = inputString.Replace("=", "&#61;");
                inputString = inputString.Replace(">", "&#62;");
                inputString = inputString.Replace("?", "&#63;");
                inputString = inputString.Replace("@", "&#64;");
                inputString = inputString.Replace("[", "&#91;");
                //inputString = inputString.Replace("\\/", "&#92;");
                inputString = inputString.Replace("]", "&#93;");
                inputString = inputString.Replace("^", "&#94;");
                inputString = inputString.Replace("_", "&#95;");
                inputString = inputString.Replace("`", "&#96;");
                inputString = inputString.Replace("{", "&#123;");
                inputString = inputString.Replace("|", "&#124;");
                inputString = inputString.Replace("}", "&#125;");
                inputString = inputString.Replace("~", "&#126;");
                inputString = inputString.Replace("，", "&#xFF0C;");
                inputString = inputString.Replace("“", "&#x201C;");
                inputString = inputString.Replace("”", "&#x201D;");
                inputString = inputString.Replace("“", "&ldquo;");
                inputString = inputString.Replace("”", "&rdquo;");
                inputString = inputString.Replace("•", "&#x2022;");
                return inputString;
            }
            return "";
        }

        /// <summary>
        /// 对从数据库中读取出来的字符串进行转换
        /// </summary>
        /// <param name="outputString"></param>
        /// <returns></returns>
        public static string OutputText(this string outputString)
        {
            if (!string.IsNullOrEmpty(outputString))
            {
                outputString = outputString.Trim();
                outputString = outputString.Replace("&#32;", " ");
                outputString = outputString.Replace("&#33;", "!");
                outputString = outputString.Replace("&#34;", "\"");
                //outputString = outputString.Replace("&#35;", "#");
                outputString = outputString.Replace("&#36;", "$");
                outputString = outputString.Replace("&#37;", "%");
                //outputString = outputString.Replace("&#38;", "&");
                outputString = outputString.Replace("&#39;", "\'");
                outputString = outputString.Replace("&#40;", "(");
                outputString = outputString.Replace("&#41;", ")");
                outputString = outputString.Replace("&#42;", "*");
                outputString = outputString.Replace("&#43;", "+");
                outputString = outputString.Replace("&#44;", ",");
                outputString = outputString.Replace("&#45;", "-");
                outputString = outputString.Replace("&#46;", ".");
                //outputString = outputString.Replace("&#47;", "/");
                outputString = outputString.Replace("&#58;", ":");
                //outputString = outputString.Replace("&#59;", ";");
                outputString = outputString.Replace("&#60;", "<");
                outputString = outputString.Replace("&#61;", "=");
                outputString = outputString.Replace("&#62;", ">");
                outputString = outputString.Replace("&#63;", "?");
                outputString = outputString.Replace("&#64;", "@");
                outputString = outputString.Replace("&#91;", "[");
                //outputString = outputString.Replace("&#92;", "\\/");
                outputString = outputString.Replace("&#93;", "]");
                outputString = outputString.Replace("&#94;", "^");
                outputString = outputString.Replace("&#95;", "_");
                outputString = outputString.Replace("&#96;", "`");
                outputString = outputString.Replace("&#123;", "{");
                outputString = outputString.Replace("&#124;", "|");
                outputString = outputString.Replace("&#125;", "}");
                outputString = outputString.Replace("&#126;", "~");
                outputString = outputString.Replace("&#xFF0C;", "，");
                outputString = outputString.Replace("&#x201C;", "“");
                outputString = outputString.Replace("&#x201D;", "”");
                outputString = outputString.Replace("&ldquo;", "“");
                outputString = outputString.Replace("&rdquo;", "”");
                outputString = outputString.Replace("&mdash;", "—");
                //outputString = outputString.Replace("&#38;", "&");
                //outputString = outputString.Replace("&#35;", "#");
                //outputString = outputString.Replace("&#59;", ";");
                outputString = outputString.Replace("&#x2022;", "•");
                return outputString;
            }
            return "";
        }

        /// <summary>
        /// 将Unicode字符串转成汉字
        /// </summary>
        /// <param name="str">包含Unicode字符的字符串</param>
        /// <returns></returns>
        public static string UnicodeToCharacter(this string str)
        {
            string[] strItem = str.Split(new[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            foreach (string t in strItem)
            {
                sb.Append(UnicodeToUnitCharacter(t.Substring(0, 4)));
                if (t.Length > 4)
                {
                    sb.Append(t.Substring(4));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 把四个字符长度的Unicode转成对应的汉字
        /// </summary>
        /// <param name="str">长度是4的Unicode</param>
        /// <returns>对应的汉字，若转换出错则返回原字符串</returns>
        static string UnicodeToUnitCharacter(this string str)
        {
            try
            {
                byte code = Convert.ToByte(str.Substring(0, 2), 16);
                byte code2 = Convert.ToByte(str.Substring(2), 16);
                return Encoding.Unicode.GetString(new[] { code2, code });
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        /// 将string转换为Int64类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static long ToInt64(this string str, long defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 20 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;
            long rv;
            if (Int64.TryParse(str, out rv))
                return rv;
            return defValue;
        }
    }
}
    
