using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;

using Syn.Utility;
using Syn.Utility.DBHelper;

namespace Syn.Special
{
    public class Pagination
    {
       private static OrclHelper OracleRead = OrclHelper.SSORead;
       private static OrclHelper OracleWrite = OrclHelper.SSOWrite;
       public DbDataReader pagefenye(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount)
       {
           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;
           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               sqlUser = "SELECT " + FieldCount + " FROM (SELECT tt." + FieldCount + ", ROWNUM AS rowno FROM (  SELECT t." + FieldCount + " FROM " + tableName + " t  " +where +" "+ sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;
           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {

            };

           OracleDataReader dsOrclUser = null;

           try
           {
              dsOrclUser = OracleRead.ExecuteReader(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null)
           {
               return dsOrclUser;
           }
           else
           {
               return null;
           }
       }

       /// <summary>
       ///  
       /// </summary>
       /// <param name="tableName">表名,多红表是请使用 tA a inner join tB b On a.AID = b.AID</param>
       /// <param name="pK">主键，可以带表头 a.AID</param>
       /// <param name="sortField">排序字段</param>
       /// <param name="curr">开始页码</param>
       /// <param name="pagesize">页大小</param>
       /// <param name="FieldCount">读取字段</param>
       /// <param name="where">Where条件</param>
       /// <param name="group">分组</param>
       /// <param name="isCount">是否获得总记录数</param>
       /// <returns></returns>
       public DataTable pagefenyeset(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount)
       {

           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= "+ pagesize +" and  n >=" + curr;

           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               curr = START_ID;
               pagesize = END_ID;
               sqlUser = "SELECT " + FieldCount + " FROM (SELECT tt." + FieldCount + ", ROWNUM AS rowno FROM (  SELECT t." + FieldCount + " FROM " + tableName + " t  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;

           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {
                new OracleParameter("tableName",OracleType.VarChar)
            };
           paramUser[0].Value = tableName;
           
           DataSet dsOrclUser = null;
           
           try
           {
               
               dsOrclUser = OracleRead.ExecuteDataSet(sqlUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null && dsOrclUser.Tables.Count > 0)
           {
               return dsOrclUser.Tables[0];
           }
           else
           {
               return null;
           }
       }

       public DataSet fenyedateset(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount)
       {

           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;
           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;

               sqlUser = "SELECT " + FieldCount + " FROM (SELECT tt." + FieldCount + ", ROWNUM AS rowno FROM (  SELECT t." + FieldCount + " FROM " + tableName + " t  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;
           }
           DbParameter[] paramUser = new DbParameter[] 
            {
               
            };

           DataSet dsOrclUser = null;

           try
           {
               dsOrclUser = OracleRead.ExecuteDataSet(sqlUser); //DBOperator.SPRead.ExecuteDataSet(sqlUser);
               // dsOrclUser = DBOperator.SPRead.ExecuteDataSet(sqlUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null && dsOrclUser.Tables.Count > 0)
           {
               return dsOrclUser;
           }
           else
           {
               return null;
           }
       }

       /// <summary>
       /// 根据表名 字段 传入参数获取总行数
       /// </summary>
       /// <param name="tableName">表名</param>
       /// <param name="id">主键字段</param>
       /// <param name="where">where条件</param>
       /// <returns></returns>
       public int fenyecount(string tableName, string id, string where)
       {

           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           string sqlUser = "";

           sqlUser = "select count("+id+") as id from " +tableName +where;
           OracleParameter[] paramUser = new OracleParameter[] 
            {};
           OracleDataReader dsOrclUser = null;
           try
           {
               dsOrclUser = OracleRead.ExecuteReader(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页统计总行数");
           }
           int count=0;
           if (dsOrclUser != null)
           {
               while (dsOrclUser.Read())
               {
                   count = Int32.Parse(dsOrclUser["id"].ToString());
               }
               return count;
           }
           else
           {
               return 0;
           }

       }

       /// <summary>
       /// 根据表名 字段 传入参数获取总行数
       /// </summary>
       /// <param name="tableName">表名</param>
       /// <param name="id">主键字段</param>
       /// <param name="where">where条件</param>
       /// <returns></returns>
       public int fenyecountfi(string tableName, string id, string where)
       {

           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           string sqlUser = "";

           sqlUser = "select count(" + id + ") as id from " + tableName + where;
           OracleParameter[] paramUser = new OracleParameter[] { };
           DataSet dsOrclUser = null;
           try
           {
               dsOrclUser = OracleRead.ExecuteDataSet(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页统计总行数");
           }
           int count = 0;
            if (dsOrclUser.Tables[0].Rows.Count >0)
            {
               count = Int32.Parse(dsOrclUser.Tables[0].Rows[0]["id"].ToString());
               return count;
           }
           else
           {
               return 0;
           }

       }

       public DataTable pagefenyesetS(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount, string FieldCountf)
       {

           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               // sqlUser = "select :FieldCount from ( select :FieldCount , rownum as n from :tableName   where rownum  <= :pagesize where :sortField   :order  ) where n >= :curr";
               sqlUser = "select " + FieldCountf + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;
               // sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + "  where rownum  <= " + pagesize + where + sortField + " " + order + " ) where n >=" + curr;
           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               curr = START_ID;
               pagesize = END_ID;
               // sqlUser = "select :FieldCount from ( select :FieldCount , rownum as n from :tableName  where rownum  <= :pagesize  where  :sortField :order  ) where n between  :curr  and :pagesize";
               sqlUser = "SELECT " + FieldCountf + " FROM (SELECT " + FieldCountf + ", ROWNUM AS rowno FROM (  SELECT " + FieldCount + " FROM " + tableName + "  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;

           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {
                new OracleParameter("tableName",tableName)
            };

           DataSet dsOrclUser = null;

           try
           {

               dsOrclUser = OracleRead.ExecuteDataSet(sqlUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null && dsOrclUser.Tables.Count > 0)
           {
               return dsOrclUser.Tables[0];
           }
           else
           {
               return null;
           }
       }

       public DbDataReader pagefenyes(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount, string FieldCountf)
       {
           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               // sqlUser = "select :FieldCount from ( select :FieldCount , rownum as n from :tableName   where rownum  <= :pagesize where :sortField   :order  ) where n >= :curr";
               sqlUser = "select " + FieldCountf + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;
               // sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + "  where rownum  <= " + pagesize + where + sortField + " " + order + " ) where n >=" + curr;
           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               curr = START_ID;
               pagesize = END_ID;
               // sqlUser = "select :FieldCount from ( select :FieldCount , rownum as n from :tableName  where rownum  <= :pagesize  where  :sortField :order  ) where n between  :curr  and :pagesize";
               sqlUser = "SELECT " + FieldCountf + " FROM (SELECT " + FieldCountf + ", ROWNUM AS rowno FROM (  SELECT " + FieldCount + " FROM " + tableName + "  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;

           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {
            };

           DbDataReader dsOrclUser = null;

           try
           {
               dsOrclUser = OracleRead.ExecuteReader(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null)
           {
               return dsOrclUser;
           }
           else
           {
               return null;
           }
       }


       /// <summary>
       ///  查询一卡通数据库
       /// </summary>
       /// <param name="tableName">表名,多红表是请使用 tA a inner join tB b On a.AID = b.AID</param>
       /// <param name="pK">主键，可以带表头 a.AID</param>
       /// <param name="sortField">排序字段</param>
       /// <param name="curr">开始页码</param>
       /// <param name="pagesize">页大小</param>
       /// <param name="FieldCount">读取字段</param>
       /// <param name="where">Where条件</param>
       /// <param name="group">分组</param>
       /// <param name="isCount">是否获得总记录数</param>
       /// <returns></returns>
       public DataTable pagefenyesetykt(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount)
       {

           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;

           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               curr = START_ID;
               pagesize = END_ID;
               sqlUser = "SELECT " + FieldCount + " FROM (SELECT tt." + FieldCount + ", ROWNUM AS rowno FROM (  SELECT t." + FieldCount + " FROM " + tableName + " t  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;

           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {
                new OracleParameter("tableName",tableName)
                //OracleWrite.MakeInParam("tableName",DbType.String,-1,tableName)
            };

           DataSet dsOrclUser = null;

           try
           {
              dsOrclUser= OrclHelper.IdenRead.ExecuteDataSet(sqlUser);
               
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null && dsOrclUser.Tables.Count > 0)
           {
               return dsOrclUser.Tables[0];
           }
           else
           {
               return null;
           }
       }


       public int fenyecountykt(string tableName, string id, string where)
       {

           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           string sqlUser = "";

           sqlUser = "select count(" + id + ") as id from " + tableName + where;
           OracleParameter[] paramUser = new OracleParameter[] { };
           OracleDataReader dsOrclUser = null;
           try
           {
               dsOrclUser = OrclHelper.IdenRead.ExecuteReader(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页统计总行数");
           }
           int count = 0;
           if (dsOrclUser != null)
           {
               while (dsOrclUser.Read())
               {
                   count = Int32.Parse(dsOrclUser["id"].ToString());
               }
               return count;
           }
           else
           {
               return 0;
           }

       }

       public DbDataReader pagefenyesykt(string tableName, string pK, string sortField, int curr, int pagesize, string FieldCount, string where, string order, string isCount)
       {
           if (curr < 1)
           {
               curr = 1;//判断 如果开始页码小于1 赋值为1
           }
           if (where.Trim() != "")
           {
               where = " where " + where; ;
           }
           if (sortField.Trim() != "")
           {
               sortField = " order by " + sortField;
           }
           string sqlUser = "";
           if (curr == 1)
           {
               //首页优化
               sqlUser = "select " + FieldCount + " from ( select " + FieldCount + " , rownum as n from " + tableName + where + sortField + " " + order + " ) where  rownum  <= " + pagesize + " and  n >=" + curr;
           }
           else
           {
               int START_ID = 0;
               int END_ID = 0;
               START_ID = (curr - 1) * pagesize + 1; //(curr * pagesize) / (pagesize) + 1;
               END_ID = curr * pagesize;
               curr = START_ID;
               pagesize = END_ID;
               sqlUser = "SELECT " + FieldCount + " FROM (SELECT tt." + FieldCount + ", ROWNUM AS rowno FROM (  SELECT t." + FieldCount + " FROM " + tableName + " t  " + where + " " + sortField + " " + order + ") tt WHERE ROWNUM <= " + END_ID + ") table_alias WHERE table_alias.rowno >= " + START_ID;

           }
           OracleParameter[] paramUser = new OracleParameter[] 
            {
            };

           DbDataReader dsOrclUser = null;

           try
           {
               dsOrclUser = OrclHelper.IdenRead.ExecuteReader(sqlUser, CommandType.Text, paramUser);
           }
           catch (Exception ex)
           {
               Log.WriteErrorLog(ex, "张晶", "分页");
           }
           if (dsOrclUser != null)
           {
               return dsOrclUser;
           }
           else
           {
               return null;
           }
       }
    }
}
