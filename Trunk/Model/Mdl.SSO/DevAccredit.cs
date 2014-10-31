using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syn.Utility.DBHelper;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace Mdl.SSO
{
    /// <summary>
    /// 关于开发授权的操作
    /// </summary>
    public class DevAccredit
    {
        /// <summary>
        /// 获取开发者列表
        /// </summary>
        /// <param name="curPage">[in,out]当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalCount">[out]总记录数</param>
        /// <returns></returns>
        public DataTable SelectAppList(ref int curPage, int pageSize, out int totalCount)
        {
            DataTable dt = null;

            Syn.Utility.Function.fenye fenyepage = new Syn.Utility.Function.fenye();

            string where = " ";//条件语句部分

            totalCount = fenyepage.fenyecount("DEVELOPER", "DEVID", where);//总页数

            int totalPage = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
            if (curPage > totalPage) curPage = totalPage;

            //调用分页方法
            dt = fenyepage.pagefenyeset("DEVELOPER", "DEVID", "DEVID", curPage, pageSize, " DEVID,ACCCODE,DEVNAME,DEVCODE,LINKMAN,MOBILE,STATE,CALLBACKURL,LOGOUTURL,EMAIL ", where, " desc ", "");

            return dt;
        }

        /// <summary>
        /// 获取开发者列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAppList2()
        {
            string sql = "select DEVID,ACCCODE,DEVNAME,DEVCODE,LINKMAN,MOBILE,STATE,callbackurl,logouturl from DEVELOPER order by DEVID";

            try
            {
                DataSet ds = OrclHelper.SSORead.ExecuteDataSet(sql, CommandType.Text, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "获取开发者列表2");
                return null;
            }
        }

        /// <summary>
        /// 添加开发者
        /// </summary>
        /// <param name="dev">开发者信息</param>
        /// <param name="devId">[out]新增开发id，如果添加字典失败则为-1</param>
        /// <returns>0成功，-1表示出错，1表示授权编码重复，2表示开发编码重复，3表示开发者重复，4表示联系人重复，5表示联系电话重复，6表示email重复</returns>
        public int InsertDevAccredit(Mdl.Entity.DEVELOPER dev, out int devId)
        {

            string sql = "begin "
                + "select count(ACCCODE) into :ACCCODENUM from DEVELOPER where ACCCODE=:ACCCODE;"
                + "select count(DEVNAME) into :DEVNAMENUM from DEVELOPER where DEVNAME=:DEVNAME;"
                + "select count(DEVCODE) into :DEVCODENUM from DEVELOPER where DEVCODE=:DEVCODE;"
                + "select count(LINKMAN) into :LINKMANNUM from DEVELOPER where LINKMAN=:LINKMAN;"
                + "select count(MOBILE) into :MOBILENUM from DEVELOPER where MOBILE=:MOBILE;"
                + "select count(EMAIL) into :EMAILNUM from DEVELOPER where EMAIL=:EMAIL;"
                + "if :ACCCODENUM=0 and :DEVCODENUM=0 and :DEVNAMENUM=0 and :LINKMANNUM=0 and :MOBILENUM=0 and :EMAILNUM=0 then "
                + ":DEVID:= DEVID.NEXTVAL; "
                + "insert into DEVELOPER(DEVID,ACCCODE,DEVNAME,DEVCODE,LINKMAN,MOBILE,EMAIL,MEMO,CREATOR,CREATDATE,MODIFIER,MODIFYDATE,STATE,siteurl,callbackurl,logouturl) values(:DEVID,:ACCCODE,:DEVNAME,:DEVCODE,:LINKMAN,:MOBILE,:EMAIL,:MEMO,:CREATOR,:CREATDATE,:MODIFIER,:MODIFYDATE,:STATE,:siteurl,:callbackurl,:logouturl);"
                + " end if;"
                + " end;";
            OracleParameter[] paras = new OracleParameter[] { 
                new OracleParameter("ACCCODE",dev.ACCCODE),
                new OracleParameter("DEVNAME",dev.DEVNAME),
                new OracleParameter("DEVCODE", dev.DEVCODE),
                new OracleParameter("LINKMAN",dev.LINKMAN),
                new OracleParameter("MOBILE",dev.MOBILE),
                new OracleParameter("EMAIL",dev.EMAIL),
                new OracleParameter("siteurl",dev.SITEURL),
                new OracleParameter("callbackurl",dev.CALLBACKURL),
                new OracleParameter("MEMO",dev.MEMO),
                new OracleParameter("CREATOR",dev.CREATOR),
                new OracleParameter("CREATDATE",dev.CREATDATE),
                new OracleParameter("MODIFIER",dev.MODIFIER),
                new OracleParameter("MODIFYDATE",dev.MODIFYDATE),
                new OracleParameter("STATE",dev.STATE),
                new OracleParameter("logouturl",dev.LOGOUTURL),
                new OracleParameter("ACCCODENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("DEVCODENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("DEVNAMENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("LINKMANNUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("MOBILENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("EMAILNUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("DEVID",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,"")
                //OrclHelper.SSORead.MakeOutParam("ACCCODENUM",DbType.String, 50), //与此授权编码同名的编码个数
                //OrclHelper.SSORead.MakeOutParam("DEVCODENUM",DbType.String, 50), //与此开发编码同名的编码个数
                //OrclHelper.SSORead.MakeOutParam("DEVNAMENUM",DbType.String, 50), //与此开发者同名的个数
                //OrclHelper.SSORead.MakeOutParam("LINKMANNUM",DbType.String, 50), //与此联系人同名的个数
                //OrclHelper.SSORead.MakeOutParam("MOBILENUM",DbType.String, 50), //与此联系电话同名的个数
                //OrclHelper.SSORead.MakeOutParam("EMAILNUM",DbType.String, 50), //与此电子邮箱同名的个数
                //OrclHelper.SSORead.MakeOutParam("DEVID",DbType.String, 50) //新增开发id
            };
            try
            {
                OrclHelper.SSOWrite.ExecuteNonQuery(sql, CommandType.Text, paras);
                if (Convert.ToInt32(paras[15].Value) != 0) //授权编码
                {
                    devId = -1;
                    return 1;
                }
                else if (Convert.ToInt32(paras[17].Value) != 0) //开发者
                {
                    devId = -1;
                    return 3;
                }
                else if (Convert.ToInt32(paras[16].Value) != 0) //开发编码
                {
                    devId = -1;
                    return 2;
                }
                else if (Convert.ToInt32(paras[18].Value) != 0) //联系人
                {
                    devId = -1;
                    return 4;
                }
                else if (Convert.ToInt32(paras[19].Value) != 0) //联系电话
                {
                    devId = -1;
                    return 5;
                }
                else if (Convert.ToInt32(paras[20].Value) != 0) //电子邮箱
                {
                    devId = -1;
                    return 6;
                }
                else
                {
                    devId = Convert.ToInt32(paras[21].Value);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "添加开发者");
                devId = -1;
                return -1;
            }
        }

        /// <summary>
        /// 编辑开发者
        /// </summary>
        /// <param name="dev">开发者信息</param>
        /// <returns>0成功，-1表示出错，2表示开发编码重复，3表示开发者重复，4表示联系人重复，5表示联系电话重复，6表示email重复</returns>
        public int UpdateDevAccredit(Mdl.Entity.DEVELOPER dev)
        {

            string sql = "begin "
                + "select count(DEVCODE) into :DEVCODENUM from DEVELOPER where DEVCODE=:DEVCODE and DEVID!=:DEVID;"
                + "select count(DEVNAME) into :DEVNAMENUM from DEVELOPER where DEVNAME=:DEVNAME and DEVID!=:DEVID;"
                + "select count(LINKMAN) into :LINKMANNUM from DEVELOPER where LINKMAN=:LINKMAN and DEVID!=:DEVID;"
                + "select count(MOBILE) into :MOBILENUM from DEVELOPER where MOBILE=:MOBILE and DEVID!=:DEVID;"
                + "select count(EMAIL) into :EMAILNUM from DEVELOPER where EMAIL=:EMAIL and DEVID!=:DEVID;"
                + "select count(DEVID) into :DEVIDNUM from DEVELOPER where DEVID=:DEVID;"
                + "if :DEVIDNUM!=0 then " //如果数据已不存在
                + "if :DEVCODENUM=0 and :DEVNAMENUM=0 and :LINKMANNUM=0 and :MOBILENUM=0 and :EMAILNUM=0 then "
                + "update DEVELOPER set DEVNAME=:DEVNAME,DEVCODE=:DEVCODE,LINKMAN=:LINKMAN,MOBILE=:MOBILE,EMAIL=:EMAIL,MEMO=:MEMO,MODIFIER=:MODIFIER,MODIFYDATE=:MODIFYDATE,SITEURL=:SITEURL,CALLBACKURL=:CALLBACKURL,LOGOUTURL=:LOGOUTURL where DEVID=:DEVID;"
                + " end if;"
                + " end if;"
                + " end;";
            OracleParameter[] paras = new OracleParameter[] { 
                new OracleParameter("DEVID",dev.DEVID),
                new OracleParameter("DEVNAME",dev.DEVNAME),
                new OracleParameter("DEVCODE", dev.DEVCODE),
                new OracleParameter("LINKMAN",dev.LINKMAN),
                new OracleParameter("MOBILE",dev.MOBILE),
                new OracleParameter("EMAIL",dev.EMAIL),
                new OracleParameter("CALLBACKURL",dev.CALLBACKURL),
                new OracleParameter("SITEURL",dev.SITEURL),
                new OracleParameter("MEMO",dev.MEMO),
                new OracleParameter("MODIFIER",dev.MODIFIER),
                new OracleParameter("MODIFYDATE",dev.MODIFYDATE),
                new OracleParameter("LOGOUTURL",dev.LOGOUTURL),
                new OracleParameter("DEVCODENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("DEVNAMENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("LINKMANNUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("MOBILENUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("EMAILNUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,""),
                new OracleParameter("DEVIDNUM",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,"")
                //new OracleParameter("DEVID",OracleType.NVarChar,50,ParameterDirection.Output,"",DataRowVersion.Default,true,"")//,
                //DBOperator.SPWrite.MakeOutParam("DEVCODENUM",DbType.String, 50), //与此开发编码同名的编码个数
                //DBOperator.SPWrite.MakeOutParam("DEVNAMENUM",DbType.String, 50), //与此开发者同名的个数
                //DBOperator.SPWrite.MakeOutParam("LINKMANNUM",DbType.String, 50), //与此联系人同名的个数
                //DBOperator.SPWrite.MakeOutParam("MOBILENUM",DbType.String, 50), //与此联系电话同名的个数
                //DBOperator.SPWrite.MakeOutParam("EMAILNUM",DbType.String, 50), //与此电子邮箱同名的个数
                //DBOperator.SPWrite.MakeOutParam("DEVIDNUM",DbType.String, 50) 
            };
            try
            {
                OrclHelper.SSOWrite.ExecuteNonQuery(sql, CommandType.Text, paras);
                if (Convert.ToInt32(paras[17].Value) == 0) //数据不存在
                {
                    return 7;
                }
                else if (Convert.ToInt32(paras[12].Value) != 0) //开发编码
                {
                    return 2;
                }
                else if (Convert.ToInt32(paras[13].Value) != 0) //开发者
                {
                    return 3;
                }
                else if (Convert.ToInt32(paras[14].Value) != 0) //联系人
                {
                    return 4;
                }
                else if (Convert.ToInt32(paras[15].Value) != 0) //联系电话
                {
                    return 5;
                }
                else if (Convert.ToInt32(paras[16].Value) != 0) //电子邮箱
                {
                    return 6;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "编辑开发者");
                return -1;
            }
        }

        /// <summary>
        /// 删除开发者
        /// </summary>
        /// <param name="devId">开发id</param>
        /// <returns>成功返回0, 数据不存在返回1，出错返回-1</returns>
        public int DeleteDevAccredit(int devId)
        {
            string sql = "delete DEVELOPER where DEVID=:DEVID";
            string sql1 = "delete devappr where DEVID=:DEVID";

            OracleParameter[] paras = new OracleParameter[]{  
                new OracleParameter("DEVID",devId)
            };
            try
            {
                OrclHelper.SSOWrite.ExecuteTransSql(new List<string> { sql, sql1 }, new List<OracleParameter[]> { paras, paras });
                if (Convert.ToInt32(paras[1].Value) == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "刘金保", "删除开发者");
                return -1;
            }
        }

        /// <summary>
        /// 根据开发Id获取开发者信息
        /// </summary>
        /// <param name="devId">开发Id</param>
        /// <returns>返回一条授权应用信息</returns>
        public Mdl.Entity.DEVELOPER SelectDevAccreditByDevId(int devId)
        {
            string sql = " select DEVID,ACCCODE,DEVNAME,DEVCODE,LINKMAN,MOBILE,EMAIL,MEMO,CREATOR,CREATDATE,MODIFIER,MODIFYDATE,STATE,siteurl,callbackurl,logouturl from DEVELOPER where DEVID=:DEVID";

            OracleParameter[] param = new OracleParameter[]
            {
                new OracleParameter("DEVID",devId)  
            };
            Mdl.Entity.DEVELOPER aa = new Mdl.Entity.DEVELOPER();
            try
            {
                DataSet ds = OrclHelper.SSORead.ExecuteDataSet(sql, CommandType.Text, param);
                DataTable dt = (ds == null || ds.Tables.Count == 0) ? null : ds.Tables[0];
                if (dt != null && dt.Rows.Count != 0 )
                {
                    aa.DEVID = Convert.ToInt32(dt.Rows[0]["DEVID"].ToString());
                    aa.ACCCODE = dt.Rows[0]["ACCCODE"].ToString();
                    aa.DEVNAME = dt.Rows[0]["DEVNAME"].ToString();
                    aa.DEVCODE = dt.Rows[0]["DEVCODE"].ToString();
                    aa.LINKMAN = dt.Rows[0]["LINKMAN"].ToString();
                    aa.MOBILE = dt.Rows[0]["MOBILE"].ToString();
                    aa.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                    aa.LOGOUTURL = dt.Rows[0]["LOGOUTURL"].ToString();
                    aa.SITEURL = dt.Rows[0]["SITEURL"].ToString();
                    aa.MEMO = dt.Rows[0]["MEMO"].ToString();
                    aa.CREATOR = Convert.ToInt32(dt.Rows[0]["CREATOR"].ToString());
                    aa.CREATDATE = Convert.ToDateTime(dt.Rows[0]["CREATDATE"]);
                    aa.MODIFIER = Convert.ToInt32(dt.Rows[0]["MODIFIER"].ToString());
                    aa.MODIFYDATE = Convert.ToDateTime(dt.Rows[0]["MODIFYDATE"].ToString());
                    aa.STATE = Convert.ToInt32(dt.Rows[0]["STATE"].ToString());
                    aa.CALLBACKURL = dt.Rows[0]["CALLBACKURL"].ToString();
                    return aa;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "根据开发Id获取开发者信息");
                return null;
            }           
        }

        /// <summary>
        /// 修改开发者状态
        /// </summary>
        /// <param name="devId">开发id</param>
        /// <param name="state">指定状态</param>
        /// <returns>成功返回true</returns>
        public int UpdateState(int devId, int state)
        {
            string sql = "update DEVELOPER set STATE=:STATE where DEVID=:DEVID";
            OracleParameter[] paras = new OracleParameter[] { 
                new OracleParameter("DEVID",devId),
                new OracleParameter("STATE",state)
                
            };
            try
            {
                return OrclHelper.SSOWrite.ExecuteNonQuery(sql, CommandType.Text, paras);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "修改开发者状态");
                return 0;
            }
        }


        /// <summary>
        /// 返回授权状态
        /// </summary>
        /// <param name="LifeNavID"></param>
        /// <returns></returns>
        public string Appnamestring(string devname, string appcode)
        {
            string sqlInsert = "";
            if (appcode == "")
            {
                 sqlInsert = " select Appname,appcode from APPLICATION where appid in( select Appid from DEVAPPR  D inner join DEVELOPER DE on D.DEVID = De.Devid where devname =:devname ) and appid in( select appid from APPLICATION where pid in ( select appid from APPLICATION where appcode=:appcode))";
            }
            else
            {
                sqlInsert = " select Appname,appcode from APPLICATION where appid in( select Appid from DEVAPPR  D inner join DEVELOPER DE on D.DEVID = De.Devid where devname =:devname ) and pid > 0";
            }
            DbDataReader dsOrclUser = null;
            OracleParameter[] paramInsert = new OracleParameter[]
            {
               new OracleParameter("devname",devname),
               new OracleParameter("appcode",appcode)
            };
            StringBuilder str = new StringBuilder();
            try
            {
                dsOrclUser = OrclHelper.SSORead.ExecuteReader(sqlInsert, CommandType.Text, paramInsert);
                StringBuilder strb = new StringBuilder();
                if (dsOrclUser != null)
                {
                    while (dsOrclUser.Read())
                    {
                        if (str.ToString() == "")
                        {
                            str.Append(dsOrclUser["Appname"].ToString() + "_" + dsOrclUser["appcode"].ToString());
                        }
                        else
                        {
                            str.Append("_"+dsOrclUser["Appname"].ToString() + "_" + dsOrclUser["appcode"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 Syn.Utility.Log.WriteErrorLog(ex, "张晶", "查询生活导航详情");
            }
            return str.ToString();
        }


        #region 应用表的操作
        /// <summary>
        /// 根据开发Id获得所有应用及授权状态
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public DataTable SelectAppByDevId(int devId)
        {
            string sql = "select a.APPID,APPNAME,APPCODE,PID,ORDERNUM,(CASE when b.APPID is null then 0 else 1 end) CHECKED from  (select APPID,APPNAME,APPCODE,PID,ORDERNUM from APPLICATION order by ORDERNUM) a, (select APPID from DEVAPPR where DEVID=:DEVID) b where a.APPID=b.APPID(+)";
            OracleParameter[] paras = new OracleParameter[] { 
                new OracleParameter("DEVID",devId)   
            };

            try
            {
                DataSet ds = OrclHelper.SSORead.ExecuteDataSet(sql, CommandType.Text, paras);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "获得所有应用");
                return null;
            }
        }

        /// <summary>
        /// 保存开发授权
        /// </summary>
        /// <param name="devId">开发id</param>
        /// <param name="appIds">应用id</param>
        /// <returns>成功返回0，出错返回-1</returns>
        public int SaveDevAccredit(int devId, string[] appIds)
        {
            OracleParameter[] paras = new OracleParameter[appIds.Length + 1];

            string sql = "begin "
                + "delete DEVAPPR where DEVID=:DEVID;";
            for (int i = 0; i < appIds.Length; i++)
            {
                sql += "insert into DEVAPPR(DAID,DEVID,APPID) values(DAID.NEXTVAL,:DEVID,:APPID" + i.ToString() + ");";
                paras[i] = new OracleParameter("APPID" + i.ToString(), appIds[i]);
            }
            paras[appIds.Length] = new OracleParameter("DEVID", devId);
            sql += " end;";

            try
            {
                bool ret = OrclHelper.SSOWrite.ExecuteTransSql(new List<string>() { sql }, new List<OracleParameter[]>() { paras });
                //OrclHelper.SSOWrite.ExecuteTransSql(null);
                if (ret)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "乙丛涛", "保存开发授权");
                return -1;
            }
        }


        /// <summary>
        /// 查询用户是否拥有权限
        /// </summary>
        /// <returns></returns>
        public int SelectDaid(string ac, string apiCode)
        {
            object[] arg = apiCode.Split('_');
            string pcode = arg[0].ToString();
            string scode = arg[1].ToString();
            string sqlInsert = " select Daid from DEVAPPR where devid=(select Devid from DEVELOPER where devcode=lower(:devcode) or devcode=Upper(:devcode) and state=1 ) and appid=(select appid from application where Pid = (select appid from  application where appcode=lower(:pcode) or appcode=Upper(:pcode)) and appcode=lower(:scode) or appcode=Upper(:scode))  ";
            OracleParameter[] paramInsert = new OracleParameter[]
            {
               new OracleParameter("devcode",ac),
               new OracleParameter("pcode",pcode),
               new OracleParameter("scode",scode)
            };

            DbDataReader dsOrclUser = null;
            try
            {
                dsOrclUser = OrclHelper.SSORead.ExecuteReader(sqlInsert, CommandType.Text, paramInsert);
            }
            catch (Exception ex)
            {
                Syn.Utility.Log.WriteErrorLog(ex, "张晶", "获取权限");
            }
            if (dsOrclUser != null)
            {
                int nid = 0;
                try
                {
                    while (dsOrclUser.Read())
                    {
                        if (dsOrclUser["Daid"].ToString() != "" || dsOrclUser["Daid"] != DBNull.Value)
                        {
                            nid = Int32.Parse(dsOrclUser["Daid"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Syn.Utility.Log.WriteErrorLog(ex, "张晶", "获取权限");
                }
                dsOrclUser.Dispose();
                return nid;
            }
            else
            {
                dsOrclUser.Dispose();
                return 0;
            }
        }

        #endregion
    }
}
