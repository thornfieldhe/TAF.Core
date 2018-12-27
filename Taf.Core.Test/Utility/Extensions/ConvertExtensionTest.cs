// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   类型转换扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using TAF.Core.Utility;
using Xunit;
using System;
using System.Collections.Generic;

namespace TAF.Core.Test
{
    /// <summary>
    /// 类型转换扩展测试
    /// </summary>
    
    public class ConvertExtensionTest
    {
        /// <summary>
        /// 转换为整数
        /// </summary>
        [Fact]
        public void TestToInt()
        {
            var obj1 = string.Empty;
            var obj2 = "1";
            Assert.Equal(0, obj1.ToInt());
            Assert.Equal(1, obj2.ToInt());
            Assert.Equal(1, true.ToInt());
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        [Fact]
        public void TestToBool()
        {
            var obj1 = -1;
            string obj2 = null;
            var obj3 = "否";
            Assert.Equal(false, obj1.ToBool());
            Assert.Equal(false, obj2.ToBool());
            Assert.Equal(false, obj3.ToBool());
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        [Fact]
        public void TestToBoolOrNull()
        {
            string obj1 = null;
            var obj2 = "1";
            Assert.Equal(null, obj1.ToBoolOrNull());
            Assert.Equal(true, obj2.ToBoolOrNull());
        }

        /// <summary>
        /// 转换为可空整数
        /// </summary>
        [Fact]
        public void TestToIntOrNull()
        {
            string obj1 = null;
            var obj2 = "1";
            Assert.Null(obj1.ToIntOrNull());
            Assert.Equal(1, obj2.ToIntOrNull());
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        [Fact]
        public void TestToDouble()
        {
            var obj1 = string.Empty;
            var obj2 = "1.2";
            Assert.Equal(0, obj1.ToDouble());
            Assert.Equal(1.2, obj2.ToDouble());
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        [Fact]
        public void TestToDoubleOrNull()
        {
            string obj1 = null;
            var obj2 = "1.2";
            Assert.Null(obj1.ToDoubleOrNull());
            Assert.Equal(1.2, obj2.ToDoubleOrNull());
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        [Fact]
        public void TestToDecimal()
        {
            var obj1 = string.Empty;
            var obj2 = "1.2";
            Assert.Equal(0, obj1.ToDecimal());
            Assert.Equal(1.2M, obj2.ToDecimal());
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        [Fact]
        public void TestToDecimalOrNull()
        {
            string obj1 = null;
            var obj2 = "1.2";
            Assert.Null(obj1.ToDecimalOrNull());
            Assert.Equal(1.2M, obj2.ToDecimalOrNull());
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        [Fact]
        public void TestToDate()
        {
            var obj1 = string.Empty;
            var obj2 = "2000-1-1";
            Assert.Equal(DateTime.Today, obj1.ToDate().Date);
            Assert.Equal(new DateTime(2000, 1, 1), obj2.ToDate());
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        [Fact]
        public void TestToDateOrNull()
        {
            var obj1 = string.Empty;
            var obj2 = "2000-1-1";
            Assert.Null(obj1.ToDateOrNull());
            Assert.Equal(new DateTime(2000, 1, 1), obj2.ToDateOrNull());
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        [Fact]
        public void TestToGuid()
        {
            var obj1 = string.Empty;
            var obj2 = "B9EB56E9-B720-40B4-9425-00483D311DDC";
            Assert.Equal(Guid.Empty, obj1.ToGuid());
            Assert.Equal(new Guid(obj2), obj2.ToGuid());
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        [Fact]
        public void TestToGuidOrNull()
        {
            string obj1 = null;
            var obj2 = "B9EB56E9-B720-40B4-9425-00483D311DDC";
            Assert.Null(obj1.ToGuidOrNull());
            Assert.Equal(new Guid(obj2), obj2.ToGuidOrNull());
        }

        /// <summary>
        /// 转换为Guid集合,值为字符串
        /// </summary>
        [Fact]
        public void TestToGuidList_String()
        {
            const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A,,EAB523C6-2FE7-47BE-89D5-C6D440C3033A,";
            Assert.Equal(2, guid.ToList<Guid>().Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), guid.ToGuidList()[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), guid.ToGuidList()[1]);
        }

        /// <summary>
        /// 转换为Guid集合,值为字符串集合
        /// </summary>
        [Fact]
        public void TestToGuidList_StringList()
        {
            var list = new List<string>
                           {
                               "83B0233C-A24F-49FD-8083-1337209EBC9A", 
                               "EAB523C6-2FE7-47BE-89D5-C6D440C3033A"
                           };
            Assert.Equal(2, list.ToList<Guid>().Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), list.ToList<Guid>()[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), list.ToList<Guid>()[1]);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        [Fact]
        public void TestToStr()
        {
            object value = null;
            Assert.Equal(string.Empty, value.ToStr());
            value = 1;
            Assert.Equal("1", value.ToStr());
        }

        /// <summary>
        /// 转换为指定对象
        /// </summary>
        [Fact]
        public void TestToT()
        {
            var obj1 = 1;
            var obj2 = true;
            var obj3 = "2014-5-1";
            var obj4 = "cc";
            Assert.Equal("1", obj1.To<string>());
            Assert.Equal("True", obj2.To<string>());
            Assert.Equal(new DateTime(2014, 5, 1), obj3.To<DateTime?>());
            Assert.Equal(null, obj4.To<DateTime?>());
        }
    }
}