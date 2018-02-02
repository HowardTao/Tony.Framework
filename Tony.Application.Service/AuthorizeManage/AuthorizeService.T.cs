using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tony.Application.Code;
using Tony.Application.IService.AuthorizeManage;
using Tony.Data.Repository;
using Tony.Util.Extension;
using Tony.Util.WebControl;

namespace Tony.Application.Service.AuthorizeManage
{
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class, new()
    {
        #region 带权限的数据源查询
        public IQueryable<T> IQueryable()
        {
            if (GetReadUserId() == "") return BaseRepository().IQueryable();
            var parameter = Expression.Parameter(typeof(T), "t");
            var authorCondition = Expression.Constant(GetReadUserId())
                .Call("Contains", parameter.Property("CreateUserId"));
            var lambda = authorCondition.ToLambda<Func<T, bool>>(parameter);
            return BaseRepository().IQueryable(lambda);
        }

        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            if (GetReadUserId() == "") return BaseRepository().IQueryable(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            var authorCondition = Expression.Constant(GetReadUserId())
                .Call("Contains", parameter.Property("CreateUserId"));
            var lambda = authorCondition.ToLambda<Func<T, bool>>(parameter);
            condition = condition.And(lambda);
            return BaseRepository().IQueryable(condition);
        }

        public IEnumerable<T> FindList(Pagination pagination)
        {
            if (GetReadUserId() == "") return BaseRepository().FindList(pagination);
            var parameter = Expression.Parameter(typeof(T), "t");
            var authorCondition = Expression.Constant(GetReadUserId())
                .Call("Contains", parameter.Property("CreateUserId"));
            var lambda = authorCondition.ToLambda<Func<T, bool>>(parameter);
            return BaseRepository().FindList(lambda, pagination);
        }

        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            if (GetReadUserId() == "") return BaseRepository().FindList(condition, pagination);
            var parameter = Expression.Parameter(typeof(T), "t");
            var authorCondition = Expression.Constant(GetReadUserId())
                .Call("Contains", parameter.Property("CreateUserId"));
            var lambda = authorCondition.ToLambda<Func<T, bool>>(parameter);
            condition = condition.And(lambda);
            return BaseRepository().FindList(condition, pagination);
        }

        public IEnumerable<T> FindList(string strSql)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format(@"and CreateUserId in ({0})", GetReadSql()));
            return BaseRepository().FindList(strSql);
        }

        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format(@"and CreateUserId in ({0})", GetReadSql()));
            return BaseRepository().FindList(strSql,dbParameter);
        }

        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format(@"and CreateUserId in ({0})", GetReadSql()));
            return BaseRepository().FindList(strSql,pagination);
        }

        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format(@"and CreateUserId in ({0})", GetReadSql()));
            return BaseRepository().FindList(strSql, dbParameter,pagination);
        }
        #endregion

        #region 取数据权限用户

        private static string GetReadUserId()
        {
            return OperatorProvider.Provider.Current().IsSystem
                ? ""
                : OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
        }

        private static string GetReadSql()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
        }
        #endregion
    }
}
