using Taf.Core.Utility;

namespace Taf.Core.Web.Test;

public class TestObjectR{
    [Fact]
    public void TestSuccess(){
        Assert.Equal(200,HttpObjectResult.Success().Code);
        Assert.Equal(404,HttpObjectResult.NotFound().Code);
        
        var r1 = HttpObjectResult.Unauthorized("813697");
        Assert.Equal(401,r1.Code);
        Assert.Equal("Unauthorized error,error code:813697",r1.Message);
        var r2 = HttpObjectResult.InternalServerError("1235845",traceId:"123456");
        Assert.Equal("123456",r2.TraceId); 
        Assert.Equal("Internal server error. error code:1235845",r2.Message); 
        Assert.Equal(500,r2.Code);

        var r3 = HttpObjectResult.GetResult<string>(data: "abc");
        Assert.Equal("abc",r3.Data);
        Assert.Equal(200,r3.Code);
        Assert.Equal(true,r3.Message.IsNullOrEmpty());
    }
}
