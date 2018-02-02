using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Tony.Data
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
   public interface IDatabase
    {
        IDatabase BeginTrans();
        int Commit();
        void Rollback();
        void Close();

        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameters);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, params DbParameter[] dbParameters);

        int Insert<T>(T entity) where T : class;
        int Insert<T>(IEnumerable<T> entities) where T : class;
        int Delete<T>() where T : class;
        int Delete<T>(T entity) where T : class;
        int Delete<T>(IEnumerable<T> entities) where T : class;
        int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        int Delete<T>(object keyValue) where T : class;
        int Delete<T>(object[] keyValues) where T : class;
        int Delete<T>(object propertyValue, string propertyName) where T : class;
        int Update<T>(T entity) where T : class;
        int Update<T>(IEnumerable<T> entities) where T : class;
        int Update<T>(Expression<Func<T, bool>> expression) where T : class, new();

        T FindEntity<T>(object keyValue) where T : class;
        T FindEntity<T>(Expression<Func<T, bool>> expression) where T : class, new();
        IQueryable<T> IQueryable<T>() where T : class, new();
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> expression) where T : class, new();
        IEnumerable<T> FindList<T>() where T : class, new();
        IEnumerable<T> FindList<T>(Func<T,object> orderby) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T,bool>> expression) where T : class, new();
        IEnumerable<T> FindList<T>(string orderField,bool isAsc,int pageSize,int pageIndex,out int total) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new();
        IEnumerable<T> FindList<T>(string strSql) where T : class;
        IEnumerable<T> FindList<T>(string strSql,params DbParameter[] dbParameters) where T : class;
        IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameters, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql,params DbParameter[] dbParameters);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        DataTable FindTable(string strSql, DbParameter[] dbParameters, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameters);
    }
}
