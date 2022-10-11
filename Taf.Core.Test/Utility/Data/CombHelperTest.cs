using System;
using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test
{
    public class CombHelperTest
    {
        [Fact]
        public void NewCombTest()
        {
            var now = DateTime.Now;
            var id = Comb.NewComb();
            var time = Comb.GetDateFromComb(id);
            Assert.True(time.Subtract(now).TotalSeconds < 1);
        }
    }
}
