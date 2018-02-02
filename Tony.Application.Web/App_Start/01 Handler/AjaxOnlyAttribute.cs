using System;
using System.Reflection;
using System.Web.Mvc;

namespace Tony.Application.Web
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：仅允许Ajax操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute:ActionMethodSelectorAttribute
    {
        /// <summary>
        /// 跳过Ajax检测
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 初始化仅允许Ajax操作
        /// </summary>
        /// <param name="ignore">跳过Ajax检测</param>
        public AjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        /// <summary>
        /// 验证请求有效性
        /// </summary>
        /// <param name="controllerContext">控制器上下文</param>
        /// <param name="methodInfo">方法</param>
        /// <returns></returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (Ignore) return true;
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}