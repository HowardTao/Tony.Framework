using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tony.Application.Business.AuthorizeManage;
using Tony.Application.CacheHelper;
using Tony.Application.Code;
using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.Entity.SystemManage.ViewModel;
using Tony.Util;

namespace Tony.Application.Web.Controllers
{
    /// <summary>
    /// 客户端数据
    /// </summary>
    public class ClientDataController : MvcControllerBase
    {
        private DataItemCache dataItemCache = new DataItemCache();
        private DepartmentCache daDepartmentCache = new DepartmentCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private PostCache postCache = new PostCache();
        private RoleCache roleCache = new RoleCache();
        private UserCache userCache = new UserCache();
        private UserGroupCache userGroupCache = new UserGroupCache();
        private AuthorizeBll authorizeBll = new AuthorizeBll();

        #region 获取数据

        /// <summary>
        /// 批量加载数据给客户端（把常用数据全部加载到浏览器中 这样能够减少数据库交互）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetClientDataJson()
        {
            var jsonData = new
            {
                organize = GetOrganizeData(),              //公司
                department = GetDepartmentData(),          //部门
                post = GetPostData(),                      //岗位
                role = GetRoleData(),                      //角色
                userGroup = GetUserGroupData(),            //用户组
                user = GetUserData(),                      //用户
                dataItem = GetDataItem(),                  //字典
                authorizeMenu = GetModuleData(),           //导航菜单
                authorizeButton = GetModuleButtonData(),   //功能按钮
                authorizeColumn = GetModuleColumnData(),   //功能视图
                menuData = GetMenuData(),
                buttonData = GetButtonData(),
            };
            return ToJsonResult(jsonData);
        }

        #region 处理基础数据
        /// <summary>
        /// 获取公司数据
        /// </summary>
        /// <returns></returns>
        private object GetOrganizeData()
        {
            var data = organizeCache.GetList();
            var dict = new Dictionary<string,object>();
            foreach (var entity in data)
            {
                var fieldItem = new {entity.EnCode, entity.FullName};
                dict.Add(entity.OrganizeId,fieldItem);
            }
            return dict;
        }
        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        private object GetDepartmentData()
        {
            var data = daDepartmentCache.GetList();
            var dict = new Dictionary<string,object>();
            foreach (var entity in data)
            {
                var fieldItem = new {entity.EnCode, entity.FullName, entity.OrganizeId};
                dict.Add(entity.DepartmentId,fieldItem);
            }
            return dict;
        }
        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetPostData()
        {
            var data = postCache.GetList();
            var dict = new Dictionary<string, object>();
            foreach (var entity in data)
            {
                var fieldItem = new { entity.EnCode, entity.FullName };
                dict.Add(entity.RoleId, fieldItem);
            }
            return dict;
        }
        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <returns></returns>
        private object GetRoleData()
        {
            var data = roleCache.GetList();
            var dict = new Dictionary<string, object>();
            foreach (var entity in data)
            {
                var fieldItem = new { entity.EnCode, entity.FullName };
                dict.Add(entity.RoleId, fieldItem);
            }
            return dict;
        }
        /// <summary>
        /// 获取用户组信息
        /// </summary>
        /// <returns></returns>
        private object GetUserGroupData()
        {
            var data = userGroupCache.GetList();
            var dict = new Dictionary<string, object>();
            foreach (var entity in data)
            {
                var fieldItem = new { entity.EnCode, entity.FullName };
                dict.Add(entity.RoleId, fieldItem);
            }
            return dict;
        }
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        private object GetUserData()
        {
            var data = userCache.GetList();
            var dict = new Dictionary<string, object>();
            foreach (var entity in data)
            {
                var fieldItem = new
                {
                    entity.EnCode,
                    entity.Account,
                    entity.RealName,
                    entity.OrganizeId,
                    entity.DepartmentId
                };
                dict.Add(entity.UserId, fieldItem);
            }
            return dict;
        }

        private object GetDataItem()
        {
            var dataList = dataItemCache.GetDataItemList();
            var dataSort = dataList.Distinct(new Comparint<DataItemModel>("EnCode"));
            var dictionarySort = new Dictionary<string, object>();
            foreach (var itemSort in dataSort)
            {
                var dataItemList = dataList.Distinct().Where(t => t.EnCode.Equals(itemSort.EnCode));
                var dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.ItemValue, itemList.ItemName);
                }
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.ItemDetailId, itemList.ItemName);
                }
                dictionarySort.Add(itemSort.EnCode, dictionaryItemList);
            }
            return dictionarySort;
        } 
        #endregion

        #region 处理授权数据
        /// <summary>
        /// 获取功能数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleData()
        {
            return authorizeBll.GetModuleList(SystemInfo.CurrentUserId);
        }
        /// <summary>
        /// 获取功能按钮数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleButtonData()
        {
            var data = authorizeBll.GetModuleButtonList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleButtonEntity>("ModuleId"));
            var dict = new Dictionary<string ,object>();
            foreach (var entity in dataModule)
            {
                var buttons = data.Where(t => t.ModuleId.Equals(entity.ModuleId));
                dict.Add(entity.ModuleId,buttons);
            }
            return dict;
        }
        /// <summary>
        /// 获取功能视图数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleColumnData()
        {
            var data = authorizeBll.GetModuleColumnList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleColumnEntity>("ModuleId"));
            var dict = new Dictionary<string, object>();
            foreach (var entity in dataModule)
            {
                var columns = data.Where(t => t.ModuleId.Equals(entity.ModuleId));
                dict.Add(entity.ModuleId, columns);
            }
            return dict;
        }

        private object GetMenuData()
        {
            var data = authorizeBll.GetModuleList(SystemInfo.CurrentUserId);
            var dict = new Dictionary<string ,object>();
            foreach (var entity in data)
            {
                var fieldIem = new {entity.EnCode, entity.FullName};
                dict.Add(entity.ModuleId,fieldIem);
            }
            return dict;
        }

        private object GetButtonData()
        {
            var data = authorizeBll.GetModuleButtonList(SystemInfo.CurrentUserId);
            var dict = new Dictionary<string, object>();
            foreach (var entity in data)
            {
                var fieldIem = new { entity.EnCode, entity.FullName };
                dict.Add(entity.ModuleButtonId, fieldIem);
            }
            return dict;
        } 
        #endregion

        #endregion
    }
}