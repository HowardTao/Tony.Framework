using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Tony.Data.EF
{
    /// <summary>
    /// 数据访问(SqlServer) 上下文
    /// </summary>
    public class SqlServerDbContext:DbContext,IDbContext
    {
        public SqlServerDbContext(string connString):base(connString)
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var assembleFileName = Assembly.GetExecutingAssembly().CodeBase
                .Replace("Tony.Data.EF.DLL", "Tony.Application.Mapping.dll").Replace("file:///", "");
            var asm = Assembly.LoadFile(assembleFileName);
            var typesToRegister = asm.GetTypes().Where(t => !string.IsNullOrEmpty(t.Namespace))
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
