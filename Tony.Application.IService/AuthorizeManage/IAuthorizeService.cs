using System.Collections.Generic;
using Tony.Application.Code;
using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.Entity.AuthorizeManage.ViewModel;

namespace Tony.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：授权认证
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取可读数据权限范围sql
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthor(Operator operators, bool isWrite);
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthorUserId(Operator operators, bool isWrite);
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetModuleList(string currentUserId);
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleButtonEntity> GetModuleButtonList(string currentUserId);
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleColumnEntity> GetModuleColumnList(string currentUserId);
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<AuthorizeUrlModel> GetUrlList(string userId);
    }
}
