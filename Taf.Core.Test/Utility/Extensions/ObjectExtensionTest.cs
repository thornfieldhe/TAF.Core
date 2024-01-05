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
    
    /// <summary>
    /// 测试克隆
    /// </summary>
    [Fact]
    public void TestClone(){
        var user  = new User(){ Email = "1@a.com", Name = "hi" };
        var user2 = user.Clone();
        user.Email = "2@a.cn";
        Assert.Equal(user2.Name,user.Name);
        Assert.NotEqual(user2.Email,user.Email);
    }
}
