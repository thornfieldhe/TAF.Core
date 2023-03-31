// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_DateTime_Format.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

// 何翔华
// Taf.Core.Utility
// Extensions.DateTime.Format.cs

namespace Taf.Core.Utility;

/// <summary>
/// DateTime格式化
/// </summary>
public interface IDateTimeFormat : IExtension<DateTime>{ }

/// <summary>
/// 
/// </summary>
public static class DateTimeFormat{
      /// <summary>
        /// 简化日期格式：xx分钟前
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToAgo(this IDateTimeFormat date)
        {
            var timeSpan = date.GetValue().GetTimeSpan(DateTime.Now);
            if (timeSpan < TimeSpan.Zero)
            {
                return "未来";
            }

            if (timeSpan < OneMinute)
            {
                return "现在";
            }

            if (timeSpan < TwoMinutes)
            {
                return "1 分钟前";
            }

            if (timeSpan < OneHour)
            {
                return $"{timeSpan.Minutes} 分钟前";
            }

            if (timeSpan < TwoHours)
            {
                return "1 小时前";
            }

            if (timeSpan < OneDay)
            {
                return $"{timeSpan.Hours} 小时前";
            }

            if (timeSpan < TwoDays)
            {
                return "昨天";
            }

            if (timeSpan < OneWeek)
            {
                return $"{timeSpan.Days} 天前";
            }

            if (timeSpan < TwoWeeks)
            {
                return "1 周前";
            }

            if (timeSpan < OneMonth)
            {
                return $"{timeSpan.Days / 7} 周前";
            }

            if (timeSpan < TwoMonths)
            {
                return "1 月前";
            }

            if (timeSpan < OneYear)
            {
                return $"{timeSpan.Days / 31} 月前";
            }

            return timeSpan < TwoYears ? "1 年前" : $"{timeSpan.Days / 365} 年前";
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <param name="isRemoveSecond">
        /// 是否移除秒
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this IDateTimeFormat dateTime, bool isRemoveSecond = false) => dateTime.GetValue().ToString(isRemoveSecond ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyyMMddHHmmss"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this IDateTimeFormat dateTime) => dateTime.GetValue().ToString("yyyyMMddHHmm");

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <param name="isRemoveSecond">
        /// 是否移除秒
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false) => dateTime == null ? string.Empty : ToDateTimeString(dateTime.Value, isRemoveSecond);

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <param name="withOutDash">是否包含-</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateString(this IDateTimeFormat dateTime, bool withOutDash = false) => dateTime.GetValue().ToString(withOutDash ? "yyyyMMdd" : "yyyy-MM-dd");

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateString(this DateTime? dateTime) => dateTime == null ? string.Empty : ToDateString(dateTime.Value);

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToTimeString(this IDateTimeFormat dateTime) => dateTime.GetValue().ToString("HH:mm:ss");

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToTimeString(this DateTime? dateTime) => dateTime == null ? string.Empty : ToTimeString(dateTime.Value);

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToMillisecondString(this IDateTimeFormat dateTime) => dateTime.GetValue().ToString("yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToMillisecondString(this DateTime? dateTime) => dateTime == null ? string.Empty : ToMillisecondString(dateTime.Value);

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToChineseDateString(this IDateTimeFormat dateTime){
            var dt = dateTime.GetValue();
           return $"{dt.Year}年{dt.Month}月{dt.Day}日";
        } 

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToChineseDateString(this DateTime? dateTime) => !dateTime.HasValue ? string.Empty : ToChineseDateString(dateTime.Value);

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <param name="isRemoveSecond">
        /// 是否移除秒
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToChineseDateTimeString(this IDateTimeFormat dateTime, bool isRemoveSecond = false){
            var dt     = dateTime.GetValue();
            var result = new StringBuilder();
            result.AppendFormat("{0}年{1}月{2}日", dt.Year, dt.Month, dt.Day);
            result.AppendFormat(" {0}时{1}分", dt.Hour, dt.Minute);
            if (isRemoveSecond == false)
            {
                result.AppendFormat("{0}秒", dt.Second);
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">
        /// 日期
        /// </param>
        /// <param name="isRemoveSecond">
        /// 是否移除秒
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false) => dateTime == null ? string.Empty : ToChineseDateTimeString(dateTime.Value,isRemoveSecond);

        
        /// <summary>
        /// The one minute.
        /// </summary>
        private static readonly TimeSpan OneMinute = new(0, 1, 0);

        /// <summary>
        /// The two minutes.
        /// </summary>
        private static readonly TimeSpan TwoMinutes = new(0, 2, 0);

        /// <summary>
        /// The one hour.
        /// </summary>
        private static readonly TimeSpan OneHour = new(1, 0, 0);

        /// <summary>
        /// The two hours.
        /// </summary>
        private static readonly TimeSpan TwoHours = new(2, 0, 0);

        /// <summary>
        /// The one day.
        /// </summary>
        private static readonly TimeSpan OneDay = new(1, 0, 0, 0);

        /// <summary>
        /// The two days.
        /// </summary>
        private static readonly TimeSpan TwoDays = new(2, 0, 0, 0);

        /// <summary>
        /// The one week.
        /// </summary>
        private static readonly TimeSpan OneWeek = new(7, 0, 0, 0);

        /// <summary>
        /// The two weeks.
        /// </summary>
        private static readonly TimeSpan TwoWeeks = new(14, 0, 0, 0);

        /// <summary>
        /// The one month.
        /// </summary>
        private static readonly TimeSpan OneMonth = new(31, 0, 0, 0);

        /// <summary>
        /// The two months.
        /// </summary>
        private static readonly TimeSpan TwoMonths = new(62, 0, 0, 0);

        /// <summary>
        /// The one year.
        /// </summary>
        private static readonly TimeSpan OneYear = new(365, 0, 0, 0);

        /// <summary>
        /// The two years.
        /// </summary>
        private static readonly TimeSpan TwoYears = new(730, 0, 0, 0);
}
