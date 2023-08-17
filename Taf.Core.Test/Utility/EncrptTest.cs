// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncrptTest.cs" company="">
//   
// </copyright>
// <summary>
//   测试Md5算法
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;

namespace Taf.Core.Test
{
    /// <summary>
    /// 测试Md5算法
    /// </summary>
    
    public class EncrptTest
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

        [Fact]
        public void TestRsa()
        {
            //2048 公钥
            const string privateKey = @"
MIICXgIBAAKBgQDiPLElDNs/7sL9ODdGZuLGnuUg1xuMyP+wBcvejpjKG6Lpnp/c
z5tLBwFK4nWdGJaflnWRTQEgQWXI3asTFTu9tQfxitB220L+G2llZQnfczSewkTk
GGJU3svTvE33gf0n57WD8PmsrctVj7SE1QHwc2Aaw3c9EpEued0VHhW1uwIDAQAB
AoGBAL3YdkKEBlwg7Kl3ChNe9P/9iILFX44FgWJhitGI0bgP9uaaQMYXxNXx2+jO
Hiok3wiuRpwWhVJe7p3fPda2o7ia+SmSnOPTpoaFpbROgosh9I/y2N0ZthLjKphO
yUO1bD5J79jl6AQy2e5RoUWyxb/YFD3GP0IWaO8SKAUQjsSJAkEA9fD8HRpsEzux
XiOyrdKzUA+7/F4gxul3l68T6wlJl/bLFtP/R3axTWJbmCSZ0XP/c/xCh4Vuf15z
Hfbj/ypXrwJBAOt9Zxg5oHDUew0CxOk0t1xEQZCfG/lfErOlMrueuQbrIK/W3lA3
PY6RcPf82PA9KS3iKZ6OJavGu/8M/qKMebUCQQCWT6BmUR24a4Utmfe8UMgSqGsm
BIlXyJy08eXkghqea9EXtJ2SmbInL3P3encgEhsQUh5/IUe1RI5qw9f4vXI1AkEA
kImy2nKcYER6llzJwZ2ioZYfbAXMpL3O+8Z8oh3k0TNGJ8dJQpD1TmlEnmFqQeI4
QTpccz4qLwnW38/5BooUNQJAF0bwvrp8rjmxxqKPtQ9Gy93v+ISLPpVhY1ewxd3R
347epX5aCDVVNcdu/ZJEi7flngqG0uSC2ai7s9OqBA9vRA==
                ";
            //2048 私钥
            const string publicKey = @"
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDiPLElDNs/7sL9ODdGZuLGnuUg
1xuMyP+wBcvejpjKG6Lpnp/cz5tLBwFK4nWdGJaflnWRTQEgQWXI3asTFTu9tQfx
itB220L+G2llZQnfczSewkTkGGJU3svTvE33gf0n57WD8PmsrctVj7SE1QHwc2Aa
w3c9EpEued0VHhW1uwIDAQAB
                ";
            const string str = "博客园 http://www.cnblogs.com/";
            
            var rsa = new RsaHelper(RsaType.Rsa2, Encoding.UTF8, privateKey, publicKey);
            
            //加密字符串
            var enStr = rsa.Encrypt(str);

            //解密字符串
            var deStr = rsa.Decrypt(enStr);
            Assert.Equal(str,deStr);

            //私钥签名
            var signStr = rsa.Sign(str);
            //公钥验证签名
            var signVerify = rsa.Verify(str, signStr);

            Assert.True(signVerify);
        }
        
        /// <summary>
        /// 测试国密加密/解密
        /// </summary>
        [Fact]
        public void TestSm(){
            var txt  = "abcdef";
            Assert.Equal("aa04b90b3546a6e010e6c2c5af045c2b",Encrypt.Sm4Encrypt(txt));
            Assert.Equal(txt, Encrypt.Sm4Decrypt("aa04b90b3546a6e010e6c2c5af045c2b"));
            var txt2 = Encrypt.Sm2Encrypt(txt);
            Assert.Equal(txt, Encrypt.Sm2Decrypt(txt2));
            
        }
    }
}