using Taf.Core.Utility;

namespace Taf.Core.Test;

public class NumExtensionTest{
    /// <summary>
    /// 截断小数位数
    /// </summary>
    [Fact]
    public void TestFixed(){
        Assert.Equal(2.23, (2.2358).ToFixed(2));
        Assert.Equal(2.23M, (2.2358M).ToFixed(2));
    }

    /// <summary>
    /// 四舍五入
    /// </summary>
    [Fact]
    public void TestRound(){
        Assert.Equal(2.24, (2.235).Round(2));
        Assert.Equal(2.24M, (2.235M).Round(2));
        Assert.Equal(2.23, (2.232).Round(2));
        Assert.Equal(2.23M, (2.232M).Round(2));
    }

    /// <summary>
    /// 是否在区间内
    /// </summary>
    [Fact]
    public void TestBetween(){
        Assert.True((2.2358).IsBetween(3.0, 2.0));
        Assert.True((2.2358M).IsBetween(3.0M, 2.0M));
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
        Assert.Equal("12.00%", (0.12M).As<IDecimalFormat>().FormatPercent()); 
        Assert.Equal("12.1%", (0.1213M).As<IDecimalFormat>().FormatPercent(1)); 
    }
    
    /// <summary>
    /// 测试小数转百分比
    /// </summary>
    [Fact]
    public void TestEqual(){
        Assert.True(12.351111.EqualsEx(12.35642,2)); 
    }
    
    
    
    /// <summary>
    /// 测试小数转百分比
    /// </summary>
    [Fact]
    public void TestFormatScience(){
        Assert.Equal("12.13%", 0.1213M.As<IDecimalFormat>().FormatPercent()); 
        Assert.Equal("12.1%", 0.1213M.As<IDecimalFormat>().FormatPercent(1)); 
        
        Assert.Equal("0.12", 0.12.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("1.213", 1.213.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("1.21", 1.21.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("1.214", 1.2135.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("0.121", 0.1213.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("0.121", 0.121.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("0.012", 0.01213.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("0.001", 0.001213.As<IDoubleFormat>().FormatScience());  
        Assert.Equal("1.213E-004", 0.0001213.As<IDoubleFormat>().FormatScience());
        Assert.Equal("12.13", 12.13.As<IDoubleFormat>().FormatScience()); 
        Assert.Equal("12.135", 12.135.As<IDoubleFormat>().FormatScience()); 
        Assert.Equal("121.35", 121.35.As<IDoubleFormat>().FormatScience()); 
        Assert.Equal("1213.5", 1213.5.As<IDoubleFormat>().FormatScience()); 
        Assert.Equal("1.214E+005", 121350D.As<IDoubleFormat>().FormatScience()); 

    }
}