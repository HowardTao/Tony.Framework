using System;

namespace Tony.Util.Extension
{
	/// <summary>
    /// 类型转换扩展
    /// </summary>
   public  static partial class Extensions
    {
        #region 数字转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            if (data == null) return 0;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success) return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null) return null;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success) return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == null) return 0;
            double result;
            return double.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null) return null;
            double result;
            var success = double.TryParse(data.ToString(), out result);
            if (success) return result;
            return null;
        }
        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            if(data==null) return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : DateTime.MinValue;
        }
        #endregion

        #region 布尔转换
        private static bool? GetBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true ;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;

            }
        }
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null) return false;
            var value = GetBool(data);
            if (value != null) return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }
        #endregion

        #region 字符串转换

        #endregion
    }
}
