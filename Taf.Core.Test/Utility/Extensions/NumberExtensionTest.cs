using TAF.Core.Utility;
using TAF.Test;
using Xunit;

namespace TAF.Core.Test
{
    
    public class NumberExtensionTest
    {
        /// <summary>
        /// 截断小数位数
        /// </summary>
        [Fact]

        public void TestFixed()
        {
            Assert.Equal(2.23, (2.2358).ToFixed(2));
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        [Fact]

        public void TestRound()
        {
            Assert.Equal(2.24, (2.2358).Round(2));
        }

        /// <summary>
        /// 是否在区间内
        /// </summary>
        [Fact]

        public void TestBetween()
        {
            Assert.True((2.2358).IsBetween(3.0, 2.0));
        }

        /// <summary>
        /// 判断是否为空并执行操作
        /// </summary>
        [Fact]
        public void TestNullableAction()
        {
            string a = null;
            a.IfNull(() => a = "ccc");
            Assert.Equal("ccc", a);
            a.IfNotNull(r => a = r + "__");
            Assert.Equal("ccc__", a);

        }

        /// <summary>
        /// 判断是否为真并执行操作
        /// </summary>
        [Fact]
        public void TestTrueAction()
        {
            var a = "";
            true.IfTrue(() => a = "ccc");
            Assert.Equal("ccc", a);
            false.IfFalse(() => a = a + "__");
            Assert.Equal("ccc__", a);

        }


        /// <summary>
        /// 判断是否为真返回默认值
        /// </summary>
        [Fact]
        public void TestTrueDefault()
        {
            var a = "";
            a = true.WhenTrue<string>("ccc");
            Assert.Equal("ccc", a);
            false.WhenFalse(() => a = a + "__");
            Assert.Equal("ccc__", a);

        }


        /// <summary>
        /// 类型判断
        /// </summary>
        [Fact]
        public void TestIsAs()
        {
            var user = new User() { Name = "user" };
            Assert.NotNull(user.As<IUser>().Name);
            Assert.True(user.Is<IUser>());
        }

        /// <summary>
        /// 安全赋值
        /// </summary>
        [Fact]
        public void TestSet()
        {
            User user = null;
            user.SafeValue().Set(u => u.Name = "xxx");
            user = null;
            Assert.Equal(user.NullOr(u => u.Name), null);
        }

        /// <summary>
        /// 移除decimal尾随0
        /// </summary>
        [Fact]
        public void TestRemoveEnd0()
        {
            Assert.Equal("0.12", (0.12M).RemoveEnd0());
            Assert.Equal("0.12", (.12M).RemoveEnd0());
            Assert.Equal("12", (12M).RemoveEnd0());
            Assert.Equal("1200", (1200M).RemoveEnd0());
            Assert.Equal("120.01", (120.01M).RemoveEnd0());
            Assert.Equal("12", (12.00M).RemoveEnd0());
            Assert.Equal("12.00010001", (012.0001000100M).RemoveEnd0());
        }
    }
}
