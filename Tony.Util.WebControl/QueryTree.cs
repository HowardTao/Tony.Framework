using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Tony.Util.Extension;

namespace Tony.Util.WebControl
{
    /// <summary>
    /// 树形结构查询
    /// </summary>
   public static class QueryTree
    {
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string primaryKey,
            string parentId = "ParentId") where T : class
        {
            var list = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            var treeList = new List<T>();
            foreach (var entity in list)
            {
                //先把自己加进来
                treeList.Add(entity);
                //向上查询
                var pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if(string.IsNullOrEmpty(pId)&&pId=="0") break;
                    var upLamda = Expression.Equal(parameter.Property(primaryKey), Expression.Constant(pId))
                        .ToLambda<Predicate<T>>(parameter).Compile();
                    var upRecord = entityList.Find(upLamda);
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }

        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public static DataTable TreeWhere(this DataTable table, string condition, string primaryKey)
        {
            var drs = table.Select(condition);
            var treeTable = table.Clone();
            foreach (var dr in drs)
            {
                treeTable.ImportRow(dr);
                var pId = dr["ParentId"].ToString();
                while (true)
                {
                    if(string.IsNullOrEmpty(pId)&&pId=="0") break;
                    var pdrs = table.Select(primaryKey + "='" + pId + "'");
                    if (pdrs.Length > 0)
                    {
                        treeTable.ImportRow(pdrs[0]);
                        pId = pdrs[0]["ParentId"].ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            var treeView = treeTable.DefaultView;
            return treeView.ToTable(true);
        }
    }
}
