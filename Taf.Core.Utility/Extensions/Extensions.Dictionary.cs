using System.Collections.Generic;
using System.Linq;

namespace Taf.Core.Utility
{
    /// <summary>
    /// Dictionary扩展
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 向字典中批量添加键值对
        /// 没有考虑线程安全的问题
        /// </summary>
        /// <param name="dict">
        /// The dict.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="replaceExisted">
        /// 如果已存在，是否替换
        /// </param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted) where TKey : notnull{
            foreach (var item in values.Where(item => dict.ContainsKey(item.Key) == false || replaceExisted))
            {
                dict[item.Key] = item.Value;
            }

            return dict;
        }
    }
}