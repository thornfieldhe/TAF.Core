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

        [Fact]
        public void TestRsa()
        {
            //2048 公钥
            const string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoQh0wEqx/R2H1v00IU12Oc30fosRC/frhH89L6G+fzeaqI19MYQhEPMU13wpeqRONCUta+2iC1sgCNQ9qGGf19yGdZUfueaB1Nu9rdueQKXgVurGHJ+5N71UFm+OP1XcnFUCK4wT5d7ZIifXxuqLehP9Ts6sNjhVfa+yU+VjF5HoIe69OJEPo7OxRZcRTe17khc93Ic+PfyqswQJJlY/bgpcLJQnM+QuHmxNtF7/FpAx9YEQsShsGpVo7JaKgLo+s6AFoJ4QldQKir2vbN9vcKRbG3piElPilWDpjXQkOJZhUloh/jd7QrKFimZFldJ1r6Q59QYUyGKZARUe0KZpMQIDAQAB";
            //2048 私钥
            const string privateKey = "MIIEpAIBAAKCAQEAoQh0wEqx/R2H1v00IU12Oc30fosRC/frhH89L6G+fzeaqI19MYQhEPMU13wpeqRONCUta+2iC1sgCNQ9qGGf19yGdZUfueaB1Nu9rdueQKXgVurGHJ+5N71UFm+OP1XcnFUCK4wT5d7ZIifXxuqLehP9Ts6sNjhVfa+yU+VjF5HoIe69OJEPo7OxRZcRTe17khc93Ic+PfyqswQJJlY/bgpcLJQnM+QuHmxNtF7/FpAx9YEQsShsGpVo7JaKgLo+s6AFoJ4QldQKir2vbN9vcKRbG3piElPilWDpjXQkOJZhUloh/jd7QrKFimZFldJ1r6Q59QYUyGKZARUe0KZpMQIDAQABAoIBAQCRZLUlOUvjIVqYvhznRK1OG6p45s8JY1r+UnPIId2Bt46oSLeUkZvZVeCnfq9k0Bzb8AVGwVPhtPEDh73z3dEYcT/lwjLXAkyPB6gG5ZfI/vvC/k7JYV01+neFmktw2/FIJWjEMMF2dvLNZ/Pm4bX1Dz9SfD/45Hwr8wqrvRzvFZsj5qqOxv9RPAudOYwCwZskKp/GF+L+3Ycod1Wu98imzMZUH+L5dQuDGg3kvf3ljIAegTPoqYBg0imNPYY/EGoFKnbxlK5S5/5uAFb16dGJqAz3XQCz9Is/IWrOTu0etteqV2Ncs8uqPdjed+b0j8CMsr4U1xjwPQ8WwdaJtTkRAoGBANAndgiGZkCVcc9975/AYdgFp35W6D+hGQAZlL6DmnucUFdXbWa/x2rTSEXlkvgk9X/PxOptUYsLJkzysTgfDywZwuIXLm9B3oNmv3bVgPXsgDsvDfaHYCgz0nHK6NSrX2AeX3yO/dFuoZsuk+J+UyRigMqYj0wjmxUlqj183hinAoGBAMYMOBgF77OXRII7GAuEut/nBeh2sBrgyzR7FmJMs5kvRh6Ck8wp3ysgMvX4lxh1ep8iCw1R2cguqNATr1klOdsCTOE9RrhuvOp3JrYzuIAK6MpH/uBICy4w1rW2+gQySsHcH40r+tNaTFQ7dQ1tef//iy/IW8v8i0t+csztE1JnAoGABdtWYt8FOYP688+jUmdjWWSvVcq0NjYeMfaGTOX/DsNTL2HyXhW/Uq4nNnBDNmAz2CjMbZwt0y+5ICkj+2REVQVUinAEinTcAe5+LKXNPx4sbX3hcrJUbk0m+rSu4G0B/f5cyXBsi9wFCAzDdHgBduCepxSr04Sc9Hde1uQQi7kCgYB0U20HP0Vh+TG2RLuE2HtjVDD2L/CUeQEiXEHzjxXWnhvTg+MIAnggvpLwQwmMxkQ2ACr5sd/3YuCpB0bxV5o594nsqq9FWVYBaecFEjAGlWHSnqMoXWijwu/6X/VOTbP3VjH6G6ECT4GR4DKKpokIQrMgZ9DzaezvdOA9WesFdQKBgQCWfeOQTitRJ0NZACFUn3Fs3Rvgc9eN9YSWj4RtqkmGPMPvguWo+SKhlk3IbYjrRBc5WVOdoX8JXb2/+nAGhPCuUZckWVmZe5pMSr4EkNQdYeY8kOXGSjoTOUH34ZdKeS+e399BkBWIiXUejX/Srln0H4KoHnTWgxwNpTsBCgXu8Q==";
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
    }
}