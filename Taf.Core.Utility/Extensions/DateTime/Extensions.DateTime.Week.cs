// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_DateTime_YearMonth.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;

// 何翔华
// Taf.Core.Utility
// Extensions.DateTime.YearMonth.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
/// 周相关函数
/// </summary>
public interface IDateOfWeek : IExtension<DateTime>{ }

/// <summary>
/// 
/// </summary>
public static class DateOfWeek{
    /// <summary>
    /// 获取日期周数
    /// </summary>
    /// <param name="datetime">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int WeekOfYear(this IDateOfWeek datetime){
        var dateinf        = new DateTimeFormatInfo();
        var weekrule       = dateinf.CalendarWeekRule;
        var firstDayOfWeek = dateinf.FirstDayOfWeek;
        return WeekOfYear(datetime, weekrule, firstDayOfWeek);
    }

    /// <summary>
    /// 获取日期周数
    /// </summary>
    /// <param name="datetime">
    /// </param>
    /// <param name="weekrule">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int WeekOfYear(this IDateOfWeek datetime, CalendarWeekRule weekrule){
        var dateinf        = new DateTimeFormatInfo();
        var firstDayOfWeek = dateinf.FirstDayOfWeek;
        return WeekOfYear(datetime, weekrule, firstDayOfWeek);
    }

    /// <summary>
    /// 获取日期周数
    /// </summary>
    /// <param name="datetime">
    /// </param>
    /// <param name="firstDayOfWeek">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int WeekOfYear(this IDateOfWeek datetime, DayOfWeek firstDayOfWeek){
        var dateinf  = new DateTimeFormatInfo();
        var weekrule = dateinf.CalendarWeekRule;
        return WeekOfYear(datetime, weekrule, firstDayOfWeek);
    }

    /// <summary>
    /// 获取日期周数
    /// </summary>
    /// <param name="datetime">
    /// </param>
    /// <param name="weekrule">
    /// </param>
    /// <param name="firstDayOfWeek">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int WeekOfYear(this IDateOfWeek datetime, CalendarWeekRule weekrule, DayOfWeek firstDayOfWeek){
        var ciCurr = CultureInfo.CurrentCulture;
        return ciCurr.Calendar.GetWeekOfYear(datetime.GetValue(), weekrule, firstDayOfWeek);
    }


    /// <summary>
    /// 是否是工作日
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWeekDay(this IDateOfWeek date) => !date.IsWeekend();

    /// <summary>
    /// 是否是周末
    /// </summary>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWeekend(this IDateOfWeek value) => IsWeekend(value.GetValue());

    /// <summary>
    /// 是否是周末
    /// </summary>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool IsWeekend(DateTime value) =>
        value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;


    /// <summary>
    /// 获取X个工作日后日期
    /// </summary>
    /// <param name="from"></param>
    /// <param name="days"></param>
    /// <returns></returns>
    public static DateTime AddWeekend(this IDateOfWeek from, int days){
        var total = 0;
        var dt    = from.GetValue();
        if(days > 0){
            for(var i = 1; i <= days; i++){
                dt = CheckAndAddDays(dt, days > 0, ref total);
            }
        } else{
            for(var i = -1; i >= days; i--){
                dt = CheckAndAddDays(dt, days > 0, ref total);
            }
        }


        return dt;
    }


    /// <summary>
    /// 获取日期所在周一日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetFirstDayOfWeek(this IDateOfWeek date) => date.GetFirstDayOfWeek(null);

    /// <summary>
    /// 获取日期所在周一日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="cultureInfo">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetFirstDayOfWeek(this IDateOfWeek date, CultureInfo cultureInfo){
        cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var dt             = date.GetValue();
        while(dt.DayOfWeek != firstDayOfWeek){
            dt = dt.AddDays(-1).Date;
        }

        return dt;
    }

    /// <summary>
    /// 获取日期所在周末日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetLastDayOfWeek(this IDateOfWeek date) => date.GetLastDayOfWeek(null);

    /// <summary>
    /// 获取日期所在周末日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="cultureInfo">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetLastDayOfWeek(this IDateOfWeek date, CultureInfo cultureInfo) =>
        date.GetFirstDayOfWeek(cultureInfo).AddDays(6);

    /// <summary>
    /// 获取工作日日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="weekday">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetWeekday(this IDateOfWeek date, DayOfWeek weekday) => date.GetWeekday(weekday, null);

    /// <summary>
    /// 获取工作日日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="weekday">
    /// </param>
    /// <param name="cultureInfo">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetWeekday(this IDateOfWeek date, DayOfWeek weekday, CultureInfo cultureInfo){
        var firstDayOfWeek = date.GetFirstDayOfWeek(cultureInfo);
        return GetNextWeekday(firstDayOfWeek, weekday);
    }

    /// <summary>
    /// 获取下周周末
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="weekday">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetNextWeekday(this IDateOfWeek date, DayOfWeek weekday){
        var dt = date.GetValue();
        while(dt.DayOfWeek != weekday){
            dt = dt.AddDays(1);
        }

        return dt;
    }

    /// <summary>
    /// 获取下周日日期
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="weekday">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime GetPreviousWeekday(this IDateOfWeek date, DayOfWeek weekday){
        var dt = date.GetValue();
        while(dt.DayOfWeek != weekday){
            dt = dt.AddDays(-1);
        }

        return dt;
    }

    /// <summary>
    /// 增加周
    /// </summary>
    /// <param name="dt">
    /// </param>
    /// <param name="count">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime AddWeeks(this IDateOfWeek dt, int count){
        var dateBegin = GetWeekday(dt, DayOfWeek.Monday);
        return dateBegin.AddDays(7 * count);
    }

    /// <summary>
    /// 获取下周周末
    /// </summary>
    /// <param name="date">
    /// </param>
    /// <param name="weekday">
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    private static DateTime GetNextWeekday(DateTime date, DayOfWeek weekday){
        while(date.DayOfWeek != weekday){
            date = date.AddDays(1);
        }

        return date;
    }
    
    
    /// <summary>
    /// 检查日期+1后是否是周末,并更新总天数
    /// </summary>
    /// <param name="date"></param>
    /// <param name="add"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    private static DateTime CheckAndAddDays(DateTime date, bool add, ref int total){
        var d = add ? date.AddDays(1) : date.AddDays(-1);

        total++;
        if(IsWeekend(d)){
            d = CheckAndAddDays(d, add, ref total);
        }

        return d;
    }
}
