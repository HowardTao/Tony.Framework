
using System;
using Microsoft.Practices.Unity;
using Tony.Data.EF;
using Tony.Util.Ioc;

namespace Tony.Data.Repository
{
    /// <summary>
    /// 数据库建立工厂
    /// </summary>
   public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DatabaseType dbType)
        {
            DbHelper.DbType = dbType;
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride("connString", connString),
                new ParameterOverride("dbType", dbType.ToString()));
        }

        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), UnityIocHelper.GetmapToByName("DBcontainer", "IDbContext"));
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride(
                "connString", "BaseDb"), new ParameterOverride(
                "dbType", ""));
            //return new Database("BaseDb", "SqlServer");
        }
    }
}
