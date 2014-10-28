using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;
using Syn.Utility.Function;

namespace Mdl.SSO.Card
{ 
    public class SsoUser
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string UserLogin(string devCode, string loginType, string loginId, string password)
        {            
            string result = "200|failed|系统未知错误";
            if (String.IsNullOrEmpty(loginType))
            {
                result = "201|failed|请选择登录方式";
                return result;
            }
            if (String.IsNullOrEmpty(loginId))
            {
                result = "202|failed|请正确填写登录ID";
                return result;
            }
            if (String.IsNullOrEmpty(loginType))
            {
                result = "203|failed|请正确填写密码";
                return result;
            }
            if (loginId.ToLower().Trim() == "admin")
            {
                result = AdminLogin(devCode, loginId, password);
                return result;
            }
            switch (loginType.Trim().ToLower())
            {
                case "sno":
                    result = SnoLogin(devCode, loginId, password);
                    break;
                case "accid":
                    result = AccIdLogin(devCode, loginId, password);
                    break;
                case "email":
                    result = EmailLogin(devCode, loginId, password);
                    break;
                case "mobile":
                    result = MobileLogin(devCode, loginId, password);
                    break;
                case "mercid":
                    result = MercLogin(devCode, loginId, password);
                    break;
                default:
                    result = "204|failed|登录方式有误";
                    break;
            }
            return result;
        }

        #region 用户登录
        
        //admin登录
        public string AdminLogin(string devCode, string loginId, string password)
        {
            string pwd = Syn.Special.EncryptHelper.UserMd5Encrypt(password);
            DataTable dtData = (new SysUser()).LoginBySno(loginId);
            if (dtData == null)
            {
                return "210|failed|admin用户不存在";
            }
            if (pwd != dtData.Rows[0]["PWD"].ToString())
            {
                return "211|failed|admin密码错误";
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "sno", loginId);
            if (String.IsNullOrEmpty(sessionKey))
            {
                return "206|failed|令牌生成失败";
            }
            return "100|succeed|"+sessionKey;
        }

        //SNO登录
        public string SnoLogin(string devCode, string sno, string password)
        {
            if (!Syn.Special.General.SiosInit())
            {
                return "205|failed|SIOS初始化失败";
            }
            string pwd = Syn.Special.EncryptHelper.CardEncrypt(password);

            //用户密码验证
            DataTable dtData = (new CardUser()).LoginBySno(sno);
            if (dtData == null)
            {
                return "220|failed|SNO用户不存在";
            }
            if (pwd != dtData.Rows[0]["QUERYPIN"].ToString())
            {
                return "221|failed|SNO密码错误";
            }

            //用户导入
            if (!(new SysUser()).IsExistsSno(sno))
            {
                Mdl.Entity.SysUser mdlSysUser = new Mdl.Entity.SysUser();
                mdlSysUser.UNITEID = 0;
                mdlSysUser.LOGINTYPE = "sno";
                mdlSysUser.LOGINID = sno;
                mdlSysUser.NAME = dtData.Rows[0]["NAME"].ToString().Trim();
                mdlSysUser.EMAIL = "";
                mdlSysUser.MOBILE = "";
                mdlSysUser.PWD = "";
                mdlSysUser.INITIDENT = dtData.Rows[0]["CLASS"].ToString();
                mdlSysUser.DATASOURCE = "card";
                mdlSysUser.CREATEDATE = DateTime.Now;
                mdlSysUser.STATE = 1;

                int userId=(new SysUser()).AddUser(mdlSysUser);
                if (userId == 0)
                {
                    return "222|failed|SNO信息导入失败";
                }
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "sno", sno);
            if (String.IsNullOrEmpty(sessionKey))
            {
                return "206|failed|令牌生成失败";
            }
            return "100|succeed|" + sessionKey;
        }

        //AccountId登录
        public string AccIdLogin(string devCode, string accountId, string password)
        {
            if (!Syn.Special.General.SiosInit())
            {
                return "205|failed|SIOS初始化失败";
            }
            string pwd = Syn.Special.EncryptHelper.CardEncrypt(password);

            //用户密码验证
            DataTable dtData = (new CardUser()).LoginByAccId(accountId);
            if (dtData == null)
            {
                return "230|failed|Account用户不存在";
            }
            if (pwd != dtData.Rows[0]["QUERYPIN"].ToString())
            {
                return "231|failed|Account密码错误";
            }

            //用户导入
            string sno = dtData.Rows[0]["SNO"].ToString().Trim();
            if (String.IsNullOrEmpty(sno))
            {
                if (!(new SysUser()).IsExistsAccId(accountId))
                {
                    Mdl.Entity.SysUser mdlSysUser = new Mdl.Entity.SysUser();
                    mdlSysUser.UNITEID = 0;
                    mdlSysUser.LOGINTYPE = "accid";
                    mdlSysUser.LOGINID = accountId;
                    mdlSysUser.NAME = dtData.Rows[0]["NAME"].ToString().Trim();
                    mdlSysUser.EMAIL = "";
                    mdlSysUser.MOBILE = "";
                    mdlSysUser.PWD = "";
                    mdlSysUser.INITIDENT = dtData.Rows[0]["CLASS"].ToString();
                    mdlSysUser.DATASOURCE = "card";
                    mdlSysUser.CREATEDATE = DateTime.Now;
                    mdlSysUser.STATE = 1;
                    int userId=(new SysUser()).AddUser(mdlSysUser);
                    if ( userId == 0)
                    {
                        return "232|failed|Account信息导入失败";
                    }
                }
            }
            else
            {
                if (!(new SysUser()).IsExistsSno(sno))
                {
                    Mdl.Entity.SysUser mdlSysUser = new Mdl.Entity.SysUser();
                    mdlSysUser.UNITEID = 0;
                    mdlSysUser.LOGINTYPE = "sno";
                    mdlSysUser.LOGINID = sno;
                    mdlSysUser.NAME = dtData.Rows[0]["NAME"].ToString();
                    mdlSysUser.EMAIL = "";
                    mdlSysUser.MOBILE = "";
                    mdlSysUser.PWD = "";
                    mdlSysUser.INITIDENT = dtData.Rows[0]["CLASS"].ToString();
                    mdlSysUser.DATASOURCE = "card";
                    mdlSysUser.CREATEDATE = DateTime.Now;
                    mdlSysUser.STATE = 1;
                    int userId = (new SysUser()).AddUser(mdlSysUser);
                    if ( userId == 0)
                    {
                        return "222|failed|SNO信息导入失败";
                    }
                }
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "accid", accountId);
            if (String.IsNullOrEmpty(sessionKey))
            {
                return "206|failed|令牌生成失败";
            }
            return "100|succeed|" + sessionKey;
        }

        //Eamil登录
        public string EmailLogin(string devCode, string email, string password)
        {
            DataTable dtLogin = (new SysUser()).GetByEmail(email);
            if (dtLogin == null)
            {
                return "240|failed|Email用户不存在";
            }
            //如果学工号存在
            if (dtLogin.Rows[0]["LoginType"] == "sno")
            {
                return SnoLogin(devCode, dtLogin.Rows[0]["LoginID"].ToString(), password);
            }
            //如果账号存在
            else if (dtLogin.Rows[0]["LoginType"] == "accid")
            {
                return AccIdLogin(devCode, dtLogin.Rows[0]["LoginID"].ToString(), password);
            }
            //如果学工号与账号均不存在
            else
            {
                return "241|failed|Email用户关联错误";
            }
        }

        //Mobile登录
        public string MobileLogin(string devCode, string mobile, string password)
        {
            DataTable dtLogin = (new SysUser()).GetByMobile(mobile);
            if (dtLogin == null)
            {
                return "250|failed|Mobile用户不存在";
            }
            //如果学工号存在
            if (dtLogin.Rows[0]["LoginType"] == "sno")
            {
                return SnoLogin(devCode, dtLogin.Rows[0]["LoginID"].ToString(), password);
            }
            //如果账号存在
            else if (dtLogin.Rows[0]["LoginType"] == "accid")
            {
                return AccIdLogin(devCode, dtLogin.Rows[0]["LoginID"].ToString(), password);
            }
            //如果学工号与账号均不存在
            else
            {
                return "251|failed|Mobile用户关联错误";
            }
        }

        //商户登录
        public string MercLogin(string devCode, string mercId, string password)
        {
            if (!Syn.Special.General.SiosInit())
            {
                return "205|failed|SIOS初始化失败";
            }

            string pwd = Syn.Special.EncryptHelper.CardEncrypt(password);
            
            //用户密码验证
            DataTable dtData = (new CardUser()).LoginByMercId(mercId);
            if (dtData == null)
            {
                return "260|failed|Merc用户不存在";
            }
            if (pwd != dtData.Rows[0]["Password"].ToString())
            {
                return "261|failed|Merc密码错误";
            }

            //用户导入
            if (!(new SysUser()).IsExistsMercId(mercId))
            {
                Mdl.Entity.SysUser mdlSysUser = new Mdl.Entity.SysUser();
                mdlSysUser.UNITEID = 0;
                mdlSysUser.LOGINTYPE = "mercid";
                mdlSysUser.LOGINID = mercId;
                mdlSysUser.NAME = dtData.Rows[0]["MercName"].ToString().Trim();
                mdlSysUser.EMAIL = "";
                mdlSysUser.MOBILE = "";
                mdlSysUser.PWD = "";
                mdlSysUser.INITIDENT = "9";
                mdlSysUser.DATASOURCE = "card";
                mdlSysUser.CREATEDATE = DateTime.Now;
                mdlSysUser.STATE = 1;

                int userId = (new SysUser()).AddUser(mdlSysUser);
                if (userId == 0)
                {
                    return "262|failed|Merc信息导入失败";
                }
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "mercid", mercId);
            if (String.IsNullOrEmpty(sessionKey))
            {
                return "206|failed|令牌生成失败";
            }
            return "100|succeed|" + sessionKey;
        }

        #endregion

        /// <summary>
        /// 验证用户登录状态
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public string LoginVerify(string devCode, string sessionKey)
        {            
            string loginType="";
            string loginId="";
            if ((new TokenInfo()).IsExistsSk(devCode, sessionKey, ref loginType, ref loginId))
            {
                if (String.IsNullOrEmpty(loginType))
                {
                    return "201|failed|LoginType为空";
                }
                else if (String.IsNullOrEmpty(loginId))
                {
                    return "202|failed|LoginId为空";
                }
                else
                {
                    return "100|succeed|" + loginType + "#" + loginId;
                }
            }
            else
            {
                return "200|failed|SessionKey不存在";
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="loginType"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUser(string devCode, string sessionKey)
        {
            string loginType = "";
            string loginId = "";
            if (!(new Mdl.SSO.TokenInfo()).IsExistsSk(devCode, sessionKey, ref loginType, ref loginId))
            {
                return null;
            }
            else
            {
                if (String.IsNullOrEmpty(loginType) || String.IsNullOrEmpty(loginId))
                {
                    return null;
                }
            }
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();

            switch (loginType.Trim().ToLower())
            {
                case "sno":
                    mdlSsoUser = GetUserBySno(devCode, loginId);
                    break;
                case "accid":
                    mdlSsoUser = GetUserByAccId(devCode, loginId);
                    break;
                case "email":
                    mdlSsoUser = GetUserByEmail(devCode, loginId);
                    break;
                case "mobile":
                    mdlSsoUser = GetUserByMobile(devCode, loginId);
                    break;
                case "mercid":
                    mdlSsoUser = GetUserByMerc(devCode, loginId);
                    break;
                default:
                    mdlSsoUser = null;
                    break;
            }
            return mdlSsoUser;
        }

        #region 获取用户信息

        /// <summary>
        /// 获取Admin用户信息
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserByAdmin(string sno)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtData = (new SysUser()).GetBySno(sno);
            if (dtData == null)
            {
                return null;
            }
            mdlSsoUser.UserId = dtData.Rows[0]["USERID"] == null ? "" : dtData.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = dtData.Rows[0]["LoginID"] == null ? "" : dtData.Rows[0]["LoginID"].ToString();
            mdlSsoUser.AccountId = "";
            mdlSsoUser.Name = dtData.Rows[0]["NAME"] == null ? "" : dtData.Rows[0]["NAME"].ToString();
            mdlSsoUser.Email = dtData.Rows[0]["EMAIL"] == null ? "" : dtData.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtData.Rows[0]["MOBILE"] == null ? "" : dtData.Rows[0]["MOBILE"].ToString();
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "-1#授权岗";
            mdlSsoUser.Identity = "";
            return mdlSsoUser;
        }

        /// <summary>
        /// 根据SNO获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="sno"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserBySno(string devCode, string sno)
        {
            if (sno.Trim().ToLower() == "admin")
            {
                return GetUserByAdmin(sno);
            }

            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtSys = (new SysUser()).GetBySno(sno);
            if (dtSys == null)
            {
                return null;
            }
            //UserId,Sno,Email,Mobile
            mdlSsoUser.UserId = dtSys.Rows[0]["USERID"] == null ? "" : dtSys.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = sno;
            mdlSsoUser.Email = dtSys.Rows[0]["EMAIL"] == null ? "" : dtSys.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtSys.Rows[0]["MOBILE"] == null ? "" : dtSys.Rows[0]["MOBILE"].ToString();
            //Name,Department,Identity
            DataTable dtIdent = (new CardUser()).GetSsoUserBySno(mdlSsoUser.Sno);
            if (dtIdent != null)
            {
                mdlSsoUser.Name = dtIdent.Rows[0]["NAME"] == null ? "" : dtIdent.Rows[0]["NAME"].ToString().Trim();
                mdlSsoUser.Department = (dtIdent.Rows[0]["DeptCode"] == null ? "" : dtIdent.Rows[0]["DeptCode"].ToString()) + "#" + (dtIdent.Rows[0]["DeptName"] == null ? "" : dtIdent.Rows[0]["DeptName"].ToString());
                mdlSsoUser.Identity = (dtIdent.Rows[0]["Class"] == null ? "" : dtIdent.Rows[0]["Class"].ToString()) + "#" + (dtIdent.Rows[0]["PidCode"] == null ? "" : dtIdent.Rows[0]["PidCode"].ToString()) + "#" + (dtIdent.Rows[0]["PidName"] == null ? "" : dtIdent.Rows[0]["PidName"].ToString());
            }
            //AccountId
            DataTable dtAccId = (new CardUser()).GetAccIdBySno(mdlSsoUser.Sno);
            if (dtAccId != null)
            {
                for (int i = 0; i < dtAccId.Rows.Count; i++)
                {
                    mdlSsoUser.AccountId = mdlSsoUser.AccountId + dtAccId.Rows[i]["Account"].ToString() + "|";
                }
                mdlSsoUser.AccountId = mdlSsoUser.AccountId.Trim('|');
            }
            //Job
            mdlSsoUser.Job = "";
            return mdlSsoUser;
        }

        /// <summary>
        /// 根据AccountId获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserByAccId(string devCode, string accountId)
        {
            DataTable dtSno = (new CardUser()).GetSnoByAccId(accountId);
            if (dtSno == null)
            {
                return null;
            }
            if ((dtSno.Rows[0]["SNO"] != null) && !String.IsNullOrEmpty(dtSno.Rows[0]["SNO"].ToString().Trim()))
            {
                return GetUserBySno(devCode, dtSno.Rows[0]["SNO"].ToString().Trim());
            }

            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtSys = (new SysUser()).GetByAccId(accountId);
            if (dtSys == null)
            {
                return null;
            }
            //UserId,Sno,Email,Mobile
            mdlSsoUser.UserId = dtSys.Rows[0]["USERID"] == null ? "" : dtSys.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = dtSys.Rows[0]["LoginType"] == "sno" ? dtSys.Rows[0]["LoginID"].ToString() : "";
            mdlSsoUser.Email = dtSys.Rows[0]["EMAIL"] == null ? "" : dtSys.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtSys.Rows[0]["MOBILE"] == null ? "" : dtSys.Rows[0]["MOBILE"].ToString();
            //Name,Department,Identity
            DataTable dtIdent = (new CardUser()).GetSsoUserByAccId(accountId);
            if (dtIdent != null)
            {
                mdlSsoUser.Name = dtIdent.Rows[0]["NAME"] == null ? "" : dtIdent.Rows[0]["NAME"].ToString().Trim();                
                mdlSsoUser.Identity = (dtIdent.Rows[0]["Class"] == null ? "" : dtIdent.Rows[0]["Class"].ToString()) + "#" + (dtIdent.Rows[0]["PidCode"] == null ? "" : dtIdent.Rows[0]["PidCode"].ToString()) + "#" + (dtIdent.Rows[0]["PidName"] == null ? "" : dtIdent.Rows[0]["PidName"].ToString());
                string deptCode = dtIdent.Rows[0]["DeptCode"] == null ? "" : dtIdent.Rows[0]["DeptCode"].ToString();
                if (!String.IsNullOrEmpty(deptCode))
                {
                    DataTable dtDept = (new CardUser()).GetDeptByCode(deptCode);
                    if (dtDept != null)
                    {
                        mdlSsoUser.Department = (dtIdent.Rows[0]["Code"] == null ? "" : dtIdent.Rows[0]["Code"].ToString()) + "#" + (dtIdent.Rows[0]["Name"] == null ? "" : dtIdent.Rows[0]["Name"].ToString());
                    }
                }
            }
            //AccountId
            mdlSsoUser.AccountId = accountId;
            //Job
            mdlSsoUser.Job = "";
            return mdlSsoUser;            
        }

        /// <summary>
        /// 根据Email获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserByEmail(string devCode, string email)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtSys = (new SysUser()).GetByEmail(email);
            if (dtSys == null)
            {
                return null;
            }

            if ((dtSys.Rows[0]["SNO"] != null) && !String.IsNullOrEmpty(dtSys.Rows[0]["SNO"].ToString()))
            {
                return GetUserBySno(devCode, dtSys.Rows[0]["SNO"].ToString());
            }

            if ((dtSys.Rows[0]["ACCOUNTID"] != null) && !String.IsNullOrEmpty(dtSys.Rows[0]["ACCOUNTID"].ToString()))
            {
                return GetUserByAccId(devCode, dtSys.Rows[0]["ACCOUNTID"].ToString());
            }
            return null;
        }

        /// <summary>
        /// 根据Mobile获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserByMobile(string devCode, string mobile)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtSys = (new SysUser()).GetByMobile(mobile);
            if (dtSys == null)
            {
                return null;
            }

            if ((dtSys.Rows[0]["SNO"] != null) && !String.IsNullOrEmpty(dtSys.Rows[0]["SNO"].ToString()))
            {
                return GetUserBySno(devCode, dtSys.Rows[0]["SNO"].ToString());
            }

            if ((dtSys.Rows[0]["ACCOUNTID"] != null) && !String.IsNullOrEmpty(dtSys.Rows[0]["ACCOUNTID"].ToString()))
            {
                return GetUserByAccId(devCode, dtSys.Rows[0]["ACCOUNTID"].ToString());
            }
            return null;
        }

        /// <summary>
        /// 根据Merc获取用户信息
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="mercId"></param>
        /// <returns></returns>
        public Mdl.Entity.SsoUser GetUserByMerc(string devCode, string mercId)
        {
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();

            DataTable dtIdent = (new CardUser()).GetSsoUserByMercId(mercId);
            DataTable dtSys = (new SysUser()).GetByMercId(mercId);
            if ((dtIdent == null) || (dtSys == null))
            {
                return null;
            }

            mdlSsoUser.UserId = dtSys.Rows[0]["UserID"] == null ? "" : dtSys.Rows[0]["UserID"].ToString();
            mdlSsoUser.Sno = dtSys.Rows[0]["LoginID"].ToString();
            mdlSsoUser.AccountId = dtSys.Rows[0]["LoginID"].ToString();
            mdlSsoUser.Name = dtIdent.Rows[0]["MercName"] == null ? "" : dtIdent.Rows[0]["MercName"].ToString();
            mdlSsoUser.Email = "";
            mdlSsoUser.Mobile = "";
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "";
            mdlSsoUser.Identity = "9#0#商户";

            return mdlSsoUser;
        }

        #endregion
        
    }
}
