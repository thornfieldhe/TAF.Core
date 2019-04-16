// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogicExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   验证bool特性扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test
{

    /// <summary>
    /// 验证特性扩展
    /// </summary>
    
    public class BoolTest
    {
        /// <summary>
        /// 获取验证特性的错误消息
        /// </summary>
        [Fact]
        public void TestLogic()
        {
            Assert.False(true.Not());
            Assert.False(true.And(false));
            Assert.True(true.And(() => 1 + 1 == 2));
            Assert.False(true.AndNot(() => 1 + 1 == 2));
            Assert.True(true.AndNot(false));
            Assert.True(true.Or(false));
            Assert.True(true.Or(() => 1 + 1 == 2));
            Assert.True(true.OrNot(true));
            Assert.True(true.OrNot(() => 1 + 1 == 2));
            Assert.True(true.Xor(false));
            Assert.False(true.Xor(true));
            Assert.False(false.Xor(false));
            Assert.True(true.Xor(() => 1 + 1 != 2));
        }
    }
}