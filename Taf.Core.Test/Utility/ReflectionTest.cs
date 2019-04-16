using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test
{
    using System;

    /// <summary>
    /// DescriptionTest 的摘要说明
    /// </summary>
    public class ReflectionTest
    {
        [Fact]
        public void TestDescription()
        {
//            Assert.Equal(typeof(TestDesc).ToDescription(),                  "测试名称");
//            Assert.Equal(Reflection.GetMemberDescription<TestDesc>("Name"), "名称");
            Type.GetType("");
        }

        [Fact]
        public void TestGetMembers()
        {
//            Assert.Equal(Reflection.GetMembers<TestDesc>()[0].Item1, "Name");
        }
    }

//    [System.ComponentModel.Description("测试名称")]
//    public class TestDesc : BaseBusiness<TestDesc>
//    {
//        [System.ComponentModel.Description("名称")]
//        public string Name { get; set; }
//    }
}
