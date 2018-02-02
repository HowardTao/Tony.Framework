using System;
using System.Data.Entity.Infrastructure;

namespace Tony.Data.EF
{
    /// <summary>
    /// 数据库连接接口
    /// </summary>
   public interface IDbContext:IDisposable,IObjectContextAdapter
    {
    }
}
