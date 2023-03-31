// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_DateTime_TimeAgo.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

// 何翔华
// Taf.Core.Utility
// Extensions.DateTime.TimeAgo.cs

namespace Taf.Core.Utility;

/// <summary>
/// 月相关函数
/// </summary>
public interface IDateOfMonth : IExtension<DateTime>{ }

/// <summary>
/// 
/// </summary>
public static class DateOfMonth{
    /// <summary>
    /// 计算指定月天数
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int GetCountDaysOfMonth(this IDateOfMonth date){
        var nextMonth = date.GetValue().AddMonths(1);
        return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
    }


    /// <summary>
    /// 获取季度
    /// </summary>
    /// <param name="datetime">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int GetQuarter(this IDateOfMonth datetime){
        var dt = datetime.GetValue();
        if(dt.Month <= 3){
            return 1;
        }

        if(dt.Month <= 6){
            return 2;
        }

        return dt.Month <= 9 ? 3 : 4;
    }

    /// <summary>
    /// 日期所在月第一天
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetFirstDayOfMonth(this IDateOfMonth date){
        var dt = date.GetValue();
      return  new DateTime(dt.Year, dt.Month, 1);
    } 

    /// <summary>
    /// 日期所在月第一天
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="dayOfWeek">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetFirstDayOfMonth(this IDateOfMonth date, DayOfWeek dayOfWeek){
        var dt = date.GetFirstDayOfMonth();
        while(dt.DayOfWeek != dayOfWeek){
            dt = dt.AddDays(1);
        }

        return dt;
    }

    /// <summary>
    /// 日期所在月最后一天
    /// </summary>
    /// <param name="dt">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetLastDayOfMonth(this IDateOfMonth dt){
        var date = dt.GetValue();
      return  new DateTime(date.Year, date.Month, GetCountDaysOfMonth(dt)); 
    }
       

    /// <summary>
    /// 日期所在月最后一天
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="dayOfWeek">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetLastDayOfMonth(this IDateOfMonth date, DayOfWeek dayOfWeek){
        var dt = date.GetLastDayOfMonth();
        while(dt.DayOfWeek != dayOfWeek){
            dt = dt.AddDays(-1);
        }

        return dt;
    }

}
