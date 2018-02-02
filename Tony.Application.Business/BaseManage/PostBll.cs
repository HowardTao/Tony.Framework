using System.Collections.Generic;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Application.Service.BaseManage;
using Tony.CacheHelper;
using Tony.Util.WebControl;

namespace Tony.Application.Business.BaseManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：岗位管理
    /// </summary>
    public class PostBll
    {

        private IPostService service = new PostService();
        public string CacheKey = "PostCache";

        #region 获取数据
        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 岗位实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoleEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 岗位编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode, keyValue);
        }
        /// <summary>
        /// 岗位名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            service.RemoveForm(keyValue);
            CacheFactory.Cache().RemoveCache(CacheKey);
        }
        /// <summary>
        /// 保存岗位表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="jobEntity">岗位实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoleEntity jobEntity)
        {
            service.SaveForm(keyValue, jobEntity);
            CacheFactory.Cache().RemoveCache(CacheKey);
        }
        #endregion
    }
}
