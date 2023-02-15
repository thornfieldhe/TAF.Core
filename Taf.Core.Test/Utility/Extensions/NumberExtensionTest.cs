namespace Taf.Core.Test;

public class NumberExtensionTest{
    /// <summary>
    /// 截断小数位数
    /// </summary>
    [Fact]
    public void TestFixed(){
        Assert.Equal(2.23, (2.2358).ToFixed(2));
    }

    /// <summary>
    /// 四舍五入
    /// </summary>
    [Fact]
    public void TestRound(){
        Assert.Equal(2.24, (2.2358).Round(2));
    }

    /// <summary>
    /// 是否在区间内
    /// </summary>
    [Fact]
    public void TestBetween(){
        Assert.True((2.2358).IsBetween(3.0, 2.0));
    }

    /// <summary>
    /// 移除decimal尾随0
    /// </summary>
    [Fact]
    public void TestRemoveEnd0(){
        Assert.Equal("0.12", (0.12M).RemoveEnd0());
        Assert.Equal("0.12", (.12M).RemoveEnd0());
        Assert.Equal("12", (12M).RemoveEnd0());
        Assert.Equal("1200", (1200M).RemoveEnd0());
        Assert.Equal("120.01", (120.01M).RemoveEnd0());
        Assert.Equal("12", (12.00M).RemoveEnd0());
        Assert.Equal("12.00010001", (012.0001000100M).RemoveEnd0());
    }
    
    /// <summary>
    /// 测试小数转百分比
    /// </summary>
    [Fact]
    public void TestFormatPercent(){
        Assert.Equal("12.00%", (0.12M).FormatPercent()); 
        Assert.Equal("12.1%", (0.1213M).FormatPercent(1)); 
    }
    
    /// <summary>
    /// 测试小数转百分比
    /// </summary>
    [Fact]
    public void TestFormatScience(){
        Assert.Equal("12.13%", 0.1213M.FormatPercent()); 
        Assert.Equal("12.1%", 0.1213M.FormatPercent(1)); 
        
        Assert.Equal("0.12", 0.12.FormatScience());  
        Assert.Equal("1.213", 1.213.FormatScience());  
        Assert.Equal("1.21", 1.21.FormatScience());  
        Assert.Equal("1.214", 1.2135.FormatScience());  
        Assert.Equal("0.121", 0.1213.FormatScience());  
        Assert.Equal("0.121", 0.121.FormatScience());  
        Assert.Equal("0.012", 0.01213.FormatScience());  
        Assert.Equal("0.001", 0.001213.FormatScience());  
        Assert.Equal("1.213E-004", 0.0001213.FormatScience());
        Assert.Equal("12.13", 12.13.FormatScience()); 
        Assert.Equal("12.135", 12.135.FormatScience()); 
        Assert.Equal("121.35", 121.35.FormatScience()); 
        Assert.Equal("1213.5", 1213.5.FormatScience()); 
        Assert.Equal("1.214E+005", 121350D.FormatScience()); 

    }
}