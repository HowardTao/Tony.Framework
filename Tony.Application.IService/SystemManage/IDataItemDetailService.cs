using Tony.Application.Entity.SystemManage;
using Tony.Util.WebControl;
using System.Collections.Generic;
using Tony.Application.Entity.SystemManage.ViewModel;

namespace Tony.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-24 08:57
    /// 描 述：数据字典明细表
    /// </summary>
    public interface IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<DataItemDetailEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        IEnumerable<DataItemDetailEntity> GetList();
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataItemModel> GetDataItemList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DataItemDetailEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, DataItemDetailEntity entity);
        #endregion


    }
}
