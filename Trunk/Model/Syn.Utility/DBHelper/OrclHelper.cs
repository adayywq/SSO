using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Common;

namespace Syn.Utility.DBHelper
{ 
    /// <summary>
    /// Oracle���ݷ��ʻ�����
    /// </summary>
    public class OrclHelper
    {
        protected String _ConnectString = ConfigurationManager.ConnectionStrings["SSOConn"].ConnectionString;

        public OrclHelper() { }
        
        public OrclHelper(string connStr)
        {
            if (!String.IsNullOrEmpty(connStr.Trim()))
            {
                _ConnectString = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }
        }

        #region ����������

        private static object lockObject = new object();
        private static volatile Syn.Utility.DBHelper.OrclHelper _SSORead;
        private static volatile Syn.Utility.DBHelper.OrclHelper _SSOWrite;
        private static volatile Syn.Utility.DBHelper.OrclHelper _IdentRead;
        private static volatile Syn.Utility.DBHelper.OrclHelper _IdentWrite;

        /// <summary>
        /// ���ϵͳ������
        /// </summary>
        public static Syn.Utility.DBHelper.OrclHelper SSORead
        {
            get
            {
                if (_SSORead == null)
                {
                    lock (lockObject)
                    {
                        if (_SSORead == null)
                        {
                            _SSORead = new Syn.Utility.DBHelper.OrclHelper("SSOConn");//��ȡ�����ַ���
                        }
                    }
                }
                return _SSORead;
            }
        }

        /// <summary>
        /// ���ϵͳд����
        /// </summary>
        public static Syn.Utility.DBHelper.OrclHelper SSOWrite
        {
            get
            {
                if (_SSOWrite == null)
                {
                    lock (lockObject)
                    {
                        if (_SSOWrite == null)
                        {
                            _SSOWrite = new Syn.Utility.DBHelper.OrclHelper("SSOConn");//��ȡ�����ַ���
                        }
                    }
                }
                return _SSOWrite;
            }
        }

        /// <summary>
        /// һ��ͨ��ݿ������
        /// </summary>
        public static Syn.Utility.DBHelper.OrclHelper IdenRead
        {
            get
            {
                if (_IdentRead == null)
                {
                    lock (lockObject)
                    {
                        if (_IdentRead == null)
                        {
                            _IdentRead = new Syn.Utility.DBHelper.OrclHelper("IdentConn");//��ȡ�����ַ���
                        }
                    }
                }
                return _IdentRead;
            }
        }

        /// <summary>
        /// һ��ͨ��ݿ�д����
        /// </summary>
        public static Syn.Utility.DBHelper.OrclHelper IdentWrite
        {
            get
            {
                if (_IdentWrite == null)
                {
                    lock (lockObject)
                    {
                        if (_IdentWrite == null)
                        {
                            _IdentWrite = new Syn.Utility.DBHelper.OrclHelper("IdentConn");//��ȡ�����ַ���
                        }
                    }
                }
                return _IdentWrite;
            }
        }

        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// ִ�д洢���̻�SQL���
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>��Ӱ������</returns>
        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL���
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdParams">����</param>
        /// <returns>��Ӱ������</returns>
        public int ExecuteNonQuery(string sql, params OracleParameter[] cmdParams)
        {
            return ExecuteNonQuery(sql, CommandType.Text, cmdParams);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL���
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdType">�������</param>
        /// <param name="cmdParams">����</param>
        /// <returns>��Ӱ������</returns>
        public int ExecuteNonQuery(string sql, CommandType cmdType, params OracleParameter[] cmdParams)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            try
            {
                BuildCommand(cmd, conn, null, cmdType, sql, cmdParams);
                //cmd.Parameters.Add(new OracleParameter("ReturnValue", OracleType.Number, 4, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                //int rowsAffected = Convert.ToInt32(cmd.Parameters["ReturnValue"].Value);
                int rows = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rows;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢������������
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>������������</returns>
        public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢������������
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdParams">����</param>
        /// <returns>������������</returns>
        public object ExecuteScalar(string sql, params OracleParameter[] cmdParams)
        {
            return ExecuteScalar(sql, CommandType.Text, cmdParams);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢������������
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdType">�������</param>
        /// <param name="cmdParams">����</param>
        /// <returns>������������</returns>
        public object ExecuteScalar(string sql, CommandType cmdType, params OracleParameter[] cmdParams)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();                
            try
            {
                BuildCommand(cmd, conn, null, cmdType, sql, cmdParams);
                object obj = cmd.ExecuteScalar();                        
                cmd.Parameters.Clear();
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (System.Data.OracleClient.OracleException e)
            {
                conn.Close();
                conn.Dispose();
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }

        #endregion

        #region ExecuteReader
        
        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataReader
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>����DataReader</returns>
        public OracleDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataReader
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdParams">����</param>
        /// <returns>����DataReader</returns>
        public OracleDataReader ExecuteReader(string sql, params OracleParameter[] cmdParams)
        {
            return ExecuteReader(sql, CommandType.Text, cmdParams);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataReader
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdType">�������</param>
        /// <param name="cmdParams">����</param>
        /// <returns>����DataReader</returns>
        public OracleDataReader ExecuteReader(string sql, CommandType cmdType, params OracleParameter[] cmdParams)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            try
            {
                BuildCommand(cmd, conn, null, cmdType, sql, cmdParams);
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);                
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.OracleClient.OracleException e)
            {
                throw new Exception(e.Message);
            }
            //finally
            //{
            //    conn.Close();
            //    cmd.Dispose();
            //}
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataSet
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>����DataSet</returns>
        public DataSet ExecuteDataSet(string sql)
        {
            return ExecuteDataSet(sql, CommandType.Text, null);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataSet
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdParms">����</param>
        /// <returns>����DataSet</returns>
        public DataSet ExecuteDataSet(string sql, params OracleParameter[] cmdParms)
        {
            return ExecuteDataSet(sql, CommandType.Text, cmdParms);
        }

        /// <summary>
        /// ִ�д洢���̻�SQL��䲢����DataSet
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="cmdType">�������</param>
        /// <param name="cmdParms">����</param>
        /// <returns>����DataSet</returns>
        public DataSet ExecuteDataSet(string sql, CommandType cmdType, params OracleParameter[] cmdParms)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter oda = new OracleDataAdapter(cmd);                
            try
            {
                BuildCommand(cmd, conn, null, cmdType, sql, cmdParms);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
            catch (OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                oda.Dispose();
            }
        }

        #endregion      

        #region ����һ������

        /// <summary>
        /// ��������ִ��һ��SQL���
        /// </summary>
        /// <param name="arrSql">SQL�������</param>
        /// <returns>�ɹ����</returns>	
        public Boolean ExecuteTransSql(ArrayList arrSql)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                for (int i = 0; i < arrSql.Count; i++)
                {
                    BuildCommand(cmd, conn, trans, CommandType.Text, arrSql[i].ToString(), null);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return true;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                trans.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                trans.Dispose();
            }
        }
        public Boolean ExecuteTransSql(List<string> arrSql, List<OracleParameter[]> lstpara)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                for (int i = 0; i < arrSql.Count; i++)
                {
                    BuildCommand(cmd, conn, trans, CommandType.Text, arrSql[i], lstpara[i]);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return true;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                trans.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                trans.Dispose();
            }
        }
        /// <summary>
        /// ��������ִ��һ��SQL���
        /// </summary>
        /// <param name="htSqlParam">��ϣ��(keyΪSQL��䣬valueΪ������OracleParameter[])</param>
        /// <returns>�ɹ����</returns>
        public Boolean ExecuteTransSql(Hashtable htSqlParam)
        {
            OracleConnection conn = new OracleConnection(_ConnectString);
            OracleCommand cmd = new OracleCommand();
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (DictionaryEntry myDE in htSqlParam)
                {
                    string cmdText = myDE.Key.ToString();
                    OracleParameter[] cmdParams = (OracleParameter[])myDE.Value;
                    BuildCommand(cmd, conn, trans, CommandType.Text, cmdText, cmdParams);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                trans.Commit();
                return true;
            }
            catch (OracleException ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                trans.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// ���� OracleCommand ����
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        private void BuildCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParams)
        {
            cmd.Parameters.Clear();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParams != null)
            {
                foreach (OracleParameter parm in cmdParams)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
