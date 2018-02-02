using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Data.Repository;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Tony.Util;
using Tony.Util.Extension;

namespace Tony.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：角色信息表
    /// </summary>
    public class RoleService : RepositoryFactory<RoleEntity>, IRoleService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                var condition = queryParam["condition"].ToString();
                var keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":      //角色编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":   //角色名称     
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                }
            }
            expression = expression.And(t => t.Category == 1);
            return BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 1).And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoleEntity GetEntity(string keyValue)
        {
            return BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 1).And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId == keyValue);
            }
            return BaseRepository().IQueryable(expression).Count() == 0;
        }

        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 1).And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId == keyValue);
            }
            return BaseRepository().IQueryable(expression).Count() == 0;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoleEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.Category = 1;
                BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
