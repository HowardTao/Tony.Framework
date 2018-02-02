using Tony.Application.Entity.BaseManage;
using Tony.Util.WebControl;
using System.Collections.Generic;

namespace Tony.Application.IService.BaseManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：部门信息表
    /// </summary>
    public interface IDepartmentService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<DepartmentEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        IEnumerable<DepartmentEntity> GetList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DepartmentEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, DepartmentEntity entity);
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">部门编号</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue);
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">部门名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue); 
        #endregion
    }
}
