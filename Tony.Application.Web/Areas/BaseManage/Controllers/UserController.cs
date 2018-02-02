using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tony.Application.Business.BaseManage;
using Tony.Application.CacheHelper;
using Tony.Util.WebControl;

namespace Tony.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：用户管理
    /// </summary>
    public class UserController : MvcControllerBase
    {
        private UserCache userCache = new UserCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private UserBll userBll = new UserBll();

        #region 视图功能
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回机构+部门+用户树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var organizeData = organizeCache.GetList().ToList();
            var departmentData = departmentCache.GetList().ToList();
            var userData = userCache.GetList().ToList();
            var treeList = new List<TreeEntity>();

            #region 机构
            foreach (var item in organizeData)
            {
                var tree = new TreeEntity();
                var hasChildren = organizeData.Count(t => t.ParentId == item.OrganizeId) != 0;
                if (hasChildren == false)
                {
                    hasChildren = organizeData.Count(t => t.OrganizeId == item.OrganizeId) != 0;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                tree.id = item.OrganizeId;
                tree.text = item.FullName;
                tree.value = item.OrganizeId;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Organize";
                treeList.Add(tree);
            }
            #endregion

            #region 部门
            treeList.AddRange(departmentData.Select(t=>new TreeEntity
            {
                id=t.DepartmentId,
                text = t.FullName,
                value = t.DepartmentId,
                parentId = t.ParentId=="0"?t.OrganizeId:t.ParentId,
                isexpand = true,
                complete = true,
                hasChildren = true,
                Attribute = "Sort",
                AttributeValue = "Department"
            }));
            #endregion

            #region 用户
            treeList.AddRange(userData.Select(item => new TreeEntity
            {
                id = item.UserId,
                text = item.RealName,
                value = item.Account,
                parentId = item.DepartmentId,
                title = item.RealName + "（" + item.Account + "）",
                isexpand = true,
                complete = true,
                hasChildren = false,
                Attribute = "Sort",
                AttributeValue = "User",
                img = "fa fa-user"
            }));
            #endregion

            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeToJson());
        }

        #endregion

        #region 验证数据

        #endregion

        #region 提交数据

        #endregion


    }
}