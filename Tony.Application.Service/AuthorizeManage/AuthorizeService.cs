using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Tony.Application.Code;
using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.Entity.AuthorizeManage.ViewModel;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.AuthorizeManage;
using Tony.Data;
using Tony.Data.Repository;

namespace Tony.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public class AuthorizeService : RepositoryFactory, IAuthorizeService
    {
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite)
        {
            if (operators.IsSystem) return "";
            var userId = operators.UserId;
            var strAuthorData = isWrite
                ? @"select * from Base_AuthorizeData where IsRead = 0 and ObjectId in (select ObjectId from Base_UserRelation where UserId = @UserId) or ObjectId = @UserId"
                : @"select * from Base_AuthorizeData where ObjectId in (select ObjectId from Base_UserRelation where UserId = @UserId) or ObjectId = @UserId";
            DbParameter[] parameters = {DbParameters.CreateDbParameter("@UserId", userId)};
            var listAuthorizeData = BaseRepository().FindList<AuthorizeDataEntity>(strAuthorData, parameters);
            var whereSb = new StringBuilder("select UserId from Base_user where 1=1 ");
            whereSb.Append(string.Format("and(UserId = '{0}'", userId));
            foreach (var item in listAuthorizeData)
            {
                switch (item.AuthorizeType)
                {
                    //最大权限
                    case 0:
                        return "";
                    case -2://本人及下属
                        whereSb.Append(string.Format(" or ManagerId = '{0}'",userId));
                        break;
                    case -3:
                        whereSb.Append(string.Format(" or DepartmentId = '{0}'", operators.DepartmentId));
                        break;
                    case -4:
                        whereSb.Append(string.Format(" or OrganizeId = '{0}'", operators.CompanyId));
                        break;
                    case -5:
                        whereSb.Append(string.Format(" or DepartmentId = '{0}'",item.ResourceId));
                        break;
                }
            }
            whereSb.Append(")");
            return whereSb.ToString();
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite)
        {
            var userIdList = GetDataAuthor(operators, isWrite);
            if (userIdList == "") return "";
            var userList = BaseRepository().FindList<UserEntity>(userIdList).ToList();
            var userSb = new StringBuilder();
            foreach (var entity in userList)
            {
                userSb.Append(entity.UserId + ",");
            }
            return userSb.ToString().Substring(0, userSb.Length - 1);
        }
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string currentUserId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_Module
                            WHERE   ModuleId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 1
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR (ItemType = 1 and ObjectId = @UserId) )
                            AND EnabledMark = 1  AND DeleteMark = 0 Order By SortCode");

            DbParameter[] parameter =
            {
                DbParameters.CreateDbParameter("@UserId",currentUserId)
            };
            return BaseRepository().FindList<ModuleEntity>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string currentUserId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_ModuleButton
                            WHERE   ModuleButtonId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 2
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR (ItemType = 2 and ObjectId = @UserId) ) Order By SortCode");

            DbParameter[] parameter =
            {
                DbParameters.CreateDbParameter("@UserId",currentUserId)
            };
            return BaseRepository().FindList<ModuleButtonEntity>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="currentUserId">当前登陆用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string currentUserId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_ModuleColumn
                            WHERE   ModuleColumnId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 3
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR (ItemType = 3 and ObjectId = @UserId) )  Order By SortCode");

            DbParameter[] parameter =
            {
                DbParameters.CreateDbParameter("@UserId",currentUserId)
            };
            return this.BaseRepository().FindList<ModuleColumnEntity>(strSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"
SELECT  ModuleId AS AuthorizeId ,
        ModuleId ,
        UrlAddress ,
        FullName
FROM    dbo.Base_Module
WHERE   ModuleId IN (
        SELECT  ItemId
        FROM    dbo.Base_Authorize
        WHERE   ItemType = 1
                AND ( ObjectId IN ( SELECT  ObjectId
                                    FROM    dbo.Base_UserRelation
                                    WHERE   UserId = @UserId ) )
                OR ( ItemType = 1
                     AND ObjectId = @UserId
                   ) )
        AND EnabledMark = 1
        AND DeleteMark = 0
        AND IsMenu = 1
        AND UrlAddress IS NOT NULL
UNION
SELECT  ModuleButtonId AS AuthorizeId ,
        ModuleId ,
        ActionAddress AS UrlAddress ,
        FullName
FROM    dbo.Base_ModuleButton
WHERE   ModuleButtonId IN (
        SELECT  ItemId
        FROM    dbo.Base_Authorize
        WHERE   ItemType = 2
                AND ( ObjectId IN ( SELECT  ObjectId
                                    FROM    dbo.Base_UserRelation
                                    WHERE   UserId = @UserId ) )
                OR ( ItemType = 2
                     AND ObjectId = @UserId
                   ) )
        AND ActionAddress IS NOT NULL");
            DbParameter[] parameters = new[] {DbParameters.CreateDbParameter("@UserId", userId)};
            return BaseRepository().FindList<AuthorizeUrlModel>(strSql.ToString(), parameters);
        }
    }
}
