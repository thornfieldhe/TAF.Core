namespace Taf.Core.Utility
{
    using System;
    using System.Collections.Generic;

    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private readonly Func<T, V> _keySelector;
        private readonly IEqualityComparer<V> _comparer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            _keySelector = keySelector;
            _comparer = comparer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y) => _comparer.Equals(_keySelector(x), _keySelector(y));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj) => _comparer.GetHashCode(_keySelector(obj));
    }
}