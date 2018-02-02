using System;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Data.Repository;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Tony.Util.Extension;

namespace Tony.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：部门信息表
    /// </summary>
    public class DepartmentService : RepositoryFactory<DepartmentEntity>, IDepartmentService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DepartmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            return BaseRepository().IQueryable().OrderByDescending(t=>t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string keyValue)
        {
            return BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            var count = BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
            {
                throw new Exception("当前所选数据有子节点数据！");
            }
            BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DepartmentEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                BaseRepository().Insert(entity);
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">部门编号</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return BaseRepository().IQueryable(expression).Count() == 0;
        }

        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">部门名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return BaseRepository().IQueryable(expression).Count() == 0;
        }

        #endregion
    }
}
