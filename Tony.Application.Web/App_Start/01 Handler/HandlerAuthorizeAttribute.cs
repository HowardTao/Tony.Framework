
using System.Web;
using System.Web.Mvc;
using Tony.Application.Business.AuthorizeManage;
using Tony.Application.Code;
using Tony.Util;
using Tony.Util.Extension;

namespace Tony.Application.Web
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：（权限认证+安全）拦截组件
    /// </summary>
    public class HandlerAuthorizeAttribute:ActionFilterAttribute
    {
        private readonly PermissionMode _customMode;

        public HandlerAuthorizeAttribute(PermissionMode mode)
        {
            _customMode = mode;
        }

        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(OperatorProvider.Provider.Current().IsSystem) return;
            if(_customMode == PermissionMode.Ignore) return;
            if (!FilterIP())
            {
                var content = new ContentResult
                {
                    Content = "<script type='text/javascript'>alert('很抱歉！您当前所在IP被系统拒绝访问！');top.Loading(false);</script>"
                };
                filterContext.Result = content;
                return;
            }
            if (!FilterTime())
            {
                var content = new ContentResult
                {
                    Content = "<script type='text/javascript'>alert('很抱歉！系统不允许您在当前时段访问！');top.Loading(false);</script>"
                };
                filterContext.Result = content;
                return;
            }
            if (!ActionAuthorize(filterContext))
            {
                var content = new ContentResult
                {
                    Content = "<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');top.Loading(false);</script>"
                };
                filterContext.Result = content;
            }
            
        }

        /// <summary>
        /// IP过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterIP()
        {
            var isFilterIp = Config.GetValue("FilterIP").ToBool();
            return true;
        }
        /// <summary>
        /// 时段过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterTime()
        {
            var isFilterTime = Config.GetValue("FilterTime").ToBool();
            return true;
        }

        private bool ActionAuthorize(ActionExecutingContext filterContext)
        {
            var currentUrl = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
            return new AuthorizeBll().ActionAuthorize(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId,
                currentUrl);
        }
    }
}