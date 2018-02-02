using System;
using System.Diagnostics;
using System.Text;

namespace Tony.Util
{
    /// <summary>
    /// 常用公共类
    /// </summary>
   public class CommonHelper
    {
        #region Stopwatch计时器
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            var watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }

        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
        #endregion

        #region 自动生成编号
        /// <summary>
        /// 自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            var random = new Random();
            var strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            var code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
        #endregion

        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int codeNum)
        {
            var sb = new StringBuilder();
            var rand = new Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                var t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();
        }
        #endregion

        #region 删除最后一个字符之后的字符
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(",", StringComparison.Ordinal));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar, StringComparison.Ordinal));
        }

        /// <summary>
        /// 删除最后结尾的长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string DelLastLength(string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Substring(0, str.Length - length);
            return str;
        } 
        #endregion
    }
}
