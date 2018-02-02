using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Tony.Util;
using Tony.Util.Ioc;

namespace Tony.Data.EF
{
   public class Database:IDatabase
    {
        public Database(string connString, string dbType)
        {
            if (dbType == "")
            {
                DbContext =
                    (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(new ParameterOverride("connString",
                        connString));
            }
            else
            {
                DbContext =
                    (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(dbType, new ParameterOverride("connString",
                        connString));
            }
            // DbContext = new SqlServerDbContext(connString);
        }

        #region 属性
        /// <summary>
        /// 数据访问上下文对象
        /// </summary>
        public DbContext DbContext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction DbTransaction { get; set; }
        #endregion

        #region 事务提交
        public IDatabase BeginTrans()
        {
            var dbConnection = ((IObjectContextAdapter) DbContext).ObjectContext.Connection;
            if(dbConnection.State==ConnectionState.Closed) dbConnection.Open();
            DbTransaction = dbConnection.BeginTransaction();
            return this;
        }

        public int Commit()
        {
            try
            {
                var result = DbContext.SaveChanges();
                if (DbTransaction != null)
                {
                    DbTransaction.Commit();
                    Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    var sqlEx = (SqlException) ex.InnerException.InnerException;
                    var msg = ExceptionMessage.GetSqlExceptionMessage(sqlEx.Number);
                    throw DataAccessException.ThrowDataAccessException(sqlEx, msg);
                }
                throw;
            }
            finally
            {
                if (DbTransaction == null)
                {
                    Close();
                }
            }
        }

        public void Rollback()
        {
            DbTransaction.Rollback();
            DbTransaction.Dispose();
            Close();
        }

        public void Close()
        {
            DbContext.Dispose();
        }
        #endregion

        #region 执行Sql语句
        public int ExecuteBySql(string strSql)
        {
            if (DbTransaction == null)
            {
                return DbContext.Database.ExecuteSqlCommand(strSql);
            }
            DbContext.Database.ExecuteSqlCommand(strSql);
            return DbTransaction == null ? Commit() : 0;
        }

        public int ExecuteBySql(string strSql, params DbParameter[] dbParameters)
        {
            if (DbTransaction == null)
            {
                return DbContext.Database.ExecuteSqlCommand(strSql, dbParameters);
            }
            DbContext.Database.ExecuteSqlCommand(strSql, dbParameters);
            return DbTransaction == null ? Commit() : 0;
        }

        public int ExecuteByProc(string procName)
        {
            if (DbTransaction == null)
            {
                return DbContext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName));
            }
            return DbTransaction == null ? Commit() : 0;
        }

        public int ExecuteByProc(string procName, params DbParameter[] dbParameters)
        {
            if (DbTransaction == null)
            {
                return DbContext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName), dbParameters);
            }
            return DbTransaction == null ? Commit() : 0;
        }
        #endregion

        #region 实体对象增删改
        public int Insert<T>(T entity) where T : class
        {
            DbContext.Entry(entity).State = EntityState.Added;
            return DbTransaction == null ? Commit() : 0;
        }

        public int Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Added;
            }
            return DbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>() where T : class
        {
            var entitySet = DbContextExtensions.GetEntitySet<T>(DbContext);
            if (entitySet != null)
            {
                var tableName =
                    entitySet.MetadataProperties.Contains("Table") &&
                    entitySet.MetadataProperties["Table"].Value != null
                        ? entitySet.MetadataProperties["Table"].Value.ToString()
                        : entitySet.Name;
                return ExecuteBySql(DbContextExtensions.DeleteSql(tableName));
            }
            return -1;
        }

        public int Delete<T>(T entity) where T : class
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Set<T>().Remove(entity);
            return DbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                DbContext.Set<T>().Attach(entity);
                DbContext.Set<T>().Remove(entity);
            }
            return DbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var entities = DbContext.Set<T>().Where(expression).ToList();
            return entities.Any() ? Delete(entities) : 0;
        }

        public int Delete<T>(object keyValue) where T : class
        {
            var entitySet = DbContextExtensions.GetEntitySet<T>(DbContext);
            if (entitySet != null)
            {
                var tableName =
                    entitySet.MetadataProperties.Contains("Table") &&
                    entitySet.MetadataProperties["Table"].Value != null
                        ? entitySet.MetadataProperties["Table"].Value.ToString()
                        : entitySet.Name;
                var keyField = entitySet.ElementType.KeyMembers[0].Name;
                return ExecuteBySql(DbContextExtensions.DeleteSql(tableName, keyField, keyValue));
            }
            return -1;
        }

        public int Delete<T>(object[] keyValues) where T : class
        {
            var entitySet = DbContextExtensions.GetEntitySet<T>(DbContext);
            if (entitySet != null)
            {
                var tableName =
                    entitySet.MetadataProperties.Contains("Table") &&
                    entitySet.MetadataProperties["Table"].Value != null
                        ? entitySet.MetadataProperties["Table"].Value.ToString()
                        : entitySet.Name;
                var keyField = entitySet.ElementType.KeyMembers[0].Name;
                return ExecuteBySql(DbContextExtensions.DeleteSql(tableName, keyField, keyValues));
            }
            return -1;
        }

        public int Delete<T>(object propertyValue, string propertyName) where T : class
        {
            var entitySet = DbContextExtensions.GetEntitySet<T>(DbContext);
            if (entitySet != null)
            {
                var tableName =
                    entitySet.MetadataProperties.Contains("Table") &&
                    entitySet.MetadataProperties["Table"].Value != null
                        ? entitySet.MetadataProperties["Table"].Value.ToString()
                        : entitySet.Name;
                return ExecuteBySql(DbContextExtensions.DeleteSql(tableName, propertyName, propertyValue));
            }
            return -1;
        }

        public int Update<T>(T entity) where T : class
        {
            DbContext.Set<T>().Attach(entity);
            var props = ConvertExtension.GetPropertyInfo(entity);
            foreach (string item in props.Keys)
            {
                var value = DbContext.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    if (value.ToString() == "&nbsp;") DbContext.Entry(entity).Property(item).CurrentValue = null;
                    DbContext.Entry(entity).Property(item).IsModified = true;
                }
            }
            return DbTransaction == null ? Commit() : 0;
        }

        public int Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
            return DbTransaction == null ? Commit() : 0;
        }

        public int Update<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 实体对象查询
        public T FindEntity<T>(object keyValue) where T : class
        {
            return DbContext.Set<T>().Find(keyValue);
        }

        public T FindEntity<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
           return DbContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public IQueryable<T> IQueryable<T>() where T : class, new()
        {
            return DbContext.Set<T>();
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return DbContext.Set<T>().Where(expression);
        }

        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return DbContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindList<T>(Func<T, object> @orderby) where T : class, new()
        {
            return DbContext.Set<T>().OrderBy(orderby).ToList();
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return DbContext.Set<T>().Where(expression).ToList();
        }

        public IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new()
        {
            var orderFields = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = DbContext.Set<T>().AsQueryable();
            foreach (var item in orderFields)
            {
                var orderPart = item;
                orderPart = Regex.Replace(orderPart, @"\s+", " ");
                var orderArr = orderPart.Split(' ');
                var _orderField = orderArr[0];
                if (orderArr.Length == 2)
                {
                    isAsc = orderArr[1].ToLower() == "ASC";
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending",
                    new[] {typeof(T), property.PropertyType}, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            return tempData.ToList();
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> expression, string orderField, bool isAsc, int pageSize, int pageIndex,
            out int total) where T : class, new()
        {
            var orderFields = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = DbContext.Set<T>().Where(expression);
            foreach (var item in orderFields)
            {
                var orderPart = item;
                orderPart = Regex.Replace(orderPart, @"\s+", " ");
                var orderArr = orderPart.Split(' ');
                var _orderField = orderArr[0];
                if (orderArr.Length == 2)
                {
                    isAsc = orderArr[1].ToLower() == "ASC";
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending",
                    new[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            return tempData.ToList();
        }

        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return FindList<T>(strSql, null);
        }

        public IEnumerable<T> FindList<T>(string strSql, params DbParameter[] dbParameters) where T : class
        {
            using (var conn = DbContext.Database.Connection)
            {
                var reader = new DbHelper(conn).ExecuteReader(CommandType.Text, strSql,dbParameters);
                return ConvertExtension.IDataReaderToList<T>(reader);
            }
        }

        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            return FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }

        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameters, string orderField, bool isAsc, int pageSize,
            int pageIndex, out int total) where T : class
        {
            using (var conn = DbContext.Database.Connection)
            {
                var sb = new StringBuilder();
                if (pageIndex == 0) pageIndex = 1;
                var num1 = (pageIndex - 1) * pageSize;
                var num2 = pageIndex * pageSize;
                var orderBy="";
                if (!string.IsNullOrEmpty(orderField))
                {
                    if (orderField.ToUpper().IndexOf("ASC", StringComparison.Ordinal) + orderField.ToUpper().IndexOf("DESC", StringComparison.Ordinal) > 0)
                    {
                        orderBy = "Order By " + orderField;
                    }
                    else
                    {
                        orderBy = "Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                    }
                }
                else
                {
                    orderBy = "order by (select 0)";
                }
                sb.Append("Select * From (Select ROW_NUMBER() Over (" + orderBy + ")");
                sb.Append(" As rowNum, * From (" + strSql + ") As T ) As N Where rowNum > " + num1 + " And rowNum <= " + num2 + "");
                total = Convert.ToInt32(new DbHelper(conn).ExecuteScalar(CommandType.Text,
                    "Select Count(1) From (" + strSql + ") As t", dbParameters));
                var reader = new DbHelper(conn).ExecuteReader(CommandType.Text, sb.ToString(),dbParameters);
                return ConvertExtension.IDataReaderToList<T>(reader);
            }
        }
        #endregion

        #region Sql查询
        public DataTable FindTable(string strSql)
        {
            return FindTable(strSql, null);
        }

        public DataTable FindTable(string strSql, params DbParameter[] dbParameters)
        {
            using (var conn = DbContext.Database.Connection)
            {
                var reader = new DbHelper(conn).ExecuteReader(CommandType.Text, strSql,dbParameters);
                return ConvertExtension.IDataReaderToDataTable(reader);
            }
        }

        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return FindTable(strSql, null, isAsc, pageSize, pageIndex, out total);
        }

        public DataTable FindTable(string strSql, DbParameter[] dbParameters, string orderField, bool isAsc, int pageSize,
            int pageIndex, out int total)
        {
            using (var conn = DbContext.Database.Connection)
            {
                var sb = new StringBuilder();
                if (pageIndex == 0) pageIndex = 1;
                var num1 = (pageIndex - 1) * pageSize;
                var num2 = pageIndex * pageSize;
                var orderBy = "";
                if (!string.IsNullOrEmpty(orderField))
                {
                    if (orderField.ToUpper().IndexOf("ASC", StringComparison.Ordinal) + orderField.ToUpper().IndexOf("DESC", StringComparison.Ordinal) > 0)
                    {
                        orderBy = "Order By " + orderField;
                    }
                    else
                    {
                        orderBy = "Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                    }
                }
                else
                {
                    orderBy = "order by (select 0)";
                }
                sb.Append("Select * From (Select ROW_NUMBER() Over (" + orderBy + ")");
                sb.Append(" As rowNum, * From (" + strSql + ") As T ) As N Where rowNum > " + num1 + " And rowNum <= " + num2 + "");
                total = Convert.ToInt32(new DbHelper(conn).ExecuteScalar(CommandType.Text,
                    "Select Count(1) From (" + strSql + ") As t", dbParameters));
                var reader = new DbHelper(conn).ExecuteReader(CommandType.Text, sb.ToString(), dbParameters);
                return ConvertExtension.IDataReaderToDataTable(reader);
            }
        }

        public object FindObject(string strSql)
        {
            return FindObject(strSql, null);
        }

        public object FindObject(string strSql, DbParameter[] dbParameters)
        {
            using (var conn = DbContext.Database.Connection)
            {
                return new DbHelper(conn).ExecuteScalar(CommandType.Text, strSql,dbParameters);
            }
        }

        #endregion
    }
}
