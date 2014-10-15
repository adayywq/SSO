namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 字符操作扩展类
    /// </summary> 
    public static class CharExt
    {
        #region 判断字符是否为中文

        /// <summary>
        /// 判断字符是否为中文
        /// </summary>
        /// <param name="c">输入的字符</param>
        /// <returns></returns>
        public static bool IsChinese(this char c)
        {
            return c >= 0x4E00 && c <= 0x9FA5;
        }

        #endregion
    }
}