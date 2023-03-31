// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.NullOrEmpty.cs" company="">
//   
// </copyright>
// <summary>
//   NULL扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Taf.Core.Utility
{
    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsNull<T>(this T @this) => @this == null;

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNotNull<T>(this T @this) => @this != null;

        /// <summary>
        /// 检测空值,为null则抛出ArgumentNullException异常
        /// </summary>
        /// <param name="obj">
        /// 对象
        /// </param>
        /// <param name="parameterName">
        /// 参数名
        /// </param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// String是否为空
        /// </summary>
        /// <param name="value">
        /// 值
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// 可空GUID是否为空
        /// </summary>
        /// <param name="value">
        /// 值
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsEmpty(this Guid? value) => value == null || IsEmpty(value.Value);

        /// <summary>
        /// GUID是否为空
        /// </summary>
        /// <param name="value">
        /// 值
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsEmpty(this Guid value) => value == Guid.Empty;

        /// <summary>
        /// 日期是否是最小值
        /// </summary>
        /// <param name="value">
        /// 值
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsValid(this DateTime value) => value > new DateTime(1900, 1, 1);

        /// <summary>
        /// 不为空执行委托方法
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="func">
        /// </param>
        public static void IfNotNull<T>(this T @this, Action<T> func)
        {
            if (@this != null)
            {
                func(@this);
            }
        }

        /// <summary>
        /// 为空执行委托方法
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        public static void IfNull<T>(this T @this, Action action)
        {
            if (@this == null)
            {
                action();
            }
        }

        /// <summary>
        /// 如果对象为空返回string.Empty,否则返回value.ToString()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStr(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        /// <summary>
        /// 为真执行委托方法
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IfTrue(this bool @this, Action action)
        {
            if (@this)
            {
                action();
            }

            return @this;
        }

        /// <summary>
        /// 如果为假，执行委托方法
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IfFalse(this bool @this, Action action)
        {
            if (!@this)
            {
                action();
            }

            return @this;
        }

        /// <summary>
        /// 如果为真，返回泛型默认值
        /// </summary>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="content">
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult WhenTrue<TResult>(this bool @this, TResult content) => @this ? content : default(TResult);

        /// <summary>
        /// 如果结果为假，执行指定委托方法
        /// </summary>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="exp">
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult WhenFalse<TResult>(this bool @this, Func<TResult> exp) => !@this ? exp() : default(TResult);

        /// <summary>
        /// 如果为假，返回泛型默认值
        /// </summary>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="content">
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult WhenFalse<TResult>(this bool @this, TResult content) => !@this ? content : default(TResult);

        /// <summary>
        /// 判断是否为泛型类型
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Is<T>(this T @this) => @this is T;

        // /// <summary>
        // /// 对象安全转换为指定类型
        // /// </summary>
        // /// <typeparam name="T">
        // /// </typeparam>
        // /// <param name="this">
        // /// </param>
        // /// <returns>
        // /// The <see cref="T"/>.
        // /// </returns>
        // public static T As<T>(this T @this) where T : class => @this;

        /// <summary>
        /// 锁定对象后执行方法
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Lock<T>(this T @this, Action<T> action)
        {
            var lockObject = new object();
            lock (lockObject)
            {
                action(@this);
            }

            return @this;
        }

        /// <summary>
        /// 当前值是否在范围中
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="lower">
        /// </param>
        /// <param name="upper">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Between<T>(this T @this, T lower, T upper) where T : IComparable<T> => @this.CompareTo(lower) >= 0 && @this.CompareTo(upper) < 0;

        /// <summary>
        /// 使用lambda表达式更新对象属性.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        public static T Set<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        /// <summary>
        /// 安全读取对象属性
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult SafeValue<T, TResult>(this T @this, Func<T, TResult> action)
        {
            if (@this == null)
            {
                return default;
            }

            try
            {
                return action(@this);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 安全创建对象
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SafeValue<T>(this T? @this) where T : struct => @this ?? default(T);

        /// <summary>
        /// 安全创建对象
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SafeValue<T>(this T @this) where T : class, new() => @this ?? new T(); 

        /// <summary>
        /// 通过表达式返回对象，对象如果为空则返回默认值
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// </typeparam>
        /// <param name="this">
        /// </param>
        /// <param name="exp">
        /// </param>
        /// <param name="elseValue">
        /// </param>
        /// <returns>
        /// The <see cref="TReturn"/>.
        /// </returns>
        public static T NullOr<T>(
            this T      @this,
            T exp)  =>
            @this != null ? @this : exp;
        
    }
}