using System;
using System.Collections.Generic;
using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Data.Repository;
using Tony.Util;
using Tony.Util.Extension;
using Tony.Util.WebControl;

namespace Tony.Application.Service.SystemManage
{

    /// <summary>
    /// 系统日志
    /// </summary>
    public class LogService : RepositoryFactory<LogEntity>, ILogService
    {

        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LogEntity GetEntity(string keyValue)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logEntity">实体对象</param>
        public void WriteLog(LogEntity logEntity)
        {
            logEntity.LogId = Guid.NewGuid().ToString();
            logEntity.OperateTime = DateTime.Now;
            logEntity.DeleteMark = 0;
            logEntity.EnabledMark = 1;
            logEntity.IPAddress = Net.Ip;
            logEntity.Host = Net.Host;
            logEntity.Browser = Net.Browser;
            BaseRepository().Insert(logEntity);
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间</param>
        public void RemoveLog(int categoryId, string keepTime)
        {
            var operateTime = DateTime.Now;
            switch (keepTime)
            {
                case "7":
                    operateTime = DateTime.Now.AddDays(-7);
                    break;
                case "1":
                    operateTime = DateTime.Now.AddMonths(-1);
                    break;
                case "3":
                    operateTime = DateTime.Now.AddMonths(-3);
                    break;
            }
            var expression = LinqExtensions.True<LogEntity>();
            expression = expression.And(t => t.OperateTime <= operateTime);
            expression = expression.And(t => t.CategoryId == categoryId);
            BaseRepository().Delete(expression);
        }
        #endregion
    }
}
