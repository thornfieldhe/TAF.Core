﻿using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test
{
    using System;
    using System.Text;

    /// <summary>
    /// 测试中国日历
    /// </summary>
    public class ChineseCalendarTest
    {
        /// <summary>
        /// 农历
        /// </summary>
        [Fact]
        public void TestChineseCalendar()
        {
            ChineseCalendar c       = new ChineseCalendar(new DateTime(1990, 01, 15));
            StringBuilder   dayInfo = new StringBuilder();
            dayInfo.Append("阳历：" + c.DateString                  + "\r\n");
            dayInfo.Append("农历：" + c.ChineseDateString           + "\r\n");
            dayInfo.Append("星期："                                 + c.WeekDayStr);
            dayInfo.Append("时辰：" + c.ChineseHour                 + "\r\n");
            dayInfo.Append("属相：" + c.AnimalString                + "\r\n");
            dayInfo.Append("节气：" + c.ChineseTwentyFourDay        + "\r\n");
            dayInfo.Append("前一个节气：" + c.ChineseTwentyFourPrevDay + "\r\n");
            dayInfo.Append("下一个节气：" + c.ChineseTwentyFourNextDay + "\r\n");
            dayInfo.Append("节日：" + c.DateHoliday                 + "\r\n");
            dayInfo.Append("干支：" + c.GanZhiDateString            + "\r\n");
            dayInfo.Append("星宿：" + c.ChineseConstellation        + "\r\n");
            dayInfo.Append("星座：" + c.Constellation               + "\r\n");
            Console.Write(dayInfo.ToString());
            Assert.True(true);
        }
    }
}
