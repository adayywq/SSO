using System.Diagnostics;
using System.Reflection;

namespace Syn.Utility.Extense
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 获得Assembly版本号
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
// ReSharper disable AssignNullToNotNullAttribute
            FileVersionInfo assemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
// ReSharper restore AssignNullToNotNullAttribute
            return string.Format("{0}.{1}.{2}", assemblyFileVersion.FileMajorPart, assemblyFileVersion.FileMinorPart, assemblyFileVersion.FileBuildPart);
        }

        /// <summary>
        /// 获得Assembly产品名称
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyProductName()
        {
// ReSharper disable AssignNullToNotNullAttribute
            FileVersionInfo assemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
// ReSharper restore AssignNullToNotNullAttribute
            return assemblyFileVersion.ProductName;
        }

        /// <summary>
        /// 获得Assembly产品版权
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyCopyright()
        {
// ReSharper disable AssignNullToNotNullAttribute
            FileVersionInfo assemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
// ReSharper restore AssignNullToNotNullAttribute
            return assemblyFileVersion.LegalCopyright;
        }
    }
}
