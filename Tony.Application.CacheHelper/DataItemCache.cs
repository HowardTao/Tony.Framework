using System.Collections.Generic;
using System.Linq;
using Tony.Application.Business.SystemManage;
using Tony.Application.Entity.SystemManage.ViewModel;
using Tony.CacheHelper;

namespace Tony.Application.CacheHelper
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：数据字典列表缓存
    /// </summary>
    public class DataItemCache
    {
        private DataItemDetailBll dataItemDetailBll = new DataItemDetailBll();
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DataItemModel>>(dataItemDetailBll.CacheKey);
            if (cacheList == null)
            {
                var data = dataItemDetailBll.GetDataItemList();
                CacheFactory.Cache().WriteCache(data, dataItemDetailBll.CacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="enCode">分类代码</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList(string enCode)
        {
            return GetDataItemList().Where(t => t.EnCode == enCode);
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="enCode">分类代码</param>
        /// <param name="itemValue">项目值</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList(string enCode,string itemValue)
        {
            var data = GetDataItemList(enCode);
            var itemDetailId = data.First(t => t.ItemValue == itemValue).ItemDetailId;
            return data.Where(t => t.ParentId == itemDetailId);
        }

        /// <summary>
        /// 项目值转换名称
        /// </summary>
        /// <param name="enCode">分类代码</param>
        /// <param name="itemValue">项目值</param>
        /// <returns></returns>
        public string ToItemName(string enCode, string itemValue)
        {
            var data = GetDataItemList(enCode);
            return data.First(t => t.ItemValue == itemValue).ItemName;
        }
    }
}
