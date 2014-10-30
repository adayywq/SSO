using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Syn.Special;
using Syn.Utility.Function;
using System.Data.OracleClient;
using Syn.Utility.DBHelper;

namespace Mdl.SSO
{
    public class TokenInfo
    {        
        /// <summary>
        /// 生成SessionKey
        /// </summary>
        /// <param name="useScene"></param>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public string AddToken(Mdl.Entity.TOKENINFO mdlToken)
        {
            string sqlData = "Insert into TokenInfo (TokenID,Token,LoginId,LoginType,UseSys,DataSource,ClientIp,ClientMac,LastUpdate) values (TokenID.NEXTVAL,:Token,:LoginId,:LoginType,:UseSys,:DataSource,:ClientIp,:ClientMac,:LastUpdate)";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("Token",OracleType.VarChar),
                new OracleParameter("LoginId",OracleType.VarChar),
                new OracleParameter("LoginType",OracleType.VarChar),
                new OracleParameter("UseSys",OracleType.VarChar),
                new OracleParameter("DataSource",OracleType.VarChar),
                new OracleParameter("ClientIp",OracleType.VarChar),
                new OracleParameter("ClientMac",OracleType.VarChar),
                new OracleParameter("LastUpdate",OracleType.DateTime)
            };
            paramData[0].Value = mdlToken.TOKEN;
            paramData[1].Value = mdlToken.LOGINID;
            paramData[2].Value = mdlToken.LOGINTYPE;
            paramData[3].Value = mdlToken.USESYS;
            paramData[4].Value = mdlToken.DATASOURCE;
            paramData[5].Value = mdlToken.CLIENTIP;
            paramData[6].Value = mdlToken.CLIENTMAC;
            paramData[7].Value = mdlToken.LASTUPDATE;

            try
            {
                if (OrclHelper.SSOWrite.ExecuteNonQuery(sqlData, CommandType.Text, paramData) > 0)
                    return mdlToken.TOKEN;
                else
                    return "";
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "Token生成失败");
                return "";
            }
        }

        /// <summary>
        /// 生成SessionKey
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public string AddToken(string devCode, string loginType, string loginId)
        {
            string sessionKey = Guid.NewGuid().ToString().GetMD5HashCode().ToHexString();

            Mdl.Entity.TOKENINFO mdlToken = new Mdl.Entity.TOKENINFO();
            mdlToken.TOKEN = sessionKey;
            mdlToken.LOGINID = loginId;
            mdlToken.LOGINTYPE = loginType;
            mdlToken.USESYS = devCode.Trim().ToUpper();
            mdlToken.DATASOURCE = ConfigurationManager.AppSettings["RunType"] == null ? "card" : ConfigurationManager.AppSettings["RunType"].ToString().ToLower();
            mdlToken.CLIENTIP = "";
            mdlToken.CLIENTMAC = "";
            mdlToken.LASTUPDATE = DateTime.Now;

            return AddToken(mdlToken);
        }

        /// <summary>
        /// 删除指定SessionKey
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public bool DeleteSk(string useScene, string sessionKey)
        {
            if (String.IsNullOrEmpty(sessionKey))
            {
                return false;
            }
            string sqlData = "Delete from TokenInfo where Token=:Token";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("Token",OracleType.VarChar)
            };
            paramData[0].Value = sessionKey;
            try
            {
                OrclHelper.SSOWrite.ExecuteNonQuery(sqlData, CommandType.Text, paramData);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "删除指定SessionKey失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除过期SessionKey
        /// </summary>
        /// <param name="devCode"></param>
        /// <returns></returns>
        public bool DelOldSk(string devCode)
        {
            int intervalTime = ((ConfigurationManager.AppSettings["BackOvertime"] == null) || String.IsNullOrEmpty(ConfigurationManager.AppSettings["Overtime"].ToString())) ? 60 : ConfigurationManager.AppSettings["Overtime"].ToString().ToInt(60);

            string timeOut = DateTime.Now.AddMinutes(intervalTime * -1).ToString();

            string sqlData = "Delete from TokenInfo where LastUpdate<:LastUpdate";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                //new OracleParameter("UseSys",OracleType.VarChar),
                new OracleParameter("LastUpdate",OracleType.DateTime)
            };
            //paramData[0].Value=devCode;
            paramData[0].Value=Convert.ToDateTime(timeOut);

            try
            {
                OrclHelper.SSOWrite.ExecuteNonQuery(sqlData, CommandType.Text, paramData);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "删除过期SessionKey失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查SessionKey是否存在
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sessionKey"></param>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public bool IsExistsSk(string devCode, string sessionKey, ref string loginType, ref string loginId)
        {
            if (String.IsNullOrEmpty(sessionKey))
            {
                return false;
            }

            //清除过期的SessionKey
            DelOldSk(devCode);

            try
            {
                //string sqlData = "Select TokenID,LoginId,LoginType from TokenInfo where UseSys=:UseSys and Token=:Token";
                string sqlData = "Select TokenID,LoginId,LoginType from TokenInfo where Token=:Token";
                OracleParameter[] paramData = new OracleParameter[] 
                {
                    //new OracleParameter("UseSys",OracleType.VarChar),
                    new OracleParameter("Token",OracleType.VarChar)
                };
                //paramData[0].Value=devCode;
                paramData[0].Value = sessionKey;

                DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
                if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
                {
                    return false;
                }
                else
                {
                    loginId = dsData.Tables[0].Rows[0]["LoginId"].ToString();
                    loginType = dsData.Tables[0].Rows[0]["LoginType"].ToString();
                }

                string sqlUpdate = "Update TokenInfo set LastUpdate=:LastUpdate where TokenID=:TokenID";
                OracleParameter[] paramUpdate = new OracleParameter[] 
                {
                    new OracleParameter("LastUpdate",OracleType.DateTime),
                    new OracleParameter("TokenID",OracleType.VarChar)
                };
                paramUpdate[0].Value = DateTime.Now;
                paramUpdate[1].Value = dsData.Tables[0].Rows[0]["TokenID"].ToString();

                OrclHelper.SSOWrite.ExecuteNonQuery(sqlUpdate, CommandType.Text, paramUpdate);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "检查SessionKey是否存在失败");
                return false;
            }
            return true;
        }
    }
}
