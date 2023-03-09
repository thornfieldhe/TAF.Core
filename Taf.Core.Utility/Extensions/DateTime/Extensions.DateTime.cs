// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.DateTime.cs" company="">
//   
// </copyright>
// <summary>
//   时间扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Core.Utility{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions{
        /// <summary>
        /// 获取两个时间间隔
        /// </summary>
        /// <param name="startTime">
        /// </param>
        /// <param name="endTime">
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
        public static TimeSpan GetTimeSpan(this DateTime startTime, DateTime endTime) => endTime - startTime;

        /// <summary>
        /// 时间是否处于时间范围中
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="startDate">
        /// </param>
        /// <param name="endDate">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsWithin(this DateTime @this, DateTime startDate, DateTime endDate) =>
            @this > startDate && @this < endDate;

        /// <summary>
        /// 返回所在年的第几天的具体日期
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="year">
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime DayInYear(this int @this, int? year = null){
            var firstDayOfYear = new DateTime(year ?? DateTime.Now.Year, 1, 1);
            return firstDayOfYear.AddDays(@this - 1);
        }

        /// <summary>
        /// 获取Java 13位时间戳转DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDateTime(this string timeStamp){
            DateTime ConvertString(int length){
                var dtStart = new DateTime(1970, 1, 1, 0, 0, 0);
                var lTime   = long.Parse(timeStamp + new string('0', length));
                var toNow   = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }

            if(long.TryParse(timeStamp, out _)){
                if(timeStamp.Length == 13){
                    return ConvertString(4);
                } else if(timeStamp.Length == 18){
                    return new DateTime(timeStamp.ToLong());
                } else if(timeStamp.Length == 10){
                    return ConvertString(7);
                }
            }

            throw new NotSupportedException("不支持该字符串格式转成时间");
        }
    }
}
