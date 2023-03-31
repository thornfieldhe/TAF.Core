﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Equality.cs" company="">
//   
// </copyright>
// <summary>
//   相等比较辅助类，用于快速创建<see cref="IEqualityComparer{T}" />的实例
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Taf.Core.Utility
{
    /// <summary>
    /// 相等比较辅助类，用于快速创建<see cref="IEqualityComparer{T}"/>的实例
    /// </summary>
    /// <example>
    /// var equalityComparer1 = Equality{Person}.CreateComparer(p => p.ID);
    /// var equalityComparer2 = Equality[Person].CreateComparer(p => p.Name);
    /// var equalityComparer3 = Equality[Person].CreateComparer(p => p.Birthday.Year);
    /// </example>
    /// <typeparam name="T"> </typeparam>
    public static class Equality<T>
    {
        #region 公共方法
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>的实例
        /// </summary>
        /// <param name="keySelector">
        /// The key Selector.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector) => new CommonEqualityComparer<TV>(keySelector);

        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>与结果二次比较器<paramref name="comparer"/>的实例
        /// </summary>
        /// <param name="keySelector">
        /// The key Selector.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IEqualityComparer<TV> comparer) => new CommonEqualityComparer<TV>(keySelector, comparer);

    #endregion

        #region Nested type: CommonEqualityComparer

        private class CommonEqualityComparer<TV> : IEqualityComparer<T>
        {
            private readonly IEqualityComparer<TV> _comparer;
            private readonly Func<T, TV> _keySelector;

            public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            public CommonEqualityComparer(Func<T, TV> keySelector)
                : this(keySelector, EqualityComparer<TV>.Default)
            {
            }

            #region IEqualityComparer<T> Members

            public bool Equals(T x, T y) => _comparer.Equals(_keySelector(x), _keySelector(y));

            public int GetHashCode(T obj) => _comparer.GetHashCode(_keySelector(obj));

        #endregion
        }

        #endregion
    }
}