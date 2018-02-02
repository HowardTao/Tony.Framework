using System.Collections.Generic;
using Tony.Application.Entity.SystemManage;
using Tony.Util.WebControl;

namespace Tony.Application.IService.SystemManage
{
    /// <summary>
    /// 系统日志
    /// </summary>
   public interface ILogService
    {
        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        LogEntity GetEntity(string keyValue);

        #endregion

        #region 提交数据
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logEntity">实体对象</param>
        void WriteLog(LogEntity logEntity);
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间</param>
        void RemoveLog(int categoryId, string keepTime);

        #endregion
    }
}
