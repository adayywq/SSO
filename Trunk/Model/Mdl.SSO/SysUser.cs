using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Syn.Utility.Function;
using System.Data.OracleClient;
using Syn.Utility.DBHelper;

namespace Mdl.SSO
{
    public class SysUser
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="mdlSysUser"></param>
        /// <returns></returns>
        public int AddUser(Mdl.Entity.SysUser mdlSysUser)
        {
            int userId = 0;
            string sqlData = "Insert into UserInfo (UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,Pwd,InitIdent,DataSource,CreateDate,State) values (UserID.NextVal,:UniteID,:LoginType,:LoginID,:Name,:Email,:Mobile,:Pwd,:InitIdent,:DataSource,:CreateDate,:State)"
                            + " returning UserID into :UserId";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("UniteID",OracleType.VarChar),
                new OracleParameter("LoginType",OracleType.VarChar),
                new OracleParameter("LoginID",OracleType.VarChar),
                new OracleParameter("Name",OracleType.VarChar),
                new OracleParameter("Email",OracleType.VarChar),
                new OracleParameter("Mobile",OracleType.VarChar),
                new OracleParameter("Pwd",OracleType.VarChar),
                new OracleParameter("InitIdent",OracleType.VarChar),
                new OracleParameter("DataSource",OracleType.VarChar),
                new OracleParameter("CreateDate",OracleType.VarChar),
                new OracleParameter("State",OracleType.VarChar),
                new OracleParameter("UserId",OracleType.VarChar,50,ParameterDirection.Output,"UserID",DataRowVersion.Default,true,userId)
            };
            paramData[0].Value = mdlSysUser.UNITEID;
            paramData[1].Value = mdlSysUser.LOGINTYPE;
            paramData[2].Value = mdlSysUser.LOGINID;
            paramData[3].Value = mdlSysUser.NAME;
            paramData[4].Value = mdlSysUser.EMAIL;
            paramData[5].Value = mdlSysUser.MOBILE;
            paramData[6].Value = mdlSysUser.PWD;
            paramData[7].Value = mdlSysUser.INITIDENT;
            paramData[8].Value = mdlSysUser.DATASOURCE;
            paramData[9].Value = mdlSysUser.CREATEDATE;
            paramData[10].Value = mdlSysUser.STATE;

            try
            {
                if (OrclHelper.SSOWrite.ExecuteNonQuery(sqlData, CommandType.Text, paramData) > 0)
                {
                    return paramData[11].Value.ToInt(0);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "杨伟强", "新增用户失败");
                return 0;
            }
        }

        /// <summary>
        /// 根据SNO登录
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public DataTable LoginBySno(string sno)
        {
            string sqlData = "Select PWD from UserInfo where LoginType='sno' and LoginID=:SNO";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据AccountId登录
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public DataTable LoginByAccId(string accId)
        {
            string sqlData = "Select PWD from UserInfo where LoginType='accid' and LoginID=:ACCOUNTID";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("ACCOUNTID",OracleType.VarChar)
            };
            paramData[0].Value = accId;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据Email登录
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataTable LoginByEmail(string email)
        {
            string sqlData = "Select PWD from UserInfo where EMAIL=:EMAIL";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("EMAIL",OracleType.VarChar)
            };
            paramData[0].Value = email;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据Mobile登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public DataTable LoginByMobile(string mobile)
        {
            string sqlData = "Select PWD from UserInfo where MOBILE=:MOBILE";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("MOBILE",OracleType.VarChar)
            };
            paramData[0].Value = mobile;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 查找指定SNO是否存在
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public bool IsExistsSno(string sno)
        {
            string sqlData = "Select Count(1) from UserInfo where SNO=:SNO";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            object objC = OrclHelper.SSORead.ExecuteScalar(sqlData, CommandType.Text, paramData);
            if ((objC == null) || String.IsNullOrEmpty(objC.ToString()) || (objC.ToString() == "0"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查找指定AccountId是否存在
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool IsExistsAccId(string accountId)
        {
            string sqlData = "Select Count(1) from UserInfo where AccountId=:AccountId";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("AccountId",OracleType.VarChar)
            };
            paramData[0].Value = accountId;

            object objC = OrclHelper.SSORead.ExecuteScalar(sqlData, CommandType.Text, paramData);
            if ((objC == null) || String.IsNullOrEmpty(objC.ToString()) || (objC.ToString() == "0"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查找指定MercID是否存在
        /// </summary>
        /// <param name="mercId"></param>
        /// <returns></returns>
        public bool IsExistsMercId(string mercId)
        {
            string sqlData = "Select Count(1) from UserInfo where MercID=:MercID";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("MercID",OracleType.VarChar)
            };
            paramData[0].Value = mercId;

            object objC = OrclHelper.SSORead.ExecuteScalar(sqlData, CommandType.Text, paramData);
            if ((objC == null) || String.IsNullOrEmpty(objC.ToString()) || (objC.ToString() == "0"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据SNO查找用户信息
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public DataTable GetBySno(string sno)
        {
            string sqlData = "Select UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,InitIdent,DataSource,CreateDate,State from UserInfo where LoginType='sno' and LoginID=:SNO";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("SNO",OracleType.VarChar)
            };
            paramData[0].Value = sno;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据AccountId查找用户信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable GetByAccId(string accountId)
        {
            string sqlData = "Select UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,InitIdent,DataSource,CreateDate,State from UserInfo where LoginType='accid' and LoginID=:ACCOUNTID";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("ACCOUNTID",OracleType.VarChar)
            };
            paramData[0].Value = accountId;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据Email查找用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataTable GetByEmail(string email)
        {
            string sqlData = "Select UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,InitIdent,DataSource,CreateDate,State from UserInfo where Email=:Email";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("Email",OracleType.VarChar)
            };
            paramData[0].Value = email;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据Mobile查找用户信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public DataTable GetByMobile(string mobile)
        {
            string sqlData = "Select UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,InitIdent,DataSource,CreateDate,State from UserInfo where Mobile=:Mobile";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("Mobile",OracleType.VarChar)
            };
            paramData[0].Value = mobile;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }

        /// <summary>
        /// 根据MercID查找用户信息
        /// </summary>
        /// <param name="mercId"></param>
        /// <returns></returns>
        public DataTable GetByMercId(string mercId)
        {
            string sqlData = "Select UserID,UniteID,LoginType,LoginID,Name,Email,Mobile,InitIdent,DataSource,CreateDate,State from UserInfo where LoginType='mercid' and LoginID=:MERCID";
            OracleParameter[] paramData = new OracleParameter[]
            {
                new OracleParameter("MERCID",OracleType.VarChar)
            };
            paramData[0].Value = mercId;

            DataSet dsData = OrclHelper.SSORead.ExecuteDataSet(sqlData, CommandType.Text, paramData);
            if ((dsData == null) || (dsData.Tables.Count == 0) || (dsData.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            return dsData.Tables[0];
        }
    }
}
