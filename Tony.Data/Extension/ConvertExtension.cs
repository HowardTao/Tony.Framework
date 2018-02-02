using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Reflection;

namespace Tony.Data
{
    /// <summary>
    /// 转换扩展类
    /// </summary>
   public class ConvertExtension
    {
        /// <summary>
        /// 将DataReader数据转为Dynamic对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static dynamic DataFillDynamic(IDataReader reader)
        {
            using (reader)
            {
                dynamic d = new ExpandoObject();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    try
                    {
                        ((IDictionary<string,object>)d).Add(reader.GetName(i),reader.GetValue(i));
                    }
                    catch
                    {
                        ((IDictionary<string, object>)d).Add(reader.GetName(i), null);
                    }
                }
                return d;
            }
        }
        /// <summary>
        /// 获取模型对象集合
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<dynamic> DataFillDynamicList(IDataReader reader)
        {
            using (reader)
            {
                var list = new List<dynamic>();
                if (reader != null && !reader.IsClosed)
                {
                    while (reader.Read())
                    {
                        list.Add(DataFillDynamic(reader));
                    }
                    reader.Close();
                    reader.Dispose();
                }
                return list;
            }
        }
        /// <summary>
        /// 将IDataReader转换为泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> IDataReaderToList<T>(IDataReader reader)
        {
            using (reader)
            {
                var field = new List<string>(reader.FieldCount);
                var list = new List<T>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    field.Add(reader.GetName(i).ToLower());
                }
                while (reader.Read())
                {
                    var model = Activator.CreateInstance<T>();
                    foreach (var property in model.GetType().GetProperties(BindingFlags.GetProperty|BindingFlags.Public|BindingFlags.Instance))
                    {
                        if (field.Contains(property.Name.ToLower()))
                        {
                            if (!IsNullOrDbNull(reader[property.Name]))
                            {
                                property.SetValue(model, HackType(reader[property.Name], property.PropertyType), null);
                            }
                        }
                        list.Add(model);
                    }
                }
                reader.Close();
                reader.Dispose();
                return list;
            }
        }
        /// <summary>
        /// 将IDataReader转换为DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable IDataReaderToDataTable(IDataReader reader)
        {
            using (reader)
            {
                var dt = new DataTable("Table");
                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    dt.Columns.Add(reader.GetName(i).ToLower(), reader.GetFieldType(i));
                }
                dt.BeginLoadData();
                var objValues = new object[reader.FieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    dt.LoadDataRow(objValues, true);
                }
                dt.EndLoadData();
                return dt;
            }
        }

        /// <summary>
        /// 获取实体类键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Hashtable GetPropertyInfo<T>(T entity)
        {
            var type = entity.GetType();
            //object cacheEntity = CacheHelper.GetCache("CacheEntity_" + EntityAttribute.GetEntityTable<T>());
            object cacheEntity = null;
            if (cacheEntity == null)
            {
                var ht = new Hashtable();
                var props = type.GetProperties();
                foreach (var property in props)
                {
                    var key = property.Name;
                    var value = property.GetValue(entity, null);
                    ht[key] = value;
                }

                //CacheHelper.SetCache("CacheEntity_" + EntityAttribute.GetEntityTable<T>(), ht);
                return ht;
            }
            return (Hashtable) cacheEntity;
        }
        /// <summary>
        /// 判断值是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrDbNull(object obj)
        {
            return obj is DBNull || string.IsNullOrEmpty(obj.ToString());
        }
        /// <summary>
        /// 这个类对可空类型进行判断转换，要不然会报错
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) return null;
                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
    }
}
