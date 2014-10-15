using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Syn.Utility.Security;
using Syn.Utility.Function;

namespace Syn.Utility.Extense
{
    /// <summary>
    /// Author:AutoTech
    /// XML操作类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 获取整个文件
        /// </summary>
        /// <param name="nameSpaceOrFileName">命名空间名称或文件名称</param>
        /// <returns></returns>
        public static XDocument GetAllFile(string nameSpaceOrFileName)
        {
            string relativePath = string.Empty;
            if (!string.IsNullOrEmpty(nameSpaceOrFileName))
            {
                bool webApp = System.Web.HttpContext.Current != null ? true : false;
                string filePath;
                if (nameSpaceOrFileName.IndexOf(":") > -1)
                {
                    filePath = nameSpaceOrFileName;
                }
                else
                {
                    if (webApp)
                    {
                        if (nameSpaceOrFileName.ToLower().EndsWith(".xml"))
                        {
                            relativePath = nameSpaceOrFileName.StartsWith("~/")
                                               ? nameSpaceOrFileName
                                               : string.Format("~/App_Data/{0}", nameSpaceOrFileName.Trim('/'));
                        }
                        else
                        {
                            relativePath = string.Format("~/App_Data/{0}.xml", nameSpaceOrFileName);
                        }
                        filePath = System.Web.HttpContext.Current.Server.MapPath(relativePath);
                    }
                    else
                    {
                        filePath = nameSpaceOrFileName.StartsWith("\\")
                                       ? AppDomain.CurrentDomain.BaseDirectory + nameSpaceOrFileName.Substring(1)
                                       : AppDomain.CurrentDomain.BaseDirectory + nameSpaceOrFileName;
                        filePath = filePath.ToLower();
                        if(!filePath.EndsWith(".xml"))
                        {
                            filePath += ".xml";
                        }
                    }

                }
                try
                {
                    //todo 缓存中读取
                    string key = Encrypt.Md5Encrypt(filePath);
                    XDocument xd = CacheHelper.Get<XDocument>(key);
                    if (xd == null)
                    {
                        xd = XDocument.Load(filePath);
                        if (xd.Document != null)
                        {
                            CacheHelper.Add(key, xd, filePath);
                        }
                    }
                    return xd;

                }
                catch (Exception)
                {

                    throw new FieldAccessException("文件" + relativePath + "不存在");
                }

            }
            return null;
        }

        /// <summary>
        /// 通过XPath查询
        /// </summary>
        /// <param name="nameSpaceOrFileName">命名空间名称或文件名称</param>
        /// <param name="xPath">xPath</param>
        /// <returns>对象</returns>
        public static XElement GetByXPath(string nameSpaceOrFileName, string xPath)
        {
            if (!string.IsNullOrEmpty(nameSpaceOrFileName) && !string.IsNullOrEmpty(xPath))
            {
                XDocument root = GetAllFile(nameSpaceOrFileName);
                try
                {
                    return root.XPathSelectElement(xPath);
                }
                catch (Exception)
                {

                    throw new XPathException("XPath:" + xPath + "错误");
                }
            }
            return null;
        }

    }
}
