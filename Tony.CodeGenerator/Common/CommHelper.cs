namespace Tony.CodeGenerator
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommHelper
    {
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindModelsType(string name)
        {
            name = name.ToLower();
            if (name == "int" || name == "number" || name == "integer" || name == "smallint") return "int?";
            if (name == "tinyint") return "byte?";
            if (name == "numeric" || name == "real") return "Single?";
            if (name == "float") return "float?";
            if (name == "decimal" || name == "number(8,2)") return "decimal?";
            if (name == "char" || name == "varchar" || name == "nvarchar2" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext") return "string";
            if (name == "bit") return "bool?";
            if (name == "datetime" || name == "date" || name == "smalldatetime") return "DateTime?";
            if (name == "money" || name == "smallmoney") return "double?";
            return "string";
        }
    }
}
