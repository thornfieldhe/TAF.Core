

using Taf.Core.Utility;
using Taf.Test;
using Xunit;

namespace Taf.Core.Test
{
    /// <summary>
    /// 验证特性扩展
    /// </summary>
    
    public class SerializeTest
    {
        /// <summary>
        /// soap序列化
        /// </summary>
        [Fact]
        public void TestSoapSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.SerializeObjectToString();
            Assert.Equal(serialize.DeserializeStringToObject<User>().Name, user.Name);
        }

        /// <summary>
        /// xml序列化
        /// 继承基类BaseEntity的业务类不适用xml序列化，
        /// 只能使用字节序列化
        /// </summary>
        [Fact]
        public void TestXMlSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.XmlSerializer();
            Assert.Equal(serialize.XmlDeserializeFromString<User>().Name, user.Name);
        }

    }
}
