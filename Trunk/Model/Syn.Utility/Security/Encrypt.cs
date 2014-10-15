using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Web.Security;

namespace Syn.Utility.Security
{
    /// <summary>
    /// Author:AutoTech
    /// 加密解密相关(如果使用DES加密，请先配置DESKey)
    /// 包含MD5加密解密
    /// </summary>
    public class Encrypt
    {
        // 密匙
        private static string Key
        {
            get
            {
                return "autotech";//设置全网统一密匙
            }
        }

        #region 返回URL字符串的编码结果
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        #endregion

        #region 返回URL字符串的解码结果
        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        #endregion

        #region Encrypt DES code
        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <param name="encryptKey">加密key</param>
        /// <returns>加密后密文</returns>
        public static string DesEncrypt(string inputString, string encryptKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byte[] byKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(inputString);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                //return error.Message;
                return null;
            }
        }
        #endregion

        #region Decrypt DES code
        /// <summary>
        /// 对字符串进行DES解密
        /// </summary>
        /// <param name="inputString">要解密的字符串</param>
        /// <param name="decryptKey">解密key</param>
        /// <returns></returns>
        public static string DesDecrypt(string inputString, string decryptKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray;
            MemoryStream ms = null;
            CryptoStream cs = null;
            string desString;
            try
            {
                byte[] byKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(inputString);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                desString = encoding.GetString(ms.ToArray());
            }
            finally
            {
                if (cs != null)
                {
                    cs.Close();
                }
                if (ms != null)
                {
                    ms.Close();
                }
            }
            return desString;
        }
        #endregion

        #region Encrypt DES code
        /// <summary>
        /// DES  加密(请慎用，极个别情况下不能被解密)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string DESEncrypt(string strInput) //加密字符串
        {
            return DesEncrypt(strInput, Key);
        }
        #endregion

        #region Decrypt DES code
        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string DESDecrypt(string strInput) //解密
        {
            return DesDecrypt(strInput, Key);
        }
        #endregion

        #region MD5Encrypt
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        }
        #endregion

        #region MD5Encrypt
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypts(string str)
        {
            string md5Str = string.Empty;
            byte[] inputData = Encoding.UTF8.GetBytes(str);
            byte[] md5Data = MD5.Create().ComputeHash(inputData);
            for (int i = 0; i < md5Data.Length; i++)
            {
                md5Str += md5Data[i].ToString("X").PadLeft(2, '0');
            }
            return md5Str;
        }
        #endregion

        # region SHA256函数
        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(str);
            var sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(sha256Data);
            return Convert.ToBase64String(result);  //返回长度为44字节的字符串
        }
        #endregion
    }
}
