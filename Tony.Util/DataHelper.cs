using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Tony.Util
{
    /// <summary>
    /// 数据源转换
    /// </summary>
   public class DataHelper
    {
        #region 检查DataTable 是否有数据行
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }
        #endregion

        #region 根据条件过滤表的内容
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable DataFilter(DataTable dt, string condition)
        {
            if (!IsExistRows(dt)) return null;
            if (string.IsNullOrEmpty(condition.Trim())) return dt;
            var newdt = dt.Clone();
            var dr = dt.Select(condition);
            foreach (var row in dr)
            {
                newdt.ImportRow(row);
            }
            return newdt;
        }
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static DataTable DataFilter(DataTable dt, string condition, string sort)
        {
            if (!IsExistRows(dt)) return null;
            if (string.IsNullOrEmpty(condition.Trim())) return dt;
            var newdt = dt.Clone();
            var dr = dt.Select(condition, sort);
            foreach (var row in dr)
            {
                newdt.ImportRow(row);
            }
            return newdt;
        }
        #endregion

        #region DataTable 转 Hashtable
        /// <summary>
        /// DataTable 转 Hashtable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Hashtable DataTableToHashtable(DataTable dt)
        {
            var ht = new Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var key = dt.Columns[i].ColumnName;
                    ht[key] = dr[key];
                }
            }
            return ht;
        }
        #endregion

        #region  DataTable/DataSet 转 XML
        /// <summary>
        /// DataTable 转 XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToXml(DataTable dt)
        {
            if (dt == null) return "";
            var writer = new StringWriter();
            dt.WriteXml(writer);
            return writer.ToString();
        }

        /// <summary>
        /// DataTable 转 XML
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DataSetToXml(DataSet ds)
        {
            if (ds == null) return "";
            var writer = new StringWriter();
            ds.WriteXml(writer);
            return writer.ToString();
        }
        #endregion

        #region DataRow  转  HashTable
        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashTable(DataRow dr)
        {
            var ht = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
            {
                ht.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            return ht;
        }
        #endregion

        #region  将泛类型集合List类转换成DataTable
        /// <summary>
        ///  将泛类型集合List类转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            //检查实体集合不能为空
            if (list == null || list.Count < 1) throw new Exception("需转换的集合为空");
            var type = list[0].GetType();
            var props = type.GetProperties();
            var dt = new DataTable();
            foreach (var prop in props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach (var entity in list)
            {
                if (entity.GetType() != type) throw new Exception("要转换的集合元素类型不一致");
                var entityValues = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    entityValues[i] = props[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        } 
        #endregion
    }
}
