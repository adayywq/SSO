using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Syn.Utility.DBHelper;

namespace Mdl.SSO
{
    public class Developer
    {
        /// <summary>
        /// 根据DevCode获取已接入系统信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <returns></returns>
        public static DataTable GetByDevCode(string devCode)
        {
            string sqlData = "select DevId,AccCode,DevName,DevCode,Linkman,Mobile,Email,SiteUrl,CallbackUrl,LogoutUrl,State from Developer where DevCode=:DevCode";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("DevCode",OracleType.VarChar)
            };
            paramData[0].Value = devCode.ToUpper();

            DataSet dsData = null;
            try
            {
                dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "根据DevCode获取已接入系统信息");
            }
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }
    }
}
