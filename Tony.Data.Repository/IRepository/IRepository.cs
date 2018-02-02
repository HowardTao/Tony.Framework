using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Tony.Util.WebControl;

namespace Tony.Data.Repository
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作接口
    /// </summary>
    public interface IRepository
    {
        IRepository BeginTrans();
        void Commit();
        void Rollback();

        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameters);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, params DbParameter[] dbParameters);

        int Insert<T>(T entity) where T : class;
        int Insert<T>(List<T> entities) where T : class;
        int Delete<T>() where T : class;
        int Delete<T>(T entity) where T : class;
        int Delete<T>(List<T> entities) where T : class;
        int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        int Delete<T>(object keyValue) where T : class;
        int Delete<T>(object[] keyValues) where T : class;
        int Delete<T>(object propertyValue, string propertyName) where T : class;
        int Update<T>(T entity) where T : class;
        int Update<T>(List<T> entities) where T : class;
        int Update<T>(Expression<Func<T, bool>> expression) where T : class, new();

        T FindEntity<T>(object keyValue) where T : class;
        T FindEntity<T>(Expression<Func<T, bool>> expression) where T : class, new();
        IQueryable<T> IQueryable<T>() where T : class, new();
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> expression) where T : class, new();
        IEnumerable<T> FindList<T>() where T : class, new();
        IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression) where T : class, new();
        IEnumerable<T> FindList<T>(Pagination pagination) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression, Pagination pagination) where T : class, new();
        IEnumerable<T> FindList<T>(string strSql) where T : class;
        IEnumerable<T> FindList<T>(string strSql, params DbParameter[] dbParameters) where T : class;
        IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class;
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameters, Pagination pagination) where T : class;

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, params DbParameter[] dbParameters);
        DataTable FindTable(string strSql, Pagination pagination);
        DataTable FindTable(string strSql, DbParameter[] dbParameters, Pagination pagination);
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameters);
    }
}
