using System.Collections.Generic;
using System.Linq;

namespace Tony.Util
{
    /// <summary>
    /// 可以根据字段过滤重复的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Comparint<T>:IEqualityComparer<T> where T:class ,new()
    {
        private readonly string[] _comparintFieldName = { };
        public Comparint() { }

        public Comparint(params string[] comparintFieldName)
        {
            this._comparintFieldName = comparintFieldName;
        }

        public bool Equals(T x, T y)
        {
            if (x == null || y == null) return false;
            if (_comparintFieldName.Length == 0) return x.Equals(y);
            var result = true;
            var typeX = x.GetType();
            var typeY = y.GetType();
            foreach (var fieldName in _comparintFieldName)
            {
                var xPropInfo =
                    (from p in typeX.GetProperties() where p.Name.Equals(fieldName) select p).FirstOrDefault();
                var yPropInfo =
                    (from p in typeY.GetProperties() where p.Name.Equals(fieldName) select p).FirstOrDefault();
                result = result && xPropInfo != null && yPropInfo != null && xPropInfo.GetValue(x, null).ToString()
                    .Equals(yPropInfo.GetValue(y, null));
            }
            return result;
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
