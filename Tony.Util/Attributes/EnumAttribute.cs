using System;
using System.ComponentModel;

namespace Tony.Util
{
    /// <summary>
    /// 获取枚举自定义属性
    /// </summary>
   public class EnumAttribute
    {
        /// <summary>
        /// 返回枚举项的描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            if (name == null) return null;
            var fieldInfo = enumType.GetField(name);
            if (fieldInfo == null) return null;
            var attr =
                Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
            return attr != null ? attr.Description : null;
        }
    }
}
