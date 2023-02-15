namespace Taf.Core.Extension.Test;

public class GuidTest{
    [Fact]
    public void Test_Guid_Ganerator(){
       GuidGanerator.Instance.NextGuid();
        Assert.Equal(GuidGanerator.Instance.Count,499 );
        GuidGanerator.Instance.NextGuid(600);
        Assert.Equal(GuidGanerator.Instance.Count,499 );
    }
}