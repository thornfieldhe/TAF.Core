// 何翔华
// Taf.Core.Extension.Test
// MlStringTest.cs

using System.Globalization;

namespace Taf.Core.Extension.Test;

public class MlStringTest{
    
    [Fact]
    public void Test_MLString(){
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
        var st = new MlString( "zhangshan","张三",Thread.CurrentThread.Name);
        Assert.Equal(st.Current,"张三" ); 
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        Assert.Equal(st.Current,"zhangshan" ); 
    }
}
