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
    /// 定义仓储模型中的数据标准操作
    /// </summary>
    public class Repository:IRepository
    {
        private IDatabase _db;
        public Repository(IDatabase database)
        {
            _db = database;
        }

        #region 事务提交

        public IRepository BeginTrans()
        {
            _db.BeginTrans();
            return this;
        }

        public void Commit()
        {
            _db.Commit();
        }

        public void Rollback()
        {
            _db.Rollback();
        }
        #endregion

        #region 执行Sql语句

        public int ExecuteBySql(string strSql)
        {
            return _db.ExecuteBySql(strSql);
        }

        public int ExecuteBySql(string strSql, params DbParameter[] dbParameters)
        {
            return _db.ExecuteBySql(strSql, dbParameters);
        }

        public int ExecuteByProc(string procName)
        {
            return _db.ExecuteByProc(procName);
        }

        public int ExecuteByProc(string procName, params DbParameter[] dbParameters)
        {
            return _db.ExecuteByProc(procName, dbParameters);
        }
        #endregion

        #region 实体对象增删改
        public int Insert<T>(T entity) where T : class
        {
            return _db.Insert<T>(entity);
        }

        public int Insert<T>(List<T> entities) where T : class
        {
            return _db.Insert<T>(entities);
        }

        public int Delete<T>() where T : class
        {
            return _db.Delete<T>();
        }

        public int Delete<T>(T entity) where T : class
        {
            return _db.Delete<T>(entity);
        }

        public int Delete<T>(List<T> entities) where T : class
        {
            return _db.Delete<T>(entities);
        }

        public int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _db.Delete<T>(expression);
        }

        public int Delete<T>(object keyValue) where T : class
        {
            return _db.Delete<T>(keyValue);
        }

        public int Delete<T>(object[] keyValues) where T : class
        {
            return _db.Delete<T>(keyValues);
        }

        public int Delete<T>(object propertyValue, string propertyName) where T : class
        {
            return _db.Delete<T>(propertyValue, propertyName);
        }

        public int Update<T>(T entity) where T : class
        {
            return _db.Update<T>(entity);
        }

        public int Update<T>(List<T> entities) where T : class
        {
            return _db.Update<T>(entities);
        }

        public int Update<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _db.Update(expression);

        }
        #endregion

        #region 实体对象查询
        public T FindEntity<T>(object keyValue) where T : class
        {
            return _db.FindEntity<T>(keyValue);
        }

        public T FindEntity<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _db.FindEntity<T>(expression);
        }

        public IQueryable<T> IQueryable<T>() where T : class, new()
        {
            return _db.IQueryable<T>();
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _db.IQueryable<T>(expression);
        }

        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return _db.FindList<T>();
        }

        public IEnumerable<T> FindList<T>(Func<T, object> @orderby) where T : class, new()
        {
            return _db.FindList<T>(orderby);
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _db.FindList<T>(expression);
        }

        public IEnumerable<T> FindList<T>(Pagination pagination) where T : class, new()
        {
            var total = pagination.records;
            var data = _db.FindList<T>(pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression, Pagination pagination) where T : class, new()
        {
            var total = pagination.records;
            var data = _db.FindList<T>(expression,pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return _db.FindList<T>(strSql);
        }

        public IEnumerable<T> FindList<T>(string strSql, params DbParameter[] dbParameters) where T : class
        {
            return _db.FindList<T>(strSql, dbParameters);
        }

        public IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class
        {
            var total = pagination.records;
            var data = _db.FindList<T>(strSql,pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameters, Pagination pagination) where T : class
        {
            var total = pagination.records;
            var data = _db.FindList<T>(strSql,dbParameters,pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }
        #endregion

        #region 数据源查询

        public DataTable FindTable(string strSql)
        {
            return _db.FindTable(strSql);
        }

        public DataTable FindTable(string strSql, params DbParameter[] dbParameters)
        {
            return _db.FindTable(strSql, dbParameters);
        }

        public DataTable FindTable(string strSql, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindTable(strSql, pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public DataTable FindTable(string strSql, DbParameter[] dbParameters, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindTable(strSql,dbParameters, pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public object FindObject(string strSql)
        {
            return _db.FindObject(strSql);
        }

        public object FindObject(string strSql, DbParameter[] dbParameters)
        {
            return _db.FindObject(strSql, dbParameters);
        }

        #endregion
    }
}
