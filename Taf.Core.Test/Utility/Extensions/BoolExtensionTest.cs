// 何翔华
// Taf.Core.Test
// BoolExtensionTest.cs

namespace Taf.Core.Test;

public class BoolExtensionTest{
    /// <summary>
    /// 判断是否为真并执行操作
    /// </summary>
    [Fact]
    public void TestTrueAction(){
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
    public void TestTrueDefault(){
        var a = "";
        a = true.WhenTrue<string>("ccc");
        Assert.Equal("ccc", a);
        false.WhenFalse(() => a = a + "__");
        Assert.Equal("ccc__", a);
    }
}
