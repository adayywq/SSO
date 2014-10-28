using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Syn.Utility.Function;
using System.Data.OracleClient;
using Syn.Utility.DBHelper;

namespace Mdl.SSO
{
    public class CardUser
    { 
        /// <summary>
        /// 根据SNO登录
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public DataTable LoginBySno(string sno)
        {
            string sqlData = "select a.SNO,a.NAME,a.ACCOUNT,a.QUERYPIN,p.CLASS from ACCOUNT a left join PID p on a.PID=p.CODE where trim(SNO)=:SNO";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据AccountId登录
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable LoginByAccId(string accountId)
        {
            string sqlData = "select a.SNO,a.NAME,a.ACCOUNT,a.QUERYPIN,p.CLASS from ACCOUNT a left join PID p on a.PID=p.CODE where ACCOUNT=:ACCOUNT";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("ACCOUNT",OracleType.VarChar)
            };
            paramData[0].Value = accountId;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据商户ID登录
        /// </summary>
        /// <param name="mercId"></param>
        /// <returns></returns>
        public DataTable LoginByMercId(string mercId)
        {
            string sqlData = "Select Account,MercName,Password from Mercacc where Account=:Account";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("Account",OracleType.Char,10)
            };
            paramData[0].Value = mercId;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据SNO获取用于SSO的用户信息
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public DataTable GetSsoUserBySno(string sno)
        {
            string sqlData = "select i.NO,i.Name,i.Sno,i.Account,i.Email,p.Class,p.Code PidCode,p.Name PidName,d.Code DeptCode,d.Name DeptName from idinformation i left join department d on DeptCode=Code left join Pid p on i.PidCode=p.Code  where trim(SNO)=:SNO";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            DataSet dsData = new DataSet();
            try
            {
                dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "根据SNO从一卡通中获取用户信息");
            }
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据AccountId获取用于SSO的用户信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable GetSsoUserByAccId(string accountId)
        {
            string sqlData = "Select a.SNO,a.NAME,a.ACCOUNT,p.Class,p.Code PidCode,p.Name PidName,a.DeptCode from Account a left join Pid p on a.PID=p.Code where ACCOUNT=:ACCOUNT";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("ACCOUNT",OracleType.VarChar)
            };
            paramData[0].Value = accountId;

            DataSet dsData = new DataSet();
            try
            {
                dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "根据AccountId从一卡通中获取用户信息");
            }
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据MercId获取用于SSO的用户信息
        /// </summary>
        /// <param name="mercId"></param>
        /// <returns></returns>
        public DataTable GetSsoUserByMercId(string mercId)
        {
            string sqlData = "Select Account,MercName from Mercacc where Account=:Account";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("Account",OracleType.Char,10)
            };
            paramData[0].Value = mercId;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据SNO获取其所有AccountId
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public DataTable GetAccIdBySno(string sno)
        {
            string sqlData = "select Account,CardId,Name from Account where trim(Sno)=:SNO";
            OracleParameter[] paramData = new OracleParameter[] 
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            DataSet dsData = new DataSet();
            dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据AccountId获取其对应的SNO
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable GetSnoByAccId(string accountId)
        {
            string sqlData = "select SNO,NAME,ACCOUNT from ACCOUNT where ACCOUNT=:ACCOUNT";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("ACCOUNT",OracleType.VarChar)
            };
            paramData[0].Value = accountId;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据DeptCode获取部门信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataTable GetDeptByCode(string deptCode)
        {
            string sqlData = "Select Code,Name,AreaCode from Department where Code=:Code";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("Code",OracleType.VarChar)
            };
            paramData[0].Value = deptCode;

            DataSet dsData = OrclHelper.IdenRead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }
    }
}
