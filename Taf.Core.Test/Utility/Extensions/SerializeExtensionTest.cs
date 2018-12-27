

using TAF.Core.Utility;
using TAF.Test;
using Xunit;

namespace TAF.Core.Test
{
    /// <summary>
    /// ��֤������չ
    /// </summary>
    
    public class SerializeTest
    {
        /// <summary>
        /// soap���л�
        /// </summary>
        [Fact]
        public void TestSoapSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.SerializeObjectToString();
            Assert.Equal(serialize.DeserializeStringToObject<User>().Name, user.Name);
        }

        /// <summary>
        /// xml���л�
        /// �̳л���BaseEntity��ҵ���಻����xml���л���
        /// ֻ��ʹ���ֽ����л�
        /// </summary>
        [Fact]
        public void TestXMlSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.XMLSerializer();
            Assert.Equal(serialize.XMLDeserializeFromString<User>().Name, user.Name);
        }

    }
}
