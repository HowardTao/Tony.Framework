using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Data.Repository;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tony.Application.Entity.SystemManage.ViewModel;

namespace Tony.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 08:57
    /// 描 述：数据字典明细表
    /// </summary>
    public class DataItemDetailService : RepositoryFactory<DataItemDetailEntity>, IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DataItemDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<DataItemDetailEntity> GetList()
        {
            return BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  i.ItemId ,
                                    i.ItemCode AS EnCode ,
                                    d.ItemDetailId ,
                                    d.ParentId ,
                                    d.ItemCode ,
                                    d.ItemName ,
                                    d.ItemValue ,
                                    d.QuickQuery ,
                                    d.SimpleSpelling ,
                                    d.IsDefault ,
                                    d.SortCode ,
                                    d.EnabledMark
                            FROM    Base_DataItemDetail d
                                    LEFT JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   1 = 1
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0
                            ORDER BY d.SortCode ASC");
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, DataItemDetailEntity entity)
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
