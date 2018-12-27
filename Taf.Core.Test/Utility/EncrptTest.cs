// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncrptTest.cs" company="">
//   
// </copyright>
// <summary>
//   测试Md5算法
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using TAF.Core.Utility;
using Xunit;

namespace TAF.Core.Test
{
    /// <summary>
    /// 测试Md5算法
    /// </summary>
    
    public class Md5Test
    {
        /// <summary>
        /// 验证空值
        /// </summary>
        [Fact]
        public void TestMd5_Validate_Empty_16()
        {
            Assert.Equal(string.Empty, Encrypt.Md5By16(null));
            Assert.Equal(string.Empty, Encrypt.Md5By16(string.Empty));
        }

        /// <summary>
        /// 加密字符串，返回16位结果
        /// </summary>
        [Fact]
        public void TestMd5_String_16()
        {
            Assert.Equal("C0F1B6A831C399E2", Encrypt.Md5By16("a"));
            Assert.Equal("CB143ACD6C929826", Encrypt.Md5By16("中国"));
        }

        /// <summary>
        /// 验证空值
        /// </summary>
        [Fact]
        public void TestMd5_Validate_Empty_32()
        {
            Assert.Equal(string.Empty, Encrypt.Md5By32(null));
            Assert.Equal(string.Empty, Encrypt.Md5By32(string.Empty));
        }

        /// <summary>
        /// 加密字符串，返回32位结果
        /// </summary>
        [Fact]
        public void TestMd5_String_32()
        {
            Assert.Equal("0CC175B9C0F1B6A831C399E269772661", Encrypt.Md5By32("a"));
            Assert.Equal("C13DCEABCB143ACD6C9298265D618A9F", Encrypt.Md5By32("中国"));
        }

        /// <summary>
        /// 可逆加密解密
        /// </summary>
        [Fact]
        public void TestGetPass()
        {
            var pass = Encrypt.GetNewPassword(10);
            Assert.NotNull(pass);
            Assert.Equal(Encrypt.DesEncrypt("149162536"), "yNoRIOe2AZbE1DouLrHENQ==");
            Assert.Equal(Encrypt.DesDecrypt("yNoRIOe2AZbE1DouLrHENQ=="), "149162536");
        }
    }
}