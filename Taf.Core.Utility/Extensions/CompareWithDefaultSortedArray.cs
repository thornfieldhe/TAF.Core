using System;
using System.Collections.Generic;

namespace Taf.Core.Utility
{
    /// <summary>
    /// 列表按照默认顺序进行排序对象
    /// <example>
    ///List<string> l=new List<string>{"A","B","C","a","b","c"},若需按照"A","a","B","b","C","c"排序，则需要在比较
    /// 对象内部创建一个对已知有限数量列表。将可能元素按照固定顺序先排列好，对于传入比较的两个对象，通过对象的下标进行比较，
    /// 若不存在于该列表数据，则默认排列到最后
    /// </example>
    /// </summary>
    public class CompareWithDefaultSortedArray<T, R> : IComparer<T> where T : IComparable
    {
        private readonly R[]        _defaultSort;
        private readonly Func<T, R> _predicate;


        /// <summary>
        /// 列表按照默认顺序进行排序对象
        /// <example>
        /// string[] l=new string[]{"A","B","C","a","b","c"},若需按照"A","a","B","b","C","c"排序，则需要在比较
        /// 对象内部创建一个对已知有限数量列表。将可能元素按照固定顺序先排列好，对于传入比较的两个对象，通过对象的下标进行比较，
        /// 若不存在于该列表数据，则默认排列到最后
        /// </example>
        /// </summary>
        public CompareWithDefaultSortedArray( R[] defaultSort, Func<T, R> predicate)
        {
            _defaultSort = defaultSort;
            _predicate   = predicate;
        }

        public int Compare(T x, T y)
        {
            if(x == null
            && y == null)
            {
                return 0;
            }

            if(x == null)
            {
                return 1;
            }

            if(y == null)
            {
                return -1;
            }

            var indexX = _defaultSort.FindIndex(r => r.Equals(_predicate(x)));
            var indexY = _defaultSort.FindIndex(r => r.Equals(_predicate(y)));

            switch(indexX)
            {
                case -1 when indexY == -1:
                    return x.CompareTo(y);
                case -1:
                    return 1;
                default:
                {
                    if(indexY == -1)
                    {
                        return -1;
                    }

                    break;
                }
            }

            return indexX.CompareTo(indexY);
        }
    }
}