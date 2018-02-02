using System;
using System.Data;
using System.Data.Common;

namespace Tony.Data
{
   public class DbHelper
    {
        #region 构造函数
        public DbHelper(DbConnection dbConnection)
        {
            DbConnection = dbConnection;
            DbCommand = DbConnection.CreateCommand();
        }
        #endregion

        #region 属性
        private DbConnection DbConnection { get; set; }
        private IDbCommand DbCommand { get; set; }
        public static DatabaseType  DbType { get; set; }
        #endregion

        public void Close()
        {
            if (DbConnection != null)
            {
                DbConnection.Close();
                DbConnection.Dispose();
            }
            if (DbCommand != null) DbCommand.Dispose();
        }

        public IDataReader ExecuteReader(CommandType cmdType, string strSql,params DbParameter[] dbParameters)
        {
            try
            {
                PrepareCommand(DbConnection, DbCommand, null, cmdType, strSql, dbParameters);
                var rdr = DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集
        /// </summary>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="dbParameters">Sql参数</param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType cmdType, string strSql, params DbParameter[] dbParameters)
        {
            try
            {
                PrepareCommand(DbConnection, DbCommand, null, cmdType, strSql, dbParameters);
                var obj = DbCommand.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }

        /// <summary>
        /// 为即将执行准备一个命令
        /// </summary>
        /// <param name="dbConnection">SqlConnection对象</param>
        /// <param name="dbCommand">SqlCommand对象</param>
        /// <param name="dbTransaction">DbTransaction对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="strSql">存储过程名称或者T-SQL命令行, e.g. Select * from Products</param>
        /// <param name="dbParameters">执行命令所需的sql语句对应参数</param>
        private void PrepareCommand(DbConnection dbConnection, IDbCommand dbCommand, DbTransaction dbTransaction, CommandType cmdType, string strSql,params DbParameter[] dbParameters)
        {
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
            dbCommand.Connection = dbConnection;
            dbCommand.CommandText = strSql;
            if (dbTransaction != null) dbCommand.Transaction = dbTransaction;
            dbCommand.CommandType = cmdType;
            if (dbParameters != null)
            {
                dbParameters = DbParameters.ToDbParameter(dbParameters);
                foreach (var dbParameter in dbParameters)
                {
                    dbCommand.Parameters.Add(dbParameter);
                }
            }
        }
    }
}
