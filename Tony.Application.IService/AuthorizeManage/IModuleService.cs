using Tony.Application.Entity.AuthorizeManage;
using Tony.Util.WebControl;
using System.Collections.Generic;

namespace Tony.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：系统功能
    /// </summary>
    public interface IModuleService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ModuleEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        IEnumerable<ModuleEntity> GetList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ModuleEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ModuleEntity entity);
        #endregion
    }
}
