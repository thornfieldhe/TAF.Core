// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixGraph.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   邻接矩阵实现的图（默认按照有向图，弧不带权重）
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// thornfield
// EfV4.Application
// MatrixGraph.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
/// 邻接矩阵实现的图（默认按照有向图，弧不带权重）
/// </summary>
[Serializable]
public class MatrixGraph<T>{
    //最大顶点数量
    public int MaxVertexNum;
    //存储顶点的数组
    public T[] Vertex;
    //邻接矩阵
    public int[,] AdjacecntMatrix;
    //顶点数量
    public int Count = 0;
    //遍历时该节点是否访问过
    private bool[]             _visited;
    public  Dictionary<T, int> Index;

    /// <summary>
    /// 自定义大小的矩阵,默认10
    /// </summary>
    /// <param name="maxSize"></param>
    public MatrixGraph(int maxSize){
        MaxVertexNum    = maxSize;
        Vertex          = new T[MaxVertexNum];
        AdjacecntMatrix = new int[MaxVertexNum, MaxVertexNum];
        Index           = new Dictionary<T, int>();
    }

    public MatrixGraph() : this(10){}

    /// <summary>
    /// 增加顶点
    /// </summary>
    public void AddVertex(T vertex){
        //自动扩容,如果容量达到上限,自动扩容1倍,若果总条数超过500条,一次性增加100条
        if(Count >= MaxVertexNum){
            var old = MaxVertexNum;
            if (MaxVertexNum<=500){
                MaxVertexNum *= 2;
            }else{
                MaxVertexNum += 100;
            } 
            var arrary = new T[MaxVertexNum];
            Array.Copy(Vertex, 0,arrary,0, old);
            Vertex          = arrary;
            AdjacecntMatrix = Expanding2DimensionalArrary(AdjacecntMatrix,new int[MaxVertexNum,MaxVertexNum]);
          
        }

        if(Index.ContainsKey(vertex)){
            throw new Exception($"重复添加定点:{vertex}");
        }

        Index[vertex]   = Count;
        Vertex[Count++] = vertex;
    }
    
    private int[,] Expanding2DimensionalArrary(int[,] source, int[,] target){
        for(var i = 0; i < source.GetLength(0); i++){
            for(var j = 0; j < source.GetLength(1); j++){
                target[i, j] = source[i, j];
            }
        }

        return target;
    }

    /// <summary>
    /// 按照顶点的索引添加边
    /// </summary>
    /// <param name="from">出度顶点</param>
    /// <param name="to">入度顶点</param>
    /// <param name="isMutual">该边是否是双向的</param>
    public void AddEdge(T from, T to, bool isMutual = false){
        Index.TryGetValue(from, out var m);
        Index.TryGetValue(to, out var n);
        //判断顶点是否存在
        if(Vertex[m] == null
        || Vertex[n] == null){
            throw (new IndexOutOfRangeException("输入的边对应的顶点不存在！"));
        }

        AdjacecntMatrix[m, n] = 1;
        if(isMutual){
            AdjacecntMatrix[n, m] = 1;
        }
    }

    /// <summary>
    /// 图是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() => Count == 0;

    /// <summary>
    /// 清空图
    /// </summary>
    public void Clear(){
        for(var i = 0; i < Count; i++){
            Vertex[i] = default;
        }

        for(var i = 0; i < Count; i++){
            for(var j = 0; j < Count; i++){
                AdjacecntMatrix[i, j] = 0;
            }
        }
    }

    /// <summary>
    /// 深度优先遍历
    /// </summary>
    /// <param name="myAct">遍历时对每个元素的处理函数</param>
    /// <param name="item">当前顶点</param>
    /// <param name="firstIn">是否为第一次递归，影响到辅助数组的初始化</param>
    public void Dfs(Action<T> myAct, T item, bool firstIn = false){
        //初始化辅助数组
        if(firstIn){
            _visited = new bool[Count];
        }

        if(Index.TryGetValue(item, out var idx)){
            _visited[idx] = true;
            myAct(Vertex[idx]);
            for(var i = 0; i < Count; i++){
                if(AdjacecntMatrix[idx, i] != 0
                && !_visited[i]){
                    Dfs(myAct, Vertex[i]);
                }
            }
        }
    }

    /// <summary>
    /// 广度优先遍历
    /// </summary>
    public void Bfs(Action<T> myAct, T item){
        //类似层次遍历，所以用队列
        var myQueue = new Queue<int>();
        if(Index.TryGetValue(item, out var idx)){
            myQueue.Enqueue(idx);
            //初始化辅助数组
            _visited      = new bool[Count];
            _visited[idx] = true;

            while(myQueue.Count != 0){
                var curIdx = myQueue.Dequeue();
                myAct(Vertex[curIdx]);
                //邻接点入队
                for(var i = 0; i < Count; i++){
                    if(AdjacecntMatrix[curIdx, i] != 0
                    && !_visited[i]){
                        myQueue.Enqueue(i);
                        _visited[i] = true;
                    }
                }
            }
        }
    }
}
