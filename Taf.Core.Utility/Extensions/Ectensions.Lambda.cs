// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ectensions.Lambda.cs" company="">
//   
// </copyright>
// <summary>
//   The where if extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Taf.Core.Utility
{
    /// <summary>
    /// The where if extension.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T>        source,
            Expression<Func<T, bool>> predicate,
            bool                      condition) =>
            condition ? source.Where(predicate) : source;

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T>             source,
            Expression<Func<T, int, bool>> predicate,
            bool                           condition) =>
            condition ? source.Where(predicate) : source;

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// IEnumerable<T>
        /// </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition) => condition ? source.Where(predicate) : source;

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            Func<T, int, bool>  predicate,
            bool                condition) =>
            condition ? source.Where(predicate) : source;

        public static bool Contains<T, V>(this IEnumerable<T> source, T value, Func<T, V> keySelector) => source.Contains(value, Equality<T>.CreateComparer(keySelector));

        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector) => source.Distinct(new CommonEqualityComparer<T, V>(keySelector));

        /// <summary>
        /// 去除重复项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer) => source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));


        /// <summary>
        /// 查找数组第一条满足需求的元素中所在位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            var i = 0;
            foreach (var o in self)
            {
                if (predicate(o)) return i;
                i++;
            }
            return -1;
        }
    }
}