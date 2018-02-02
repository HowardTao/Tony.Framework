using System.Collections.Generic;
using Tony.Application.Entity.BaseManage;

namespace Tony.Application.IService.AuthorizeManage
{
    public interface IPermissionService
    {
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<UserRelationEntity> GetObjectList(string userId);
    }
}