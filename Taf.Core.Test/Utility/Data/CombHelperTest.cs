using System;
using TAF.Core.Test.Utility;
using Xunit;

namespace TAF.Core.Test
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
