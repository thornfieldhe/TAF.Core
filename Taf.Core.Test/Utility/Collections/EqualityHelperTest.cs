using System;
using System.Collections.Generic;
using Taf.Core.Utility;
using Taf.Test;
using Xunit;

namespace Taf.Core.Test
{
    /// <summary>
    /// EqualityHelperTest 的摘要说明
    /// </summary>
    public class EqualityHelperTest
    {

        /// <summary>
        /// 普通对象提供相等判定的扩展
        /// </summary>
        [Fact]
        public void CreateComparerTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "b" },
                                       };
            var info = new TestInfo { Id = Guid.NewGuid(), Name = "a" };
            var comparer = Equality<TestInfo>.CreateComparer(m => m.Name);
            Assert.Contains(info, list1, comparer);

            Assert.True(list1.Contains(info, r => r.Name));
        }
    }
}
