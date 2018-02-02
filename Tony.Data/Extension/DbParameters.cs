using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Tony.Data
{
   public class DbParameters
    {
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParamCharacter()
        {
            string character;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    character = "@";
                    break;
                case DatabaseType.Oracle:
                    character = ":";
                    break;
                case DatabaseType.MySql:
                    character = "?";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return character;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter()
        {
            DbParameter dbParameter;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    dbParameter = new SqlParameter();
                    break;
                case DatabaseType.Oracle:
                    dbParameter = new OracleParameter();
                    break;
                case DatabaseType.MySql:
                    dbParameter = new MySqlParameter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return dbParameter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            var param = CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            var param = CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }

        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            var i = 0;
            var size = dbParameter.Length;
            DbParameter[] newDbParam;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    newDbParam = new DbParameter[size];
                    while (i < size)
                    {
                        newDbParam[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.Oracle:
                    newDbParam = new DbParameter[size];
                    while (i < size)
                    {
                        newDbParam[i] = new OracleParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.MySql:
                    newDbParam = new DbParameter[size];
                    while (i < size)
                    {
                        newDbParam[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return newDbParam;
        }
    }
}
