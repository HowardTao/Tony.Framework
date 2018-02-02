using System.Configuration;
using System.Web;
using System.Xml;

namespace Tony.Util
{
    /// <summary>
    /// Config文件操作
    /// </summary>
   public class Config
    {
        /// <summary>
        /// 根据key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].Trim();
        }

        /// <summary>
        /// 根据key修改值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, string value)
        {
            var xDoc = new XmlDocument();
            var fileName = HttpContext.Current.Server.MapPath("~/XmlConfig/system.config");
            xDoc.Load(fileName);
            var xNode = xDoc.SelectSingleNode("//appSettings");
            var xElem1 = (XmlElement) xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null)
            {
                xElem1.SetAttribute("value", value);
            }
            else
            {
                var xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(fileName);
        }
    }
}
