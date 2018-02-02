using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using Tony.Util.Extension;

namespace Tony.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-26 15:58
    /// 描 述：行政区域管理
    /// </summary>
    public class AreaService : RepositoryFactory<AreaEntity>, IAreaService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<AreaEntity> GetList()
        {
            return BaseRepository().IQueryable(t=>t.Layer!=4).OrderBy(t=>t.CreateDate).ToList();
        }

        public IEnumerable<AreaEntity> GetList(string parentId, string keyword)
        {
            var expression = LinqExtensions.True<AreaEntity>();
            if (!string.IsNullOrEmpty(parentId))
            {
                expression = expression.And(t => t.ParentId == parentId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.AreaCode.Contains(keyword));
                expression = expression.Or(t => t.AreaName.Contains(keyword));
            }
            return BaseRepository().IQueryable(expression).OrderBy(t => t.CreateDate).ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AreaEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, AreaEntity entity)
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
