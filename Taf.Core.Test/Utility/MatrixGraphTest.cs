// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixGraphTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   邻接矩阵测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// thornfield
// Taf.Core.Test
// MatrixGraphTest.cs

namespace Taf.Core.Test;

using System;

/// <summary>
/// 邻接矩阵测试
/// </summary>

public class MatrixGraphTest{
    [Fact]
    public void TestMatrixGrap(){
        var myGraph = new MatrixGraph<string>(7);
        myGraph.AddVertex("A0");
        myGraph.AddVertex("A1");
        myGraph.AddVertex("A2");
        myGraph.AddVertex("A3");
        myGraph.AddVertex("A4");
        myGraph.AddVertex("A5");
        myGraph.AddVertex("A6");

        myGraph.AddEdge("A0","A1");
        myGraph.AddEdge("A0","A2");
        myGraph.AddEdge("A0","A3");
        myGraph.AddEdge("A1","A6");
        myGraph.AddEdge("A2","A4");
        myGraph.AddEdge("A4","A3");
        myGraph.AddEdge("A3","A5");

        var list = new List<string>();
        myGraph.Dfs((a)=> {
            list.Add(a);
        },"A1",true);
        
        Assert.True(list.Contains("A6"));//A1的上游只包含自身和A6
        Assert.False(list.Contains("A0"));
        Assert.True(list.Contains("A1")); //A1的上游只包含自身和A6
        Assert.False(list.Contains("A2"));
        Assert.False(list.Contains("A3"));
        Assert.False(list.Contains("A4"));
        Assert.False(list.Contains("A5"));
        myGraph.Dfs((a)=> {
            list.Add(a);
        },"A0");
        Assert.True(list.Contains("A6")); //A0的上游包含所有
        Assert.True(list.Contains("A0"));
        Assert.True(list.Contains("A1"));
        Assert.True(list.Contains("A2"));
        Assert.True(list.Contains("A3"));
        Assert.True(list.Contains("A4"));
        Assert.True(list.Contains("A5"));
    }
}
