using System.Net;
using System.Net.Sockets;
using System.Web;
using Tony.Util.Extension;

namespace Tony.Util
{
   public class Net
    {
        #region 获取Ip
        /// <summary>
        /// 获取Ip
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = "";
                if (HttpContext.Current != null) result = GetWebClientIp();
                if (result.IsEmpty()) result = GetLanIp();
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端的Ip
        /// </summary>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork) return hostAddress.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        private static string GetWebRemoteIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                   HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork) return hostAddress.ToString();
            }
            return "";
        }
        #endregion

        #region 获取主机名

        /// <summary>
        /// 获取主机名
        /// </summary>
        public static string Host
        {
            get { return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName(); }
        }

        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal) return "";
            var ip = GetWebRemoteIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain") result = Dns.GetHostName();
            return result;
        }
        #endregion

        #region 获取浏览器信息

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null) return "";
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }
        #endregion
    }
}
