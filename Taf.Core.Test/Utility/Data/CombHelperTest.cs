using System;
using Taf.Core.Test.Utility;
using Xunit;

namespace Taf.Core.Test
{
    public class CombHelperTest
    {
        [Fact]
        public void NewCombTest()
        {
            DateTime now = DateTime.Now;
            Guid id = Comb.NewComb();
            DateTime time = Comb.GetDateFromComb(id);
            Assert.True(time.Subtract(now).TotalSeconds < 1);
        }
    }
}
