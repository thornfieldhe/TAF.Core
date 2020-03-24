// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByExtention.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   针对List OrderBy的扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Taf.Core.Utility{
    using System;

    /// <summary>
    /// 针对List OrderBy的扩展
    /// </summary>
    public static class OrderByExtention{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="sources"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderBy<T, R>(this IEnumerable<T> source, Func<T, R> keySelector
                                                        , R[]                 sources) where R : IComparable<R>{
            return source.OrderBy(keySelector, new DefaultOrderComparer<R>(sources));
        }
    }

    /// <summary>
    /// 针对List ThenBy的扩展
    /// </summary>
    public static class ThenByExtention{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="sources"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenBy<T, R>(this IOrderedEnumerable<T> source, Func<T, R> keySelector
                                                        , R[]                 sources) where R : IComparable<R>{
            return source.ThenBy(keySelector, new DefaultOrderComparer<R>(sources));
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public class DefaultOrderComparer<R> : IComparer<R> where R : IComparable<R>{
        private readonly R[] _types;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        public DefaultOrderComparer(R[] types) => _types = types;

        public int Compare(R x, R y){
            int index1 = _types.FindIndex(r => r.Equals(x));
            int index2 = _types.FindIndex(r => r.Equals(y));
            if(index1 < 0){
                if(index2 < 0)
                    return x.CompareTo(y);
                return 1;
            }

            if(index2 < 0)
                return -1;
            return index1.CompareTo(index2);
        }
    }
}
