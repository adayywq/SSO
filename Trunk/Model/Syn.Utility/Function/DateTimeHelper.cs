using System;
using System.Text.RegularExpressions;

namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 日期操作
    /// </summary>
    public static class DateTimeHelper
    {
        #region 返回当前日期的标准格式(yyyy-MM-dd)
        /// <summary>
        /// 返回当前日期的标准格式(yyyy-MM-dd)
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion 

        #region 将指定日期字符串转换为标准格式
        /// <summary>
        /// 将指定日期字符串转换为标准格式
        /// </summary>
        /// <param name="datetimestr">日期字符串</param>
        /// <param name="replacestr">默认日期格式</param>
        /// <returns>返回指定日期</returns>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;

        }
        #endregion

        #region 返回标准时间格式(HH:mm:ss)
        /// <summary>
        /// 返回标准时间格式(HH:mm:ss)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        #endregion

        #region 返回标准时间格式(yyyy-MM-dd HH:mm:ss)
        /// <summary>
        /// 返回标准时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 返回相对于当前时间的相对天数
        /// <summary>
        /// 返回相对于当前时间的相对天数(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="relativeday">相对天数</param>
        /// <returns></returns>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 返回标准时间格式(yyyy-MM-dd HH:mm:ss:fffffff)
        /// <summary>
        /// 返回标准时间格式(yyyy-MM-dd HH:mm:ss:fffffff)
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
        #endregion

        #region 返回标准时间(yyyy-MM-dd)
        /// <summary>
        /// 返回标准时间(yyyy-MM-dd)
        /// </summary>
        /// <param name="fDate">指定的时间</param>
        /// <returns>返回的时间</returns>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }
        #endregion

        #region 返回标准时间(yyyy-MM-dd HH:mm:ss)
        /// <summary>
        /// 返回标准时间(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="fDateTime">指定时间</param>
        /// <param name="formatStr">时间格式 如yyyy-MM-dd HH:mm:ss</param>
        /// <returns>返回标准时间</returns>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }
        #endregion

        #region 返回标准时间(yyyy-MM-dd HH:mm:ss)
        /// <summary>
        /// 返回标准时间(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="fDateTime">指定的时间</param>
        /// <returns>返回的时间</returns>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        #endregion 

        #region 计算两个时间的间隔
        /// <summary>
        /// 计算两个时间的间隔
        /// </summary>
        /// <param name="dateType">date1 和 date2 的时间差的时间间隔</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public static double DateDiff(int dateType, DateTime startDate, DateTime endDate)
        {
            return DateDiff((DateType)dateType, startDate, endDate);
        }
        #endregion

        #region 计算两个时间的间隔
        /// <summary>
        /// 计算两个时间的间隔
        /// </summary>
        /// <param name="dateType">时间类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public static double DateDiff(DateType dateType, DateTime startDate, DateTime endDate)
        {
            double result = 0;
            var timeSpan = new TimeSpan(endDate.Ticks - startDate.Ticks);
            switch (dateType)
            {
                case DateType.Year:
                    result = timeSpan.TotalDays / 365;
                    break;
                case DateType.Month:
                    result = (timeSpan.TotalDays / 365) * 12;
                    break;
                case DateType.Day:
                    result = timeSpan.TotalDays;
                    break;
                case DateType.Hour:
                    result = timeSpan.TotalHours;
                    break;
                case DateType.Minute:
                    result = timeSpan.TotalMinutes;
                    break;
                case DateType.Second:
                    result = timeSpan.TotalSeconds;
                    break;
            }
            return result;
        }
        #endregion

        #region 返回相差的秒数
        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string time, int sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddSeconds(sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalSeconds;
        }
        #endregion

        #region 返回相差的秒数
        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            return ts.TotalMinutes < int.MinValue ? int.MinValue : (int) ts.TotalMinutes;
        }
        #endregion

        #region 返回相差的小时数
        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalHours;
        }
        #endregion 

        #region 判断是否是时间格式
        /// <summary>
        /// 判断是否是时间格式
        /// </summary>
        /// <param name="timeval">指定的时间</param>
        /// <returns>true或者false</returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        #endregion

        #region 验证日期格式是否正确
        /// <summary>
        /// 验证日期格式是否正确(格式：YYYY-MM-DD[包括验证瑞年、二月等情况])
        /// </summary>
        /// <param name="date"></param>
        /// <returns>正确返回true</returns>
        public static bool IsDate(string date)
        {
            const string dateFormat = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            return Regex.IsMatch(date, dateFormat);
        }
        #endregion

        #region 转换时间为unix时间戳
        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
        #endregion

        #region 根据时间戳计算对应日期
        /// <summary>
        /// 根据时间戳计算对应日期
        /// </summary>
        /// <param name="tick">时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(long tick)
        {
            string timeStamp = tick.ToString();
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            var toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
        #endregion

        #region 计算时间戳
        /// <summary>
        /// 计算时间戳
        /// </summary>
        /// <param name="dt">目标时间</param>
        /// <returns></returns>
        public static long ToTick(DateTime dt)
        {
            if (dt == DateTime.MinValue) return 0;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            long i;
            long.TryParse(timeStamp, out i);
            return i;
        }
        #endregion

        #region 返回当前日期是星期几
        /// <summary>
        /// 返回当前日期是星期几，为星期的数字字符
        /// </summary>
        /// <param name="userDate">要获取的日期</param>
        /// <returns></returns>
        public static string GetWeeks(DateTime userDate)
        {
            string userWeeks = userDate.DayOfWeek.ToString();
            string retWeeks = "0";
            switch (userWeeks)
            {
                case "Monday":
                    retWeeks = "1";
                    break;
                case "Tuesday":
                    retWeeks = "2";
                    break;
                case "Wednesday":
                    retWeeks = "3";
                    break;
                case "Thursday":
                    retWeeks = "4";
                    break;
                case "Friday":
                    retWeeks = "5";
                    break;
                case "Saturday":
                    retWeeks = "6";
                    break;
                case "Sunday":
                    retWeeks = "7";
                    break;
            }
            return retWeeks;
        }
        #endregion

        #region 枚举时间 年 月 日 时 分 秒
        /// <summary>
        /// 枚举时间 年 月 日 时 分 秒
        /// </summary>
        public enum DateType
        {
            /// <summary>
            /// 年
            /// </summary>
            Year,
            /// <summary>
            /// 月
            /// </summary>
            Month,
            /// <summary>
            /// 日
            /// </summary>
            Day,
            /// <summary>
            /// 时
            /// </summary>
            Hour,
            /// <summary>
            /// 分
            /// </summary>
            Minute,
            /// <summary>
            /// 秒
            /// </summary>
            Second
        }
        #endregion

    }
}
