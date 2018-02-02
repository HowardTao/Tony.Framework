using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Tony.Util
{
    /// <summary>
    /// Json操作
    /// </summary>
   public static class Json
    {
        public static object ToJson(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject(json);
        }

        public static T ToObject<T>(this string json)
        {
            return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public static List<T> ToList<T>(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static DataTable ToTable(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }

        public static JObject ToJObject(this string json)
        {
            return json == null ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }

        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"};
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        public static string ToJson(this object obj,string datetimeFormat)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeFormat };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }


    }
}
