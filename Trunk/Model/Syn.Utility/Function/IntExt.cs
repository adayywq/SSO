namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 数字操作扩展类 
    /// </summary>
    public static class IntExt
    {
        #region 格式化字节数字符串

        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes">字节数字字符串</param>
        /// <returns></returns>
        public static string FormatBytesString(this int bytes)
        {
            if (bytes > 1073741824)
            {
                // ReSharper disable PossibleLossOfFraction
                return string.Format("{0}G", ((double) (bytes/1073741824)).ToString("0"));
                // ReSharper restore PossibleLossOfFraction
            }
            if (bytes > 1048576)
            {
                // ReSharper disable PossibleLossOfFraction
                return string.Format("{0}M", ((double) (bytes/1048576)).ToString("0"));
                // ReSharper restore PossibleLossOfFraction
            }
            if (bytes > 1024)
            {
                // ReSharper disable PossibleLossOfFraction
                return string.Format("{0}K", ((double) (bytes/1024)).ToString("0"));
                // ReSharper restore PossibleLossOfFraction
            }
            return string.Format("{0}Bytes", bytes);
        }

        #endregion
    }
}