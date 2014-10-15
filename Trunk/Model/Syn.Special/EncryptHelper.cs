using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Syn.Special
{
    public static class EncryptHelper
    {
        /// <summary>
        /// 一卡通查询密码加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CardEncrypt(string str)
        {
            string pwd = "";
            byte[] newPwd = new byte[16];
            if (Syn.GDSC2.AIOAPI.G_PW_Encrypt(System.Text.Encoding.Default.GetBytes(str), (newPwd.Length > 8 ? 8 : newPwd.Length), newPwd) > 0)
            {
                pwd = System.Text.Encoding.Default.GetString(newPwd);
            }
            return pwd;
        }

        /// <summary>
        /// 一卡通查询密码解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CardDecrypt(string str)
        {
            string targetTemp = "";
            byte[] newPwd = new byte[8];
            if (Syn.GDSC2.AIOAPI.G_PW_Decrypt(System.Text.Encoding.Default.GetBytes(str), 16, newPwd) > 0)
            {
                targetTemp = System.Text.Encoding.Default.GetString(newPwd);
            }
            return targetTemp.Trim('\0');
        }

        /// <summary>
        /// 用户密码加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string UserMd5Encrypt(string pwd)
        {
            pwd = pwd + "synjones";
            return Md5Encrypts(pwd);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypts(string str)
        {
            string md5Str = string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
            md5Str = BitConverter.ToString(output).Replace("-", "");
            return md5Str;
        }

        /// <summary>
        /// 计算给定字符的MD5哈希值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetMD5HashCode(this string data)
        {
            return GetHashCode(data, "utf-8", "MD5");
        }

        /// <summary>
        /// 对传入的字符串进行哈希运算
        /// </summary>
        /// <param name="data"></param>
        /// <param name="chars_set">符串编码格式(gbk,utf-8等)</param>
        /// <param name="sign_type">哈希算法名称(MD5,SHA,SHA1)</param>
        /// <returns></returns>
        public static byte[] GetHashCode(this string data, string chars_set, string sign_type)
        {
            Encoding encoding = Encoding.GetEncoding(chars_set);
            byte[] data2sign = encoding.GetBytes(data);
            byte[] datasigned = GetHashCode(data2sign, sign_type);
            return datasigned;
        }
        
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign_type">哈希算法名称(MD5,SHA,SHA1)</param>
        /// <returns></returns>
        public static byte[] GetHashCode(this byte[] data, string sign_type)
        {
            HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create(sign_type);
            return hash.ComputeHash(data);
        }

        /// <summary>
        /// 获取16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte d in data)
            {
                sb.Append(d.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
