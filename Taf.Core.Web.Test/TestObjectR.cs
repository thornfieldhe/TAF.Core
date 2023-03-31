using Taf.Core.Utility;

namespace Taf.Core.Web.Test;

public class TestObjectR{
    [Fact]
    public void TestSuccess(){
        Assert.Equal(200,HttpObjectResult.Success().Code);
        var r3 = HttpObjectResult.GetResult<string>(data: "abc");
        Assert.Equal("abc",r3.Data);
        Assert.Equal(200,r3.Code);
        Assert.Equal(true,r3.Message.IsNullOrEmpty());
    }
}
