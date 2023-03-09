// 何翔华
// Taf.Core.Test
// ObjectExtensionTest.cs

using Taf.Test;

namespace Taf.Core.Test;

public class ObjectExtensionTest{
   
    /// <summary>
    /// 类型判断
    /// </summary>
    [Fact]
    public void TestIsAs(){
        var user = new User(){ Name = "user" };
        Assert.NotNull(user.As<IUser>().Name);
        Assert.True(user.Is<IUser>());
    }

    /// <summary>
    /// 安全赋值
    /// </summary>
    [Fact]
    public void TestSet(){
        User user = new User();
        user.SafeValue().Set(u => u.Name = "xxx");
        Assert.Equal("xxx", user.Name.NullOr( "abc"));
        user.Name=null;
        Assert.Equal("abc", user.Name.NullOr( "abc"));
    } 
    
    /// <summary>
    /// 判断是否为空并执行操作
    /// </summary>
    [Fact]
    public void TestNullableAction(){
        string a = null;
        a.IfNull(() => a = "ccc");
        Assert.Equal("ccc", a);
        a.IfNotNull(r => a = r + "__");
        Assert.Equal("ccc__", a);
    }
}
