using Taf.Core.Web;

namespace Taf.Core.Extension.Test;

public class GuidTest{
    [Fact]
    public void Test_Guid_Ganerator(){
       GuidGanerator.NextGuid();
        Assert.Equal(GuidGanerator.Count,499 );
        GuidGanerator.NextGuid(600);
        Assert.Equal(GuidGanerator.Count,499 );
    }
}