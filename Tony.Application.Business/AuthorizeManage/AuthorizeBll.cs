using System;
using System.Collections.Generic;
using System.Linq;
using Tony.Application.Code;
using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.Entity.AuthorizeManage.ViewModel;
using Tony.Application.IService.AuthorizeManage;
using Tony.Application.Service.AuthorizeManage;
using Tony.CacheHelper;

namespace Tony.Application.Business.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeBll
    {
        private IAuthorizeService service =new AuthorizeService();
        private ModuleBll moduleBll = new ModuleBll();
        private ModuleButtonBll moduleButtonBll = new ModuleButtonBll();
        private ModuleColumnBll moduleColumnBll = new ModuleColumnBll();

        /// <summary>
        /// 获取可读数据权限范围sql
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthor(operators, isWrite);
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthorUserId(operators, isWrite);
        }
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string currentUserId)
        {
            return OperatorProvider.Provider.Current().IsSystem ? moduleBll.GetList().FindAll(t => t.EnabledMark == 1) : service.GetModuleList(currentUserId);
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string currentUserId)
        {
            return OperatorProvider.Provider.Current().IsSystem ? moduleButtonBll.GetList() : service.GetModuleButtonList(currentUserId);
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string currentUserId)
        {
            return OperatorProvider.Provider.Current().IsSystem ? moduleColumnBll.GetList() : service.GetModuleColumnList(currentUserId);
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            return service.GetUrlList(userId);
        }
        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="action">请求地址</param>
        /// <returns></returns>
        public bool ActionAuthorize(string userId, string moduleId, string action)
        {
            List<AuthorizeUrlModel> authorizeUrlList;
            var cacheList = CacheFactory.Cache().GetCache<List<AuthorizeUrlModel>>("AuthorizeUrl_" + userId);
            if (cacheList == null)
            {
                authorizeUrlList = GetUrlList(userId).ToList();
                CacheFactory.Cache().WriteCache(authorizeUrlList, "AuthorizeUrl_" + userId,DateTime.Now.AddMinutes(1));
            }
            else
            {
                authorizeUrlList = cacheList;
            }
            authorizeUrlList = authorizeUrlList.FindAll(t => t.ModuleId.Equals(moduleId));
            foreach (var item in authorizeUrlList)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    var url = item.UrlAddress.Split('?');
                    if (item.ModuleId == moduleId && url[0] == action) return true;
                }
            }
            return false;
        }
    }
}
