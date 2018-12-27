// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwitchUnitTest.cs" company="">
//   
// </copyright>
// <summary>
//   SwitchUnitTest 的摘要说明
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using TAF.Core.Utility;
using Xunit;

namespace TAF.Core.Test
{
    /// <summary>
    /// SwitchUnitTest 的摘要说明
    /// </summary>
    
    public class SwitchUnitTest
    {
        /// <summary>
        /// The test method 1.
        /// </summary>
        [Fact]
        public void Fact1()
        {
            var typeName = string.Empty;
            var item = 0;
            item.Switch((string s) => typeName = s)
                .Case(0, "食品")
                .Case(1, "饮料")
                .Case(2, "酒水")
                .Case(3, "毒药")
                .Default("未知");
            Assert.Equal("食品",typeName);

            const string password = "xxxxxx";
            var result = 0;
            password.Switch(
                            p => p.Length, 
                            (int c) =>
                            {
                                result = c;
                            })
                .Case(l => l <= 4, 100)
                .Case(l => l <= 6, 200)
                .Case(7, 300)
                .Case(8, 400)
                .Default(500);
            Assert.Equal(result, 200);
        }
    }
}