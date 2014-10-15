namespace Syn.Utility.Function
{
    /// <summary>
    /// Author:AutoTech
    /// 数据转换
    /// </summary>
    public static class ConvertHelper
    {
        #region 数据转换
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="typeName">转换后的数据类型</param>
        /// <param name="sourceData">要转换的数据</param>
        /// <returns></returns>
        public static object Convert(string typeName,object sourceData)
        {
            switch (typeName.ToLower())
            {
                case "string":
                    return sourceData.ToString();
                case "int32":
                    return System.Convert.ToInt32(sourceData.ToString());
                case "int":
                    return System.Convert.ToInt32(sourceData.ToString());
                case "int16":
                    return System.Convert.ToInt16(sourceData.ToString());
                case "datetime":
                    return System.Convert.ToDateTime(sourceData.ToString());
                case "long":
                    return System.Convert.ToInt64(sourceData.ToString());
                case "double":
                    return System.Convert.ToDouble(sourceData.ToString());
                case "bool":
                    return System.Convert.ToBoolean(sourceData.ToString());
                case "boolean":
                    return System.Convert.ToBoolean(sourceData.ToString());
                default:
                    return sourceData;
            }
        }
        #endregion

    }
}
