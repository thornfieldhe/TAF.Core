// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_DateTime_Day.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

// 何翔华
// Taf.Core.Utility
// Extensions.DateTime.Day.cs

namespace Taf.Core.Utility;

/// <summary>
/// 日相关函数
/// </summary>
public interface IDateOfDay : IExtension<DateTime>{ }

/// <summary>
/// $Summary$
/// </summary>
public static class DateOfDay{
            
    /// <summary>
    /// 明天
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime NextDay(this IDateOfDay @this) => @this.StartOfDay().AddDays(1);

    /// <summary>
    /// 昨天
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime Yesterday(this IDateOfDay @this) => @this.StartOfDay().AddDays(-1);

    /// <summary>
    /// 判断日期是否是今日
    /// </summary>
    /// <param name="dt">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsToday(this IDateOfDay dt) => dt.GetValue().Date == DateTime.Today;

    /// <summary>
    /// 判断dto日期是否是今日
    /// </summary>
    /// <param name="dto">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsToday(this DateTimeOffset dto) => dto.IsToday();
        
    /// <summary>
    /// 返回当日结束时间 23:59:59;
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime EndOfDay(this IDateOfDay @this) => @this.GetValue().Date.AddDays(1).AddSeconds(-1);

    /// <summary>
    /// 返回当日开始时间 00:00:00
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime StartOfDay(this IDateOfDay @this) => @this.GetValue().Date;

    /// <summary>
    /// 返回几秒钟前时间
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime SecondsAgo(this int @this) => DateTime.Now.AddSeconds(-@this);

}
