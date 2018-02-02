using System;
using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Application.Service.SystemManage;

namespace Tony.Application.Business.SystemManage
{
   public static class LogBll
    {
        private static ILogService service = new LogService();
        public static void WriteLog(this LogEntity logEntity)
        {
            try
            {
                service.WriteLog(logEntity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
