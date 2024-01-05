// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   日期时间扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
            Assert.Equal("201201021111", date.ToDate().As<IDateTimeFormat>().ToDateTimeString());
            Assert.Equal("2012-01-02 11:11", date.ToDate().As<IDateTimeFormat>().ToDateTimeString(true));
            Assert.Equal("2012-01-02 11:11:11", date.ToDate().As<IDateTimeFormat>().ToDateTimeString(false));
            Assert.Equal("2012-01-02T11:11:11Z", date.ToDate().As<IDateTimeFormat>().ToUtcDateTimeString());
        }
        
        /// <summary>
        /// unix时间字符串(13/10位)转 
        /// </summary>
        [Fact]
        public void TestConvertStringToDateTime()
        {
            Assert.Equal(new DateTime(2022,1,1).ToUniversalTime(), "1640966400000".ConvertStringToDateTime());
            Assert.Equal(new DateTime(2022,1,1).ToUniversalTime(), "1640966400".ConvertStringToDateTime());
            Assert.Equal("1640966400000", new DateTime(2022,1,1).ConvertDateTimeToInt());
        }

        /// <summary>
        /// 获取格式化日期字符串
        /// </summary>
        [Fact]
        public void TestToDateString()
        {
            var date = "2012-01-02";
            Assert.Equal(date, date.ToDate().As<IDateTimeFormat>().ToDateString());
        }

        /// <summary>
        /// 获取格式化时间字符串
        /// </summary>
        [Fact]
        public void TestToTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("11:11:11", date.ToDate().As<IDateTimeFormat>().ToTimeString());
        }

        /// <summary>
        /// 获取格式化毫秒字符串
        /// </summary>
        [Fact]
        public void TestToMillisecondString()
        {
            var date = "2012-01-02 11:11:11.123";
            Assert.Equal(date, date.ToDate().As<IDateTimeFormat>().ToMillisecondString());
        }

        /// <summary>
        /// 获取格式化中文日期字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateString()
        {
            var date = "2012-01-02";
            Assert.Equal("2012年1月2日", date.ToDate().As<IDateTimeFormat>().ToChineseDateString());
        }

        /// <summary>
        /// 获取格式化中文日期时间字符串
        /// </summary>
        [Fact]
        public void TestToChineseDateTimeString()
        {
            var date = "2012-01-02 11:11:11";
            Assert.Equal("2012年1月2日 11时11分11秒", date.ToDate().As<IDateTimeFormat>().ToChineseDateTimeString());
        }

        /// <summary>
        /// 获取时间间隔
        /// </summary>
        [Fact]
        public void TestTimeSpan()
        {
            var timeSpan = new DateTime(2015, 1, 1).GetTimeSpan(new DateTime(2015, 1, 2));

            Assert.Equal(1, timeSpan.Days);
        }

        /// <summary>
        /// 计算指定月天数
        /// </summary>
        [Fact]
        public void TestGetCountDaysOfMonth()
        {
            var days = new DateTime(2015, 1, 1).As<IDateOfMonth>().GetCountDaysOfMonth();

            Assert.Equal(31, days);
        }

        /// <summary>
        /// 计算指定月天数
        /// </summary>
        [Fact]
        public void TestWeekOfYear()
        {
            var weeks = new DateTime(2015, 1, 6).As<IDateOfWeek>().WeekOfYear();
            Assert.Equal(2, weeks);
        }

        /// <summary>
        /// 获取季度
        /// </summary>
        [Fact]
        public void TestGetQuarter()
        {
            var month = new DateTime(2015, 6, 6).As<IDateOfMonth>().GetQuarter();
            Assert.Equal(2, month);
        }

        /// <summary>
        /// 是否是周末
        /// </summary>
        [Fact]
        public void TestIsWeekend()
        {
            var isWeekend = new DateTime(2015, 6, 6).As<IDateOfWeek>().IsWeekend();
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
            var toAgo = new DateTime(2025, 6, 6).As<IDateTimeFormat>().ToAgo();
            Assert.Equal("未来", toAgo);
        }

        /// <summary>
        /// X个工作日后
        /// </summary>
        [Fact]
        public void TestAddWeekend()
        {
            var dt1 = new DateTime(2018, 6, 17).As<IDateOfWeek>().AddWeekend(10);
            Assert.Equal(new DateTime(2018,6,29), dt1);
            var dt2 = new DateTime(2018, 6, 17).As<IDateOfWeek>().AddWeekend(-10);
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

            Assert.Equal(date1, date.As<IDateOfDay>().StartOfDay());
            Assert.Equal(date2, date.As<IDateOfDay>().EndOfDay());
            Assert.Equal(date3, date.As<IDateOfDay>().NextDay());
            Assert.Equal(date4, date.As<IDateOfDay>().Yesterday());
            Assert.Equal(date5, date.As<IDateOfMonth>().GetFirstDayOfMonth());
            Assert.Equal(date6, date.As<IDateOfMonth>().GetLastDayOfMonth());
            Assert.Equal(date7, date.As<IDateOfWeek>().GetFirstDayOfWeek());
            Assert.Equal(date8, date.As<IDateOfWeek>().GetLastDayOfWeek());
            Assert.Equal(date9, date.As<IDateOfWeek>().GetWeekday(DayOfWeek.Thursday));
        }
        
        /// <summary>
        /// 根据年份和周数获取当前周一日期
        /// </summary>
        [Fact]
        public void TestGetMondayOfWeekOfYear(){
          
            var monday = DateOfWeek.GetMondayOfWeekOfYear(2023, 31);
            var weekOfYear=monday.As<IDateOfWeek>().WeekOfYear();
            Console.WriteLine(monday.ToString("yyyy-M-d"));
            Assert.Equal(31, weekOfYear);
        }
    }
}