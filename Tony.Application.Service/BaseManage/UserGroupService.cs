using System.Collections.Generic;
using System.Linq;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Data.Repository;
using Tony.Util.Extension;
using Tony.Util.WebControl;

namespace Tony.Application.Service.BaseManage
{
   public class UserGroupService: RepositoryFactory<RoleEntity>, IUserGroupService
    {
        #region 获取数据
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 4).And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 职位实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoleEntity GetEntity(string keyValue)
        {
            return BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 职位编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 职位名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 保存职位表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">职位实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoleEntity roleEntity)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
