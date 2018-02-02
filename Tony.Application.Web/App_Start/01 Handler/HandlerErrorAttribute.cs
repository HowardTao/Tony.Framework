using System;
using System.Web;
using System.Web.Mvc;
using Tony.Application.Business.SystemManage;
using Tony.Application.Code;
using Tony.Application.Entity.SystemManage;
using Tony.Util;
using Tony.Util.Logs;
using Tony.Util.WebControl;

namespace Tony.Application.Web
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：错误日志（Controller发生异常时会执行这里） 
    /// </summary>
    public class HandlerErrorAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            WriteLog(filterContext);
            base.OnException(filterContext);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.Result = new ContentResult()
            {
                Content = new AjaxResult() {type = ResultType.error, message = filterContext.Exception.Message}.ToJson()
            };
        }
        /// <summary>
        /// 写入日志(log4net)
        /// </summary>
        /// <param name="context"></param>
        private void WriteLog(ExceptionContext context)
        {
            if(context==null) return;
            if(OperatorProvider.Provider.IsOverdue()) return;
            var log = LogFactory.GetLogger(context.Controller.ToString());
            var error = context.Exception;
            var logMessage = new LogMessage
            {
                OperationTime = DateTime.Now,
                Url = HttpContext.Current.Request.RawUrl,
                Class = context.Controller.ToString(),
                Ip = Net.Ip,
                Host = Net.Host,
                Browser = Net.Browser,
                UserName = OperatorProvider.Provider.Current().Account + "(" +
                           OperatorProvider.Provider.Current().UserName + ")",
                ExceptionInfo = error.InnerException == null ? error.Message : error.InnerException.Message
            };
            var strMessage = new LogFormat().ExceptionFormat(logMessage);
            log.Error(strMessage);
            var logEntity = new LogEntity
            {
                CategoryId = 4,
                OperateTypeId = ((int) OperationType.Exception).ToString(),
                OperateType = EnumAttribute.GetDescription(OperationType.Exception),
                OperateAccount = logMessage.UserName,
                OperateUserId = OperatorProvider.Provider.Current().UserId,
                ExecuteResult = -1,
                ExecuteResultJson = strMessage
            };
            logEntity.WriteLog();
        }
    }
}