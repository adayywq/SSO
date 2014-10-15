using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using Syn.Utility;
using Syn.Utility.Function;
using System.Data.OracleClient;

namespace Syn.Special
{
    public class ApiHelper
    {
        public static string CheckApi(string cmd, params string[] appMustParams)
        {
            string ac = RequestHelper.GetString("ac").Trim().ToLower();
            string dt = String.IsNullOrEmpty(RequestHelper.GetString("dt")) ? "json" : RequestHelper.GetString("dt").Trim().ToLower();
            string sign = RequestHelper.GetString("sign").Trim().ToLower();

            string param = "";
            if (String.IsNullOrEmpty(ac))
                param += "ac,";
            if (String.IsNullOrEmpty(sign))
                param += "sign,";
            for (int i = 0; i < appMustParams.Length; i++)
            {
                string appParam = RequestHelper.GetString(appMustParams[i]);
                if (String.IsNullOrEmpty(appParam))
                    param = param + appMustParams[i].Trim().ToLower() + ",";
            }
            if (!String.IsNullOrEmpty(param.Trim(',')))
            {
                return ApiHelper.ReturnApi(dt, "dk0053", param.Trim(','), "", "");
            }

            //参数与签名比对
            if (!ApiHelper.CheckApiSign())
            {
                return ApiHelper.ReturnApi(dt, "dk0054", "", "", "");
            }

            //用户权限检查
            if (!ApiHelper.CheckApiPower(ac, cmd))
            {
                return ApiHelper.ReturnApi(dt, "dk0055", "", "", "");
            }
            return "succeed";
        }

        /// <summary>
        /// API参数吕与签名串比对
        /// </summary>
        /// <returns></returns>
        public static bool CheckApiSign()
        {
            List<string> lstQuery = HttpContext.Current.Request.QueryString.ToString().Split('&').ToList();
            List<string> lstForm = HttpContext.Current.Request.Form.ToString().Split('&').ToList();

            string signQuery = lstQuery.Find(item => { return item.Contains("sign="); });
            string signForm = lstForm.Find(item => { return item.Contains("sign="); });
            string sign = String.IsNullOrEmpty(signQuery) ? (String.IsNullOrEmpty(signForm) ? "" : signForm.Trim().Remove(0, 5)) : signQuery.Trim().Remove(0, 5);

            lstQuery.RemoveAll(item => { return item.Contains("sign="); });
            lstForm.RemoveAll(item => { return item.Contains("sign="); });
            string param = String.Join("&", lstQuery.Concat(lstForm).OrderBy(item => item).ToArray()).Trim('&').ToLower();
            string encryptParam = EncryptHelper.Md5Encrypts(param);
            return (encryptParam.ToLower() == sign.ToLower());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac">系统编号</param>
        /// <param name="cmd">请求方法</param>
        /// <returns>是否有执行权限</returns>
        public static bool CheckApiPower(string ac, string apiCode)
        {
            string pcode = apiCode.Split('_')[0].ToString();
            string ccode = apiCode.Split('_')[1].ToString();
            string sqlInsert = " select Daid from DEVAPPR where devid=(select Devid from DEVELOPER where devcode=lower(:devcode) or devcode=Upper(:devcode) and state=1 ) and appid=(select appid from application where Pid = (select appid from  application where appcode=lower(:pcode) or appcode=Upper(:pcode)) and appcode=lower(:scode) or appcode=Upper(:scode))  ";
            OracleParameter[] sqlParam = new OracleParameter[]{  
                new OracleParameter("devcode",ac),
                new OracleParameter("pcode",pcode),
                new OracleParameter("scode",ccode)
            };

            OracleDataReader dsOrclUser = null;
            try
            {
                dsOrclUser = Syn.Utility.DBHelper.OrclHelper.SSORead.ExecuteReader(sqlInsert, sqlParam);
                if (dsOrclUser != null)
                {
                    while (dsOrclUser.Read())
                    {
                        if (dsOrclUser["Daid"].ToString() != "" || dsOrclUser["Daid"] != DBNull.Value)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "获取第三方授权权限");
            }
            finally
            {
                dsOrclUser.Dispose();
            }
            return false;
        }

        /// <summary>
        /// 返回API的结果集
        /// </summary>
        /// <param name="dt">结果集数据类型</param>
        /// <param name="apiCode">信息代码</param>
        /// <param name="codeRemark">信息码附记</param>
        /// <param name="sysRemark">系统级信息附记</param>
        /// <param name="data">请求结果集</param>
        /// <returns></returns>
        public static string ReturnApi(string dt, string apiCode, string codeRemark, string sysRemark, string data)
        {
            string ret = "";
            switch (dt.Trim().ToLower())
            {
                case "json":
                    ret = ReturnApiJosn(apiCode, codeRemark, sysRemark, data);
                    break;
                case "xml":
                    ret = ReturnApiXml(apiCode, codeRemark, sysRemark, data);
                    break;
                default:
                    ret = ReturnApiJosn(apiCode, codeRemark, sysRemark, data);
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 返回API的json结果集
        /// </summary>
        /// <param name="apiCode">信息代码</param>
        /// <param name="codeRemark">信息码附记</param>
        /// <param name="sysRemark">系统级信息附记</param>
        /// <param name="data">请求结果集</param>
        /// <returns></returns>
        public static string ReturnApiJosn(string apiCode, string codeRemark, string sysRemark, string data)
        {
            apiCode = apiCode.ToLower().Trim();
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            //sbJson.Append("\"result\":\"" + ((apiCode == "sp0000") ? "succeed" : "failed") + "\",");
            sbJson.Append("\"code\":\"" + apiCode + "\",");
            sbJson.Append("\"msg\":\"" + GetApiMsgByCode(apiCode) + (String.IsNullOrEmpty(codeRemark) ? "" : ("(" + codeRemark + ")")) + "\",");
            sbJson.Append("\"time\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",");
            sbJson.Append(sysRemark.Trim());
            sbJson.Append("\"data\":" + data);
            sbJson.Append("}");
            return sbJson.ToString();
        }

        /// <summary>
        /// 返回API的xml结果集
        /// </summary>
        /// <param name="apiCode">信息代码</param>
        /// <param name="codeRemark">信息码附记</param>
        /// <param name="sysRemark">系统级信息附记</param>
        /// <param name="data">请求结果集</param>
        /// <returns></returns>
        public static string ReturnApiXml(string apiCode, string codeRemark, string sysRemark, string data)
        {
            apiCode = apiCode.ToLower().Trim();
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sbXml.Append("<synjones>");
            //sbXml.Append("<result>" + ((apiCode == "sp0000") ? "succeed" : "failed") + "</result>");
            sbXml.Append("<code>" + apiCode + "</code>");
            sbXml.Append("<msg>" + GetApiMsgByCode(apiCode) + (String.IsNullOrEmpty(codeRemark) ? "" : ("(" + codeRemark + ")")) + "</msg>");
            sbXml.Append("<time>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</time>");
            sbXml.Append(sysRemark.Trim());
            sbXml.Append("<data>" + data + "</data>");
            sbXml.Append("</synjones>");
            return sbXml.ToString();
        }

        /// <summary>
        /// 通过ApiCode.xml获取信息代码对应在的信息说明
        /// </summary>
        /// <param name="apiCode">信息代码</param>
        /// <returns></returns>
        private static string GetApiMsgByCode(string apiCode)
        {
            string xPath = "//ApiCode";
            XDocument root = null;
            try
            {
                root = XDocument.Load(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/ApiCode.xml");
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex, "杨伟强", "ApiCode.xml文件不存在");
                return "";
            }

            XElement handler = null;
            try
            {
                if (root != null)
                {
                    handler = root.XPathSelectElement(xPath);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex, "杨伟强", "ApiCode.xml文件xPath错误");
                return "";
            }

            try
            {
                if (handler != null)
                {
                    string ret = handler.Element(apiCode).Value;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex, "杨伟强", "ApiCode.xml文件读取节点出错");
                return "";
            }

            return "";
        }
    }
}
