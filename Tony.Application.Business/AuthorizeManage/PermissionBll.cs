using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.AuthorizeManage;
using Tony.Application.Service.AuthorizeManage;

namespace Tony.Application.Business.AuthorizeManage
{
   public class PermissionBll
    {
        private readonly IPermissionService service = new PermissionService();
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetObjectStr(string userId)
        {
            var sb = new StringBuilder();
            var list = service.GetObjectList(userId).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.Append(item.ObjectId + ",");
                }
                sb.Append(userId);
            }
            else
            {
                sb.Append(userId + ",");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetMemberList(string roleId)
        {
            throw new System.NotImplementedException();
        }
    }
}
