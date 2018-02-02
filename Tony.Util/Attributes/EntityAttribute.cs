using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Tony.Util
{
    /// <summary>
    /// 获取实体类Attribute自定义属性
    /// </summary>
    public class EntityAttribute
    {
        /// <summary>
        /// 获取实体对象的Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEntityKey<T>()
        {
            var type = typeof(T);
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                foreach (Attribute attr in prop.GetCustomAttributes(true))
                {
                    var keyAttribute = attr as KeyAttribute;
                    if (keyAttribute != null) return prop.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取实体对象的表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEntityTable<T>()
        {
            var entityName = "";
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttributes(true).OfType<TableAttribute>();
            var descriptionAttributes = tableAttribute as TableAttribute[] ?? tableAttribute.ToArray();
            entityName = descriptionAttributes.Any() ? descriptionAttributes.ToList()[0].Name : type.Name; 
            return entityName;
        }
    }
}
