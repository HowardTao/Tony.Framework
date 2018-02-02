using System.Collections.Generic;
using System.Linq;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.AuthorizeManage;
using Tony.Data.Repository;

namespace Tony.Application.Service.AuthorizeManage
{
   public class PermissionService:RepositoryFactory,IPermissionService
    {
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectList(string userId)
        {
            return BaseRepository().IQueryable<UserRelationEntity>(t => t.UserId == userId)
                .OrderByDescending(t => t.CreateDate).ToList();
        }
    }
}
