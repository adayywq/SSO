using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace Mdl.SSO.Freedom
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
            string pwd = Syn.Special.EncryptHelper.UserMd5Encrypt(password);

            //用户密码验证
            DataTable dtData = (new SysUser()).LoginBySno(sno);
            if (dtData==null)
            {
                return "220|failed|SNO用户不存在";
            }
            if (pwd != dtData.Rows[0]["PWD"].ToString())
            {
                return "221|failed|SNO密码错误";
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
            string pwd = Syn.Special.EncryptHelper.UserMd5Encrypt(password);

            //用户密码验证
            DataTable dtData = (new SysUser()).LoginByAccId(accountId);
            if (dtData == null)
            {
                return "230|failed|Account用户不存在";
            }
            if (pwd != dtData.Rows[0]["PWD"].ToString())
            {
                return "231|failed|Account密码错误";
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
            string pwd = Syn.Special.EncryptHelper.UserMd5Encrypt(password);

            //用户密码验证
            DataTable dtData = (new SysUser()).LoginByEmail(email);
            if (dtData == null)
            {
                return "240|failed|Email用户不存在";
            }
            if (pwd != dtData.Rows[0]["PWD"].ToString())
            {
                return "241|failed|Email用户关联错误";
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "email", email);
            if (String.IsNullOrEmpty(sessionKey))
            {
                return "206|failed|令牌生成失败";
            }
            return "100|succeed|" + sessionKey;
        }

        //Mobile登录
        public string MobileLogin(string devCode, string mobile, string password)
        {
            string pwd = Syn.Special.EncryptHelper.UserMd5Encrypt(password);

            //用户密码验证
            DataTable dtData = (new SysUser()).LoginByMobile(mobile);
            if (dtData == null)
            {
                return "250|failed|Mobile用户不存在";
            }
            if (pwd != dtData.Rows[0]["PWD"].ToString())
            {
                return "251|failed|Mobile用户关联错误";
            }

            string sessionKey = (new TokenInfo()).AddToken(devCode, "mobile", mobile);
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
            mdlSsoUser.Sno = dtData.Rows[0]["LoginID"].ToString();
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
            DataTable dtData = (new SysUser()).GetBySno(sno);
            if (dtData == null)
            {
                return null;
            }
            mdlSsoUser.UserId = dtData.Rows[0]["USERID"] == null ? "" : dtData.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = sno;
            mdlSsoUser.AccountId = "";
            mdlSsoUser.Name = dtData.Rows[0]["NAME"] == null ? "" : dtData.Rows[0]["NAME"].ToString();
            mdlSsoUser.Email = dtData.Rows[0]["EMAIL"] == null ? "" : dtData.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtData.Rows[0]["MOBILE"] == null ? "" : dtData.Rows[0]["MOBILE"].ToString();
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "";
            mdlSsoUser.Identity = "";
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
            Mdl.Entity.SsoUser mdlSsoUser = new Mdl.Entity.SsoUser();
            DataTable dtData = (new SysUser()).GetByAccId(accountId);
            if (dtData == null)
            {
                return null;
            }
            mdlSsoUser.UserId = dtData.Rows[0]["USERID"] == null ? "" : dtData.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = "";
            mdlSsoUser.AccountId = accountId;
            mdlSsoUser.Name = dtData.Rows[0]["NAME"] == null ? "" : dtData.Rows[0]["NAME"].ToString();
            mdlSsoUser.Email = dtData.Rows[0]["EMAIL"] == null ? "" : dtData.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtData.Rows[0]["MOBILE"] == null ? "" : dtData.Rows[0]["MOBILE"].ToString();
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "";
            mdlSsoUser.Identity = "";
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
            DataTable dtData = (new SysUser()).GetByEmail(email);
            if (dtData == null)
            {
                return null;
            }
            mdlSsoUser.UserId = dtData.Rows[0]["USERID"] == null ? "" : dtData.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = dtData.Rows[0]["LoginType"] == "sno" ? dtData.Rows[0]["LoginID"].ToString() : "";
            mdlSsoUser.AccountId = dtData.Rows[0]["LoginType"] == "accid" ? dtData.Rows[0]["LoginID"].ToString() : "";
            mdlSsoUser.Name = dtData.Rows[0]["NAME"] == null ? "" : dtData.Rows[0]["NAME"].ToString();
            mdlSsoUser.Email = dtData.Rows[0]["EMAIL"] == null ? "" : dtData.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtData.Rows[0]["MOBILE"] == null ? "" : dtData.Rows[0]["MOBILE"].ToString();
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "";
            mdlSsoUser.Identity = "";
            return mdlSsoUser;
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
            DataTable dtData = (new SysUser()).GetByMobile(mobile);
            if (dtData == null)
            {
                return null;
            }
            mdlSsoUser.UserId = dtData.Rows[0]["USERID"] == null ? "" : dtData.Rows[0]["USERID"].ToString();
            mdlSsoUser.Sno = dtData.Rows[0]["LoginType"] == "sno" ? dtData.Rows[0]["LoginID"].ToString() : "";
            mdlSsoUser.AccountId = dtData.Rows[0]["LoginType"] == "accid" ? dtData.Rows[0]["LoginID"].ToString() : "";
            mdlSsoUser.Name = dtData.Rows[0]["NAME"] == null ? "" : dtData.Rows[0]["NAME"].ToString();
            mdlSsoUser.Email = dtData.Rows[0]["EMAIL"] == null ? "" : dtData.Rows[0]["EMAIL"].ToString();
            mdlSsoUser.Mobile = dtData.Rows[0]["MOBILE"] == null ? "" : dtData.Rows[0]["MOBILE"].ToString();
            mdlSsoUser.Department = "";
            mdlSsoUser.Job = "";
            mdlSsoUser.Identity = "";
            return mdlSsoUser;
        }
        
        #endregion
    }
}
