using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Syn.Utility.Security
{
    /// <summary>
    /// Author:AutoTech
    /// SQL过滤
    /// </summary>
    public class Sql
    {
        #region SQL安全过滤
        /// <summary>
        /// SQL安全过滤
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>安全过滤后的SQL语句</returns>
        public static string SqlTrans(string strSql)
        {
            if (String.IsNullOrEmpty(strSql)) return string.Empty;
            //进行安全过滤
            strSql = strSql.Replace("'", "''");
            strSql = strSql.Replace("%", "％");
            strSql = strSql.Replace(";", "；");
            strSql = strSql.Replace("/*", "");
            strSql = strSql.Replace("--", "——");
            strSql = strSql.Replace("&nbsp；", "&nbsp;");
            return SqlSafe(strSql);
        }

        public static string SqlSafe(string sql)
        {
            if (String.IsNullOrEmpty(sql)) return string.Empty;

            string restr = Regex.Replace(sql, "exec", "ｅｘｅｃ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            restr = Regex.Replace(restr, "declare", "ｄｅｃｌａｒｅ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            restr = Regex.Replace(restr, "update", "ｕｐｄａｔｅ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            restr = Regex.Replace(restr, "delete", "ｄｅｌｅｔｅ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            restr = Regex.Replace(restr, "select", "ｓｅｌｅｃｔ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return restr;
        }
        #endregion

        #region 是否有攻击请求
        /// <summary>
        /// 处理用户提交的请求
        /// </summary>
        public static bool IsAttackRequest()
        {
            bool iAttack = false;//没有攻击请求
            try
            {
                string getkeys;
                if (HttpContext.Current.Request.QueryString != null)
                {
                    for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
                    {

                        getkeys = HttpContext.Current.Request.QueryString.Keys[i];
                        if (!IsAttackCode(getkeys))//判断参数名是否含有攻击字符串
                        {
                            iAttack = true;
                            break;
                        }

                        if (!IsAttackCode(HttpContext.Current.Request.QueryString[getkeys]))
                        {
                            iAttack = true;
                            break;
                        }
                    }
                }

                if (HttpContext.Current.Request.Form != null)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
                    {
                        getkeys = HttpContext.Current.Request.Form.Keys[i];
                        if (!IsAttackCode(getkeys))
                        {
                            iAttack = true;
                            break;
                        }

                        if (!IsAttackCode(HttpContext.Current.Request.Form[getkeys]))
                        {
                            iAttack = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                iAttack = true;
            }

            return iAttack;
        }

        #endregion

        #region 分析是否含有攻击代码
        /// <summary>
        /// 分析是否含有攻击代码
        /// </summary>
        /// <param name="strCode">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        private static bool IsAttackCode(string strCode)
        {
            if (string.IsNullOrEmpty(strCode))
                return true;

            bool returnValue = true;
            try
            {
                if (strCode != "")
                {
                    const string sqlStr = " and |exec |insert |select |delete |update |count | * |chr |mid |master |truncate |char |declare |; | |<|>|'|(|)|{|}";
                    string[] anySqlStr = sqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (strCode.IndexOf(ss) >= 0)
                        {
                            returnValue = false;
                        }
                    }
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }
        #endregion

        #region 检测是否有危险的可能用于链接的字符串
        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsStrSafe(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }
        #endregion

        #region 改正SQL语句中的转义字符
        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        public static string MashSql(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }
        #endregion

        #region 替换SQL语句中的有问题符号
        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSql(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                str2 = str;
            }
            return str2;
        }
        #endregion
    }
    
}