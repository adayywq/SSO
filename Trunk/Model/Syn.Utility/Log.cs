using System;
using System.IO;
using System.Web;
using Syn.Utility.Function;

namespace Syn.Utility
{
    public class Log
    {
        /// <summary>
        /// 输出Debug日志(如果需要配置开关，请在comfig中配置DebugLogSwitch节点值为“on”或“off”。日志保存在网站根目录下的LogInfo文件夹下)
        /// </summary>
        /// <param name="message">输出信息</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void WriteDebugLog(string coder,string message)
        {
            string debugLogStatus =Utils. GetConfigParam("DebugLog", "on");

            if (String.IsNullOrEmpty(debugLogStatus) || (debugLogStatus != "on"))
            {
                return;
            }

            string mCoder = "Coder：" + coder;
            string mIp = "用户IP：" + RequestHelper.GetIp();
            string mTime = "发生时间：" + DateTime.Now;
            string mPage = "发生页地址：" + RequestHelper.GetUrl();
            string mInfo = "输出信息：" + message;
            //独占方式，因为文件只能由一个进程写入.
            StreamWriter sw = null;
            try
            {
                //分别取出需要的年，月，日
                DateTime nowDate = DateTime.Now;
                string nowYear = nowDate.Year.ToString();
                string nowMonth = nowDate.Month.ToString();
                if (nowDate.Month < 10)
                {
                    nowMonth = "0" + nowMonth;
                }
                string nowDay = nowDate.Day.ToString();
                if (nowDate.Day < 10)
                {
                    nowDay = "0" + nowDay;
                }

                // 写入日志
                const string fileName = "debug.txt";
                string logPath = HttpContext.Current.Server.MapPath("\\LogInfo\\" + nowYear + "_" + nowMonth + "\\" + nowDay);
                //如果目录不存在则创建
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                var fi = new FileInfo(logPath + "\\" + fileName);
                //文件不存在就创建,true表示追加
                sw = new StreamWriter(fi.FullName, true);
                sw.WriteLine(mCoder);
                sw.WriteLine(mIp);
                sw.WriteLine(mTime);
                sw.WriteLine(mPage);
                sw.WriteLine(mInfo);
                sw.WriteLine("--------------------------------------------------------------------------------------------");
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// 输出Error日志(如果需要配置开关，请在comfig中配置ErrorLogSwitch节点值为“on”或“off”。日志保存在网站根目录下的LogInfo文件夹下)
        /// </summary>
        /// <param name="objErr">错误信息</param>
        /// <param name="coder">作者名称</param>
        /// <param name="remark">备注(如果有用户ID,请务必传入)</param>
        public static void WriteErrorLog(Exception objErr, string coder, string remark)
        {
            string errorLogStatus = Utils.GetConfigParam("ErrorLog", "on");
            if (String.IsNullOrEmpty(errorLogStatus) || (errorLogStatus != "on"))
            {
                return;
            }

            string errorCoder = "Coder：" + coder;
            string errorIp = "用户IP：" + RequestHelper.GetIp();
            string errorTime = "发生时间：" + DateTime.Now;
            string errorPage = "发生异常页：" + RequestHelper.GetUrl();
            string errorInfo = "异常信息：" + objErr.Message;
            string errorSource = "错误源：" + objErr.Source;
            string errorTrace = "堆栈信息：" + objErr.StackTrace;
            string errorTargetSite = "引发异常的方法" + objErr.TargetSite;
            string innerException = "引发异常的实例" + objErr.InnerException;
            string errorRemark = "备注：" + remark;
            //独占方式，因为文件只能由一个进程写入.
            StreamWriter sw = null;
            const string fileName = "error.txt";
            try
            {
                //分别取出需要的年，月，日
                DateTime nowDate = DateTime.Now;
                string nowYear = nowDate.Year.ToString();
                string nowMonth = nowDate.Month.ToString();
                if (nowDate.Month < 10)
                {
                    nowMonth = "0" + nowMonth;
                }
                string nowDay = nowDate.Day.ToString();
                if (nowDate.Day < 10)
                {
                    nowDay = "0" + nowDay;
                }

                // 写入日志
                string logPath = HttpContext.Current.Server.MapPath("\\LogInfo\\" + nowYear + "_" + nowMonth + "\\" + nowDay);
                //如果目录不存在则创建
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                var fi = new FileInfo(logPath + "\\" + fileName);
                //文件不存在就创建,true表示追加
                sw = new StreamWriter(fi.FullName, true);
                sw.WriteLine(errorCoder);
                sw.WriteLine(errorIp);
                sw.WriteLine(errorTime);
                sw.WriteLine(errorPage);
                sw.WriteLine(errorInfo);
                sw.WriteLine(errorSource);
                sw.WriteLine(errorTrace);
                sw.WriteLine(errorRemark);
                sw.WriteLine(errorTargetSite);
                sw.WriteLine(innerException);
                sw.WriteLine("--------------------------------------------------------------------------------------------");                
            }
            finally
            {
                if (sw != null)
                    sw.Close();

                throw objErr;
            }
        }
    }
}
