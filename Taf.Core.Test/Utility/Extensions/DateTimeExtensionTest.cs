// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   日期时间扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test
{
    /// <summary>
    /// 日期时间扩展测试
    /// </summary>
    
    public class DateTimeExtensionTest
    {
        /// <summary>
        /// 获取格式化日期时间字符串
        /// </summary>
        [Fact]
        public void TestToDateTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("201201021111", date.ToDate().ToDateTimeString());
            Assert.Equal("2012-01-02 11:11", date.ToDate().ToDateTimeString(true));
            Assert.Equal("2012-01-02 11:11:11", date.ToDate().ToDateTimeString(false));
        }
        
        /// <summary>
        /// unix时间字符串(13/10位)转 
        /// </summary>
        [Fact]
        public void TestConvertStringToDateTime()
        {
            Assert.Equal(new DateTime(2022,1,1), "1640966400000".ConvertStringToDateTime());
            Assert.Equal(new DateTime(2022,1,1), "1640966400".ConvertStringToDateTime());
        }

        /// <summary>
        /// 获取格式化日期字符串
        /// </summary>
        [Fact]
        public void TestToDateString()
        {
            var date = "2012-01-02";
            Assert.Equal(date, date.ToDate().ToDateString());
        }

        /// <summary>
        /// 获取格式化时间字符串
        /// </summary>
        [Fact]
        public void TestToTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("11:11:11", date.ToDate().ToTimeString());
        }

        /// <summary>
        /// 获取格式化毫秒字符串
        /// </summary>
        [Fact]
        public void TestToMillisecondString()
        {
            var date = "2012-01-02 11:11:11.123";
            Assert.Equal(date, date.ToDate().ToMillisecondString());
        }

        /// <summary>
        /// 获取格式化中文日期字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateString()
        {
            var date = "2012-01-02";
            Assert.Equal("2012年1月2日", date.ToDate().ToChineseDateString());
        }

        /// <summary>
        /// 获取格式化中文日期时间字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("2012年1月2日 11时11分11秒", date.ToDate().ToChineseDateTimeString());
        }

        /// <summary>
        /// 获取时间间隔
        /// </summary>
        [Fact]
        public void TestTimeSpan()
        {
            var timeSpan = new DateTime(2015, 1, 1).GetTimeSpan(new DateTime(2015, 1, 2));

            Assert.Equal(timeSpan.Days, 1);
        }

        /// <summary>
        /// 计算指定月天数
        /// </summary>
        [Fact]
        public void TestGetCountDaysOfMonth()
        {
            var days = new DateTime(2015, 1, 1).GetCountDaysOfMonth();

            Assert.Equal(days, 31);
        }

        /// <summary>
        /// 计算指定月天数
        /// </summary>
        [Fact]
        public void TestWeekOfYear()
        {
            var weeks = new DateTime(2015, 1, 6).WeekOfYear();
            Assert.Equal(weeks, 2);
        }

        /// <summary>
        /// 获取季度
        /// </summary>
        [Fact]
        public void TestGetQuarter()
        {
            var month = new DateTime(2015, 6, 6).GetQuarter();
            Assert.Equal(month, 2);
        }

        /// <summary>
        /// 是否是周末
        /// </summary>
        [Fact]
        public void TestIsWeekend()
        {
            var isWeekend = new DateTime(2015, 6, 6).IsWeekend();
            Assert.True(isWeekend);
        }

        /// <summary>
        /// 是否在日期区间内
        /// </summary>
        [Fact]
        public void TestIsWithin()
        {
            var isWithin = new DateTime(2015, 6, 6).IsWithin(new DateTime(2015, 1, 1), new DateTime(2015, 10, 1));
            Assert.True(isWithin);
        }

        /// <summary>
        /// 是否在日期区间内
        /// </summary>
        [Fact]
        public void TestToAgo()
        {
            var toAgo = new DateTime(2025, 6, 6).ToAgo();
            Assert.Equal("未来", toAgo);
        }

        /// <summary>
        /// X个工作日后
        /// </summary>
        [Fact]
        public void TestAddWeekend()
        {
            var dt1 = new DateTime(2018, 6, 17).AddWeekend(10);
            Assert.Equal(new DateTime(2018,6,29), dt1);
            var dt2 = new DateTime(2018, 6, 17).AddWeekend(-10);
            Assert.Equal(new DateTime(2018,6,4), dt2);
        }

        /// <summary>
        /// 是否在日期区间内
        /// </summary>
        [Fact]
        public void TestCurrentDate()
        {
            var date = new DateTime(2015, 1, 30, 10, 30, 5);
            var date1 = new DateTime(2015, 1, 30);
            var date2 = new DateTime(2015, 1, 30, 23, 59, 59);
            var date3 = new DateTime(2015, 1, 31);
            var date4 = new DateTime(2015, 1, 29);
            var date5 = new DateTime(2015, 1, 1);
            var date6 = new DateTime(2015, 1, 31);
            var date7 = new DateTime(2015, 1, 26);
            var date8 = new DateTime(2015, 2, 1);
            var date9 = new DateTime(2015, 1, 29);

            Assert.Equal(date1, date.StartOfDay());
            Assert.Equal(date2, date.EndOfDay());
            Assert.Equal(date3, date.NextDay());
            Assert.Equal(date4, date.Yesterday());
            Assert.Equal(date5, date.GetFirstDayOfMonth());
            Assert.Equal(date6, date.GetLastDayOfMonth());
            Assert.Equal(date7, date.GetFirstDayOfWeek());
            Assert.Equal(date8, date.GetLastDayOfWeek());
            Assert.Equal(date9, date.GetWeekday(DayOfWeek.Thursday));
        }
    }
}