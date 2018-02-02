using Tony.Util;

namespace Tony.Application.Code
{
    /// <summary>
    /// 系统信息
    /// </summary>
   public class SystemInfo
    {
        public static string CurrentUserId { get { return OperatorProvider.Provider.Current().UserId; } }

        public static string CurrentModuleId { get { return WebHelper.GetCookie("currentmoduleId"); } }
    }
}
