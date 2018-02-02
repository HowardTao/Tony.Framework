namespace Tony.Data.Repository
{
    /// <summary>
    /// 定义仓储模型工厂
    /// </summary>
    /// <typeparam name="T">泛型实体</typeparam>
    public class RepositoryFactory<T> where T : class, new()
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository<T> BaseRepository(string connString)
        {
            return new Repository<T>(DbFactory.Base(connString,DatabaseType.SqlServer));
        }
        /// <summary>
        /// 定义仓储模型工厂（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository<T> BaseRepository()
        {
            return new Repository<T>(DbFactory.Base());
        }
    }
}
