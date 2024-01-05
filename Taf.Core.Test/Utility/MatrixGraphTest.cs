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
        var myGraph = GanerateMatrixGrap();

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
    
    /// <summary>
    /// 移出中间项目,并移出其上游所有子项
    /// </summary>
    [Fact]
    public void TestRemoveItem(){
        var myGraph = GanerateMatrixGrap();
       Assert.True(myGraph.HasChildren("A2")); 
       Assert.False(myGraph.HasChildren("A4")); 
        myGraph.RemoveVertex("A1");//移出A1节点及A1的上游节点A6
        Assert.Equal(5, myGraph.Count);
        
        Assert.Null(myGraph.Vertex[1]);
        Assert.Null(myGraph.Vertex[6]);
        
        for(var i = 0; i < 7; i++){
            Assert.Equal(0, myGraph.AdjacecntMatrix[1, i]);
        }
        for(var i = 0; i < 7; i++){
            Assert.Equal(0, myGraph.AdjacecntMatrix[6, i]);
        }

        Assert.False(myGraph.Index.ContainsKey("A1"));
        Assert.False(myGraph.Index.ContainsKey("A6"));
        
        Assert.Equal(2, myGraph.EmptyVertex.Count);
        Assert.Equal(1, myGraph.EmptyVertex[0]);
        Assert.Equal(6, myGraph.EmptyVertex[1]);
        
        myGraph.AddVertex("A8");//添加A8节点
        myGraph.AddEdge("A0","A8");
        
        Assert.Equal(6, myGraph.Count);
        
        Assert.NotNull(myGraph.Vertex[1]);
        Assert.Null(myGraph.Vertex[6]);
        
        Assert.Equal(1, myGraph.AdjacecntMatrix[0, 1]);

        Assert.True(myGraph.Index.ContainsKey("A8"));
        Assert.Equal(1, myGraph.Index["A8"]); 
        
        Assert.Equal(1, myGraph.EmptyVertex.Count);
        Assert.Equal(6, myGraph.EmptyVertex[0]);
    }
    
    private MatrixGraph<string> GanerateMatrixGrap(){
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
        myGraph.AddEdge("A3","A5");

        return myGraph;
    }
}
