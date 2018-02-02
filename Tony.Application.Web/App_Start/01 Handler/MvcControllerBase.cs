using System.Web.Mvc;
using Tony.Application.Code;
using Tony.Util;
using Tony.Util.Logs;
using Tony.Util.WebControl;

namespace Tony.Application.Web
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：控制器基类
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class MvcControllerBase:Controller
    {
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(GetType().ToString())); }
        }

        /// <summary>
        /// 返回json数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult {type = ResultType.success, message = message}.ToJson());
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message,object data)
        {
            return Content(new AjaxResult {type = ResultType.success, message = message, resultdata = data}.ToJson());
        }
        /// <summary>
        /// 返回失败信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult {type = ResultType.error, message = message}.ToJson());
        }
    }
}