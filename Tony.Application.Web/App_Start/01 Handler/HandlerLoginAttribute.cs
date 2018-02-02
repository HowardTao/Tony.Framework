using System.Web.Mvc;
using Tony.Application.Code;
using Tony.Util;
using Tony.Util.Extension;

namespace Tony.Application.Web
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute:AuthorizeAttribute
    {
        private readonly LoginMode _customMode;
        public HandlerLoginAttribute(LoginMode mode)
        {
            _customMode = mode;
        }

        /// <summary>
        ///  响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登陆拦截是否忽略
            if(_customMode == LoginMode.Ignore) return;
            if (OperatorProvider.Provider.IsOverdue())
            {
                //登陆已超时，请重新登陆
                WebHelper.WriteCookie("tony_login_error","Overdue");
                filterContext.Result = new RedirectResult("~/Login/Default");
                return;
            }
            var isOnLine = OperatorProvider.Provider.IsOnLine();
            if (isOnLine == 0)
            {
                var checkOnLine = Config.GetValue("CheckOnLine").ToBool();//是否允许重复登陆
                if (!checkOnLine)
                {
                    //您的帐号已在其它地方登录,请重新登录
                    WebHelper.WriteCookie("tony_login_error", "OnLine");
                    //filterContext.Result = new RedirectResult("~/Login/Default");
                    filterContext.Result = new RedirectResult("https://bpm.redsun.com.cn:19088/sso.aspx?");
                }
            }
            else if (isOnLine == -1)
            {
                //缓存已超时，请重新登陆
                WebHelper.WriteCookie("tony_login_error", "-1");
            }
        }
    }
}