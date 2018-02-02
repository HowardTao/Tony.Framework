using System.Collections.Generic;
using System.Text;

namespace Tony.Util.WebControl
{
    /// <summary>
    /// 构造树形表格Json
    /// </summary>
   public static class TreeGridJson
    {
        private static int _lft = 1, _rgt = 1000000;

        /// <summary>
        /// 转换树形Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="index"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private static string TreeJson(List<TreeGridEntity> list, int index, string parentId)
        {
            var sb =new StringBuilder();
            var childNodeList = list.FindAll(t => t.parentId == parentId);
            if (childNodeList.Count > 0) index++;
            foreach (var entity in childNodeList)
            {
                var strJson = entity.entityJson;
                strJson = strJson.Insert(1, "\"level\":" + index + ",");
                strJson = strJson.Insert(1, "\"isLeaf\":" + (entity.hasChildren != true).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"expanded\":" + entity.expanded.ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"lft\":" + _lft++ + ",");
                strJson = strJson.Insert(1, "\"rgt\":" + _rgt-- + ",");
                sb.Append(strJson);
                sb.Append(TreeJson(list, index, entity.id));
            }
            return sb.ToString().Replace("}{", "},{");
        }

        /// <summary>
        /// 转换树形Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <returns></returns>
        public static string TreeJson(this List<TreeGridEntity> list)
        {
            var sb = new StringBuilder();
            sb.Append("{ \"rows\": [");
            sb.Append(TreeJson(list, -1, "0"));
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
