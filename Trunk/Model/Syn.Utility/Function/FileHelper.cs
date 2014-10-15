using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
using Syn.Utility.Extense;
using Syn.Utility.Security;
namespace Syn.Utility.Function
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取文件夹的大小
        /// </summary>
        /// <param name="dirPath">文件夹的路径</param>
        /// <returns>文件夹下所有文件的大小总和</returns>
        public static long GetDirectoryLength(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            //定义一个DirectoryInfo对象
            var di = new DirectoryInfo(dirPath);
            //通过GetFiles方法,获取di目录中的所有文件的大小
            for (int i = 0; i < di.GetFiles().Length; i++)
            {
                FileInfo fi = di.GetFiles()[i];
                len += fi.Length;
            }
            //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                len += dis.Sum(t => GetDirectoryLength(t.FullName));
            }
            return len;
        }

        /// <summary>
        /// 保存上传的单个文件
        /// </summary>
        /// <param name="saveDir">保存的目录</param>
        /// <param name="maxAllowFileSize">最大允许的文件大小,单位K</param>
        /// <param name="allowFileExtName">允许上传的文件扩展名列表</param>
        /// <returns>上传后保存的文件路径,如果上传失败则为null</returns>
        /// <example>string filename = Utils.SaveRequestFile("~/upload/", 500, ".jpg,.jpeg,.gif,.png,.zip,.rar");</example>
        public static string SaveRequestFile(string saveDir, int maxAllowFileSize, string allowFileExtName)
        {
            if (HttpContext.Current.Request.Files[0].FileName == "" || HttpContext.Current.Request.Files[0] == null)
            {
                return "filenull";
            }

            //获得上文件的文件名
            string filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            //获得文件扩展名
            string fileextname = (Path.GetExtension(filename).ToLower()).Trim(new[] { '.' });
            if (allowFileExtName.ToLower().InArray(fileextname.ToLower()))
            {
                string nameWithoutExtens = Path.GetFileNameWithoutExtension(filename);
                if (nameWithoutExtens.GetStringLength() > 200)
                {
                    return "nametoolong";
                }
                if ((nameWithoutExtens.LastIndexOf('.') == (nameWithoutExtens.Length - 1)) || (nameWithoutExtens.LastIndexOf(' ') == (nameWithoutExtens.Length - 1)))
                {
                    return "lastspecialchar";
                }
                if ((nameWithoutExtens.IndexOf(' ') == 0) || (nameWithoutExtens.IndexOf('　') == 0) || (nameWithoutExtens.LastIndexOf(' ') == (nameWithoutExtens.Length - 1)) || (nameWithoutExtens.LastIndexOf('　') == (nameWithoutExtens.Length - 1)))
                {
                    return "havespace";
                }
                if ((nameWithoutExtens.IndexOf('%') >= 0) || (nameWithoutExtens.IndexOf('#') >= 0) || (nameWithoutExtens.IndexOf('\'') >= 0) || (nameWithoutExtens.IndexOf('’') >= 0))
                {
                    return "specialchar";
                }
                if (HttpContext.Current.Request.Files[0].ContentLength <= maxAllowFileSize * 1024)
                {
                    if (HttpContext.Current.Request.Files[0].ContentLength == 0)
                    {
                        return "fileerror";
                    }
                    //取得要保存图片的磁盘绝对路径
                    string savepath = saveDir;
                    //如果目录不存在则创建
                    if (!Directory.Exists(GetMapPath(savepath)))
                    {
                        CreateDir(GetMapPath(savepath));
                    }

                    //// 取得要保存图片的磁盘绝对路径
                    //savepath = RequestHelper.GetVirtualDirectory() + SaveDir;
                    //// 如果目录不存在则创建
                    //if (!Directory.Exists(savepath))
                    //{
                    //    CreateDir(savepath);
                    //}

                    // 新文件名
                    string filePathName = savepath + filename;

                    // 保存文件到磁盘
                    try
                    {
                        HttpContext.Current.Request.Files[0].SaveAs(filePathName);
                    }
                    catch (Exception)
                    {
                        //string strRemark = "\n目标路径：" + filePathName;
                        //Log.WriteErrorLog(ex, "", "公用的上传文件", strRemark, false);
                        return "Error";
                    }

                    // 返回文件名                    
                    string[] fileTemp = filePathName.Split(new[] { '\\' });
                    filePathName.Substring(0, filePathName.LastIndexOf("\\"));
                    string fileName = fileTemp[fileTemp.Length - 1];
                    string cr = "." + fileextname;

                    int r = fileName.ToLower().IndexOf(cr.ToLower());
                    fileName = fileName.Substring(0, r).InputText();

                    return fileName + "|" + fileextname + "|" +
                           string.Format("{0:f2}",
                                         Convert.ToDouble(HttpContext.Current.Request.Files[0].ContentLength) /
                                         Convert.ToDouble(1024));
                }
                return "ToMaxFileSize";
            }
            return "extenserror";
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFolder">源文件夹的相对路径</param>
        /// <param name="sourceFile">源文件名（包括扩展名）</param>
        /// <param name="destFolder">目的地文件夹的相对路径</param>
        /// <param name="destFile">复制后的新文件名（包括扩展名）</param>
        public static string CopyFile(string sourceFolder, string sourceFile, string destFolder, string destFile)
        {
            if (destFolder == null) throw new ArgumentNullException("destFolder");
            if ((String.IsNullOrEmpty(sourceFolder)) || (String.IsNullOrEmpty(sourceFile)) || (String.IsNullOrEmpty(destFolder)) || (String.IsNullOrEmpty(destFile)))
            {
                return "folder or file is null";
            }

            sourceFolder = GetMapPath(sourceFolder);
            destFolder = GetMapPath(destFolder);
            //sourceFolder = RequestHelper.GetVirtualDirectory() + sourceFolder;
            //destFolder = RequestHelper.GetVirtualDirectory() + destFolder;

            //查看文件是否存在
            if (!File.Exists(sourceFolder + sourceFile))
            {
                return "not have source file";
            }
            // 如果目录不存在则创建
            if (!Directory.Exists(destFolder))
            {
                CreateDir(destFolder);
            }
            if (Directory.Exists(destFolder))
            {
                try
                {
                    File.Copy(sourceFolder + sourceFile, destFolder + destFile, true);
                }
                catch (Exception)
                {
                    //string strRemark = "\n源路径：" + sourceFolder + sourceFile;
                    //strRemark += "\n目标路径：" + destFolder + destFile;
                    //Log.WriteErrorLog(ex, "", "公用的复制文件", strRemark, false);                    
                    return "Error";
                }
            }
            else
            {
                return "not have path";
            }
            return "succeed";
        }

        /// <summary>
        /// 拷贝文件夹中所有内容
        /// </summary>
        /// <param name="strSrcdir">源文件夹</param>
        /// <param name="strDesdir">目标文件夹</param>
        public static string CopyDirectory(string strSrcdir, string strDesdir)
        {
            if ((String.IsNullOrEmpty(strSrcdir)) || (String.IsNullOrEmpty(strDesdir)))
            {
                return "path is null";
            }

            strSrcdir = GetMapPath(strSrcdir);
            string desfolderdir = GetMapPath(strDesdir);
            //strSrcdir = RequestHelper.GetVirtualDirectory() + strSrcdir;
            //string desfolderdir = RequestHelper.GetVirtualDirectory() + _strDesdir;

            // 如果目录不存在则创建
            if (!Directory.Exists(desfolderdir))
            {
                CreateDir(desfolderdir);
            }

            try
            {
                string[] filenames = Directory.GetFileSystemEntries(strSrcdir);

                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    //if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    //{

                    //    string currentdir = desfolderdir + "/" + file.Substring(file.LastIndexOf("/") + 1);
                    //    if (!Directory.Exists(currentdir))
                    //    {
                    //        Directory.CreateDirectory(currentdir);
                    //    }

                    //    CopyDirectory(file, currentdir);
                    //}


                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }

                    if (File.Exists(srcfileName))
                    {
                        File.Delete(srcfileName);
                    }
                    File.Copy(file, srcfileName);
                }
            }
            catch (DirectoryNotFoundException)
            {
                //string strRemark = "\n源路径：" + strSrcdir;
                //strRemark += "\n目标路径：" + desfolderdir;
                //Log.WriteErrorLog(dnfe, "", "公用的拷贝文件夹下所有文件", strRemark, false);

                return "服务器上的文件已删除，无法找到路径";
            }
            catch (Exception)
            {
                //string strRemark = "\n源路径：" + strSrcdir;
                //strRemark += "\n目标路径：" + desfolderdir;
                //Log.WriteErrorLog(ex, "", "公用的拷贝文件夹下所有文件", strRemark, false);                
                return "Error";
            }

            return "succeed";
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFolder">源文件夹的相对路径</param>
        /// <param name="sourceFile">源文件名（包括扩展名）</param>
        /// <param name="destFolder">目的地文件夹的相对路径</param>
        /// <param name="destFile">移动后的新文件名（包括扩展名）</param>
        public static string MoveFile(string sourceFolder, string sourceFile, string destFolder, string destFile)
        {
            if ((String.IsNullOrEmpty(sourceFolder)) || (String.IsNullOrEmpty(sourceFile)) || (String.IsNullOrEmpty(destFolder)) || (String.IsNullOrEmpty(destFile)))
            {
                return "folder or file is null";
            }

            sourceFolder = GetMapPath(sourceFolder);
            destFolder = GetMapPath(destFolder);

            //sourceFolder = RequestHelper.GetVirtualDirectory() + sourceFolder;
            //destFolder = RequestHelper.GetVirtualDirectory() + destFolder;

            //查看文件是否存在
            if (!File.Exists(sourceFolder + sourceFile))
            {
                return "not have source file";
            }
            // 如果目录不存在则创建
            if (!Directory.Exists(destFolder))
            {
                CreateDir(destFolder);
            }
            if (Directory.Exists(destFolder))
            {
                try
                {
                    File.Move(sourceFolder + sourceFile, destFolder + destFile);
                }
                catch (Exception)
                {
                    //string strRemark = "\n源路径：" + sourceFolder + sourceFile;
                    //strRemark += "\n目标路径：" + destFolder + destFile;
                    //Log.WriteErrorLog(ex, "", "公用的移动文件", strRemark, false);                    
                    return "Error";
                }
            }
            else
            {
                return "not have path";
            }
            return "succeed";
        }

        /// <summary>
        /// 更改文件名
        /// </summary>
        /// <param name="filePath">源文件夹的相对路径</param>
        /// <param name="oldName">源文件名（包括扩展名）</param>
        /// <param name="newName">更改后的文件名</param>
        public static string EditFileName(string filePath, string oldName, string newName)
        {
            if ((String.IsNullOrEmpty(filePath)) || (String.IsNullOrEmpty(oldName)) || (String.IsNullOrEmpty(oldName)))
            {
                return "folder or file is null";
            }

            string sourceFile = GetMapPath(filePath + oldName);
            string destFile = GetMapPath(filePath + newName);
            //string sourceFile = RequestHelper.GetVirtualDirectory() + filePath + oldName;
            //string destFile = RequestHelper.GetVirtualDirectory() + filePath + newName;

            //查看源文件是否存在
            if (!File.Exists(sourceFile))
            {
                return "not have source file";
            }

            //查看目标文件是否存在
            if (File.Exists(destFile))
            {
                File.Delete(destFile);
            }

            var fi = new FileInfo(sourceFile);
            fi.MoveTo(destFile);

            return "succeed";
        }

        /// <summary>
        /// 删除指定目录及其下面所有子目录(同时删除指定目录)
        /// </summary>
        /// <param name="strPath">要删除的目录 如c:\test</param>
        /// <returns></returns>
        public static bool DeleteDirtory(string strPath)
        {
            string path = GetMapPath(strPath);
            if (path == null) return false;
            try
            {
                if (Directory.Exists(path) == false)
                {
                    return false;
                }
                if (String.IsNullOrEmpty(path))
                {
                    Directory.Delete(path, true);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //string temp = error.ToString();
                return false;
            }
        }

        /// <summary>
        /// 删除指定目录及其下面所有子目录
        /// </summary>
        /// <param name="dir">要删除的路径</param>
        /// <param name="isDelFolder">是否删除目录本身</param>
        /// <returns></returns>
        public static string DeleteDirtory(string dir, bool isDelFolder)
        {
            if (String.IsNullOrEmpty(dir))
            {
                return "path is null";
            }
            dir = GetMapPath(dir);
            //dir = RequestHelper.GetVirtualDirectory() + dir;

            try
            {
                if (Directory.Exists(dir)) //如果存在这个文件夹删除之    
                {
                    foreach (string d in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                           
                        else
                            DeleteDirtory(d, true); //递归删除子文件夹    
                    }
                    if (isDelFolder)
                    {
                        Directory.Delete(dir, true); //删除已空文件夹                    
                    }
                }
            }
            catch (Exception)
            {
                //string strRemark = "\n路径：" + dir;
                //Log.WriteErrorLog(ex, "", "公用的删除文件", strRemark, false);                
                return "Error";
            }
            return "succeed";
        }

        /// <summary>
        /// 删除指定文件(删除之前先将文件的只读属性更改为普通属性)
        /// </summary>
        /// <param name="fileName">要删除的文件名</param>
        /// <returns></returns>
        public static bool DeleteFile(string fileName)
        {
            string filePath = HttpContext.Current.Server.MapPath(fileName);

            if (File.Exists(filePath) == false)
            {
                return false;
            }
            var fi = new FileInfo(filePath) { Attributes = FileAttributes.Normal };
            //filesystemobject 
            if (fileName != null)
            {
                if (fileName.Tostring().Trim() != "")
                {
                    try
                    {
                        if (fi.Attributes.ToString().ToLower().IndexOf("readonly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                            File.Delete(filePath);
                        }
                        else
                        {
                            File.Delete(filePath);
                        }
                    }
                    catch (Exception)
                    {
                        //string temp = ex.ToString();
                        return false;
                    }
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 除pat根据创建时间删h下所有文件
        /// </summary>
        /// <param name="path">目录</param>
        /// <param name="days">天数（几天前的数据）</param>
        /// <returns></returns>
        public static bool DeleteFileByCreateTime(string path, int days)
        {
            var isOk = true;
            string paths = HttpContext.Current.Server.MapPath(path);
            var di = new DirectoryInfo(paths);
            FileInfo[] fiA = di.GetFiles();
            DateTime dt1 = DateTime.Now;
            foreach (FileInfo t in fiA)
            {
                DateTime dt2 = t.CreationTime;
                int dnum = (dt1 - dt2).Days.ToInt(0);
                if (dnum > days)
                {
                    try
                    {
                        DeleteFile(path + t.Name);
                    }
                    catch (Exception)
                    {
                        isOk = false;
                    }
                }
            }
            return isOk;
        }

        /// <summary>
        /// 查看文件是否存在
        /// </summary>
        /// <param name="fileName">要查看的文件的相对路径</param>
        /// <returns></returns>
        public static bool IsHaveFile(string fileName)
        {
            string pathName = GetMapPath(fileName);
            //string pathName = RequestHelper.GetVirtualDirectory() + filePath + "\\" + fileName;
            return File.Exists(pathName);
        }

        /// <summary>
        /// 查看文件夹下是否有文件
        /// </summary>
        /// <param name="dir">要查看的文件夹的相对路径</param>
        /// <returns></returns>
        public static bool IsExistsFileInDir(string dir)
        {
            return Directory.Exists(dir) && Directory.GetFiles(dir).Any(File.Exists);
        }

        /// <summary>
        /// 以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            var buffer = new Byte[10000];

            // 文件长度

            // 需要读的数据长度

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                long dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Encrypt.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 返回指定目录下的非 UTF8 字符集文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件名的字符串数组</returns>
        public static string[] FindNoUtf8File(string path)
        {
            //System.IO.StreamReader reader = null;
            var filelist = new StringBuilder();
            var folder = new DirectoryInfo(path);
            //System.IO.DirectoryInfo[] subFolders = Folder.GetDirectories(); 
            /*
            for (int i=0;i<subFolders.Length;i++) 
            { 
                FindNoUTF8File(subFolders[i].FullName); 
            }
            */
            FileInfo[] subFiles = folder.GetFiles();
            foreach (FileInfo t in subFiles)
            {
                if (!t.Extension.ToLower().Equals(".htm")) continue;
                var fs = new FileStream(t.FullName, FileMode.Open, FileAccess.Read);
                bool bUtf8 = IsUtf8(fs);
                fs.Close();
                if (bUtf8) continue;
                filelist.Append(t.FullName);
                filelist.Append("\r\n");
            }
            return filelist.ToString().ToArray("\r\n");
        }

        /// <summary>
        /// 判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUtf8(Stream sbInputStream)
        {
            int i;
            bool bAllAscii = true;
            long iLen = sbInputStream.Length;

            byte cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                var chr = (byte)sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        }
                        while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            return !bAllAscii;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return MakeSureDirectoryPathExists(name);
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            if (!File.Exists(backupFileName))
            {
                throw new FileNotFoundException(backupFileName + "文件不存在！");
            }
            if (backupTargetFileName != null)
            {
                if (!File.Exists(targetFileName))
                {
                    throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                }
                File.Copy(targetFileName, backupTargetFileName, true);
            }
            File.Delete(targetFileName);
            File.Copy(backupFileName, targetFileName);
            return true;
        }

        /// <summary>
        /// 恢复文件(不再备份恢复文件)
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <returns></returns>
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        /// 转换长文件名为短文件名
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="repstring"></param>
        /// <param name="leftnum"></param>
        /// <param name="rightnum"></param>
        /// <param name="charnum"></param>
        /// <returns></returns>
        public static string ConvertSimpleFileName(string fullname, string repstring, int leftnum, int rightnum, int charnum)
        {
            string simplefilename;

            string extname = GetFileExtName(fullname);
            if (string.IsNullOrEmpty(extname))
            {

                throw new Exception("字符串不含有扩展名信息");
            }

            int dotindex = fullname.LastIndexOf('.');
            string filename = fullname.Substring(0, dotindex);
            int filelength = filename.Length;
            if (dotindex > charnum)
            {
                string leftstring = filename.Substring(0, leftnum);
                string rightstring = filename.Substring(filelength - rightnum, rightnum);
                if (string.IsNullOrEmpty(repstring))
                {
                    simplefilename = leftstring + rightstring + "." + extname;
                }
                else
                {
                    simplefilename = leftstring + repstring + rightstring + "." + extname;
                }
            }
            else
            {
                simplefilename = fullname;
            }
            return simplefilename;

        }

        /// <summary>
        /// 获取文件名的扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileExtName(string filename)
        {
            string[] array = filename.Trim().Split('.');
            Array.Reverse(array);
            return array[0];
        }

        /// <summary>
        /// 获取文件夹下所有的文件个数
        /// </summary>
        /// <param name="path">文件夹地址</param>
        /// <returns>文件个数</returns>
        public static List<string> GetAllFiles(string path)
        {
            var ret = new List<string>();
            ret.AddRange(Directory.GetFiles(path));
            Array.ForEach(Directory.GetDirectories(path),
                          path1 => ret.AddRange(GetAllFiles(path1)));
            return ret;
        }

        /// <summary>
        /// 生成随机文件名
        /// </summary>
        /// <returns></returns>
        public static string RandomFile()
        {
            string str = Guid.NewGuid().ToString();
            string[] str1 = str.Split('-');
            string num = str1[1];
            string id = num;
            string t = str1[0];
            string cf = str1[0];
            id = id + "_" + t + "_" + cf;
            return id;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        public static void CreateFile(string fileName, string content)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            CreateOrAppendFile(fileName, content, false);
        }

        /// <summary>
        /// 写入文件(新建文件或者追加内容)
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        /// <param name="append">是否为追加文件，为false时创建或覆盖原有文件</param>
        private static void CreateOrAppendFile(string fileName, string content, bool append)
        {
            FileMode fileMode = FileMode.Create;
            FileAccess fileAccess = FileAccess.ReadWrite;
            if (append)
            {
                fileMode = FileMode.Append;
                fileAccess = FileAccess.Write;
            }
            if (fileName.IndexOf('/') > -1)
                fileName = GetMapPath(fileName);
            string dir = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (fileMode == FileMode.Append && !File.Exists(fileName))
            {
                CreateFile(fileName, "");
            }

            using (var fs = new FileStream(fileName, fileMode, fileAccess, FileShare.ReadWrite))
            {
                byte[] info = Encoding.UTF8.GetBytes(content);
                fs.Write(info, 0, info.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取文件中的内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件内容</returns>
        public static string GetFileContent(string fileName)
        {
            string result = string.Empty;
            if (fileName.IndexOf('/') > -1)
                fileName = GetMapPath(fileName);
            if (!File.Exists(fileName)) return result;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        /// <summary>
        /// 根据文件名获取文件ContentType
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetContentType(string fileName)
        {
            const string defaultContentType = "application/unknown";
            string[] array = fileName.Split('.');
            string result;
            string suffix = "." + array[array.Length - 1];
            Microsoft.Win32.RegistryKey rg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(suffix);
            if (rg != null)
            {
                object obj = rg.GetValue("Content Type", defaultContentType);
                rg.Close();
                result = obj != null ? obj.ToString() : string.Empty;
            }
            else
            {
                result = defaultContentType;
            }
            return result;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="page">指定当前页即可，用this</param>
        /// <param name="fileName">下载时要显示的文件名称</param>
        /// <param name="fileNameUrl">文件所存储的网站相对路径包括文件和扩展名</param>
        /// <param name="charset">输出流的Http字符集 如utf-8,gb2312</param>
        public static bool Download(System.Web.UI.Page page, string fileName, string fileNameUrl, string charset)
        {
            fileNameUrl = page.Server.MapPath(fileNameUrl);
            //fileNameUrl = URequest.GetVirtualDirectory() + fileNameUrl;
            if (File.Exists(fileNameUrl))
            {
                string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
                page.Response.Charset = charset;
                page.Response.ContentEncoding = Encoding.UTF8;

                //page.Response.ContentType = "application/x-zip-compressed";
                page.Response.ContentType = GetContentType(fileName);
                if (curBrowser.CutString(0, 7) == "firefox")
                {
                    string filename = HttpUtility.UrlDecode(fileName, Encoding.UTF8);
                    filename = System.Text.RegularExpressions.Regex.Replace(filename, @"\s", "");
                    page.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                }
                else
                {
                    page.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(fileName));
                }

                try
                {
                    page.Response.TransmitFile(fileNameUrl);
                    return true;
                }
                catch
                {
                    //MessBox.ShowUrlRedirectR(page, "../Error.htm");
                    page.Response.Close();
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
    }
}
