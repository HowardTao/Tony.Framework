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
    /// <typeparam name="T">泛型实体</typeparam>
    public class Repository<T>:IRepository<T> where T:class ,new()
    {
        private IDatabase _db;
        public Repository(IDatabase database)
        {
            _db = database;
        }

        #region 事务提交
        public IRepository<T> BeginTrans()
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

        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return _db.ExecuteBySql(strSql, dbParameter);
        }

        public int ExecuteByProc(string procName)
        {
            return _db.ExecuteByProc(procName);
        }

        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            return _db.ExecuteByProc(procName, dbParameter);
        }
        #endregion

        #region 实体对象增删改
        public int Insert(T entity)
        {
            return _db.Insert<T>(entity);
        }

        public int Insert(List<T> entity)
        {
            return _db.Insert<T>(entity);
        }

        public int Delete()
        {
            return _db.Delete<T>();
        }

        public int Delete(T entity)
        {
            return _db.Delete<T>(entity);
        }

        public int Delete(List<T> entity)
        {
            return _db.Delete<T>(entity);
        }

        public int Delete(Expression<Func<T, bool>> condition)
        {
            return _db.Delete<T>(condition);
        }

        public int Delete(object keyValue)
        {
            return _db.Delete<T>(keyValue);
        }

        public int Delete(object[] keyValue)
        {
            return _db.Delete<T>(keyValue);
        }

        public int Delete(object propertyValue, string propertyName)
        {
            return _db.Delete<T>(propertyValue, propertyName);
        }

        public int Update(T entity)
        {
            return _db.Update<T>(entity);
        }

        public int Update(List<T> entity)
        {
            return _db.Update<T>(entity);
        }

        public int Update(Expression<Func<T, bool>> condition)
        {
            return _db.Update<T>(condition);
        } 
        #endregion

        #region 实体对象查询

        public T FindEntity(object keyValue)
        {
            return _db.FindEntity<T>(keyValue);
        }

        public T FindEntity(Expression<Func<T, bool>> condition)
        {
            return _db.FindEntity<T>(condition);
        }

        public IQueryable<T> IQueryable()
        {
            return _db.IQueryable<T>();
        }

        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            return _db.IQueryable<T>(condition);
        }

        public IEnumerable<T> FindList(string strSql)
        {
            return _db.FindList<T>(strSql);
        }

        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            return _db.FindList<T>(strSql, dbParameter);
        }

        public IEnumerable<T> FindList(Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindList<T>(pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindList<T>(condition, pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindList<T>(strSql, pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindList<T>(strSql,dbParameter,pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
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

        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            return _db.FindTable(strSql, dbParameter);
        }

        public DataTable FindTable(string strSql, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindTable(strSql,pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public DataTable FindTable(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            var total = pagination.records;
            var data = _db.FindTable(strSql,dbParameter, pagination.sidx, pagination.sord.ToLower() == "asc", pagination.rows,
                pagination.page, out total);
            pagination.records = total;
            return data;
        }

        public object FindObject(string strSql)
        {
            return _db.FindObject(strSql);
        }

        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            return _db.FindObject(strSql, dbParameter);
        } 
        #endregion
    }
}
