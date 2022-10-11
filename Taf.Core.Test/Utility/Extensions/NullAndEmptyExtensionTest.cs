// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullAndEmptyExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   The null and empty test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Xunit;
using System;
using System.Collections.Generic;

namespace Taf.Core.Test
{

    /// <summary>
    /// The null and empty test.
    /// </summary>
    
    public class NullAndEmptyTest
    {
        /// <summary>
        /// The test is null.
        /// </summary>
        [Fact]
        public void TestIsNull()
        {
            string a = null;
            Assert.True(a.IsNull());
            Assert.False(a.IsNotNull());
        }

        /// <summary>
        /// The test string is empty.
        /// </summary>
        [Fact]
        public void TestStringIsEmpty()
        {
            var a = string.Empty;
            Assert.True(a.IsEmpty());
            a = null;
            Assert.True(a.IsEmpty());
        }

        /// <summary>
        /// The test guid is empty.
        /// </summary>
        [Fact]
        public void TestGuidIsEmpty()
        {
            Guid? a = null;
            Assert.True(a.IsEmpty());
            a = Guid.Empty;
            Assert.True(a.IsEmpty());
        }

        /// <summary>
        /// 测试可空整型
        /// </summary>
        [Fact]
        public void TestSafeValue_Int()
        {
            int? value = null;
            Assert.Equal(0, value.SafeValue());

            value = 1;
            Assert.Equal(1, value.SafeValue());
            List<int> b = null;
            Assert.Equal(b.SafeValue().Count, 0);
        }

        /// <summary>
        /// 测试可空DateTime
        /// </summary>
        [Fact]
        public void TestSafeValue_DateTime()
        {
            DateTime? value = null;
            Assert.Equal(DateTime.MinValue, value.SafeValue());

            value = "2000-1-1".ToDate();
            Assert.Equal(value.Value, value.SafeValue());
        }

        /// <summary>
        /// 测试可空bool
        /// </summary>
        [Fact]
        public void TestSafeValue_Boolean()
        {
            bool? value = null;
            Assert.Equal(false, value.SafeValue());

            value = true;
            Assert.Equal(true, value.SafeValue());
        }

        /// <summary>
        /// 测试可空double
        /// </summary>
        [Fact]
        public void TestSafeValue_Double()
        {
            double? value = null;
            Assert.Equal(0, value.SafeValue());

            value = 1.1;
            Assert.Equal(1.1, value.SafeValue());
        }

        /// <summary>
        /// 测试可空decimal
        /// </summary>
        [Fact]
        public void TestSafeValue_Decimal()
        {
            decimal? value = null;
            Assert.Equal(0, value.SafeValue());

            value = 1.1M;
            Assert.Equal(1.1M, value.SafeValue());
        }

        /// <summary>
        /// The test_ lock.
        /// </summary>
        [Fact]
        public void Test_Lock()
        {
            var value = "Fluentx";
            value.Lock(x => { });
        }
    }
}