using Tony.Application.Entity.SystemManage;
using Tony.Util.WebControl;
using System.Collections.Generic;

namespace Tony.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-26 15:58
    /// 描 述：行政区域管理
    /// </summary>
    public interface IAreaService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        IEnumerable<AreaEntity> GetList();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IEnumerable<AreaEntity> GetList(string parentId, string keyword);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        AreaEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, AreaEntity entity);
        #endregion

    }
}
