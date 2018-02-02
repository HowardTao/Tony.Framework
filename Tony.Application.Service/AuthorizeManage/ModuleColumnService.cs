using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.IService.AuthorizeManage;
using Tony.Data.Repository;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace Tony.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：功能表格列表
    /// </summary>
    public class ModuleColumnService : RepositoryFactory<ModuleColumnEntity>, IModuleColumnService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ModuleColumnEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<ModuleColumnEntity> GetList()
        {
            return BaseRepository().IQueryable().OrderBy(t=>t.SortCode).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleColumnEntity GetEntity(string keyValue)
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
            BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ModuleColumnEntity entity)
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
    }
}
