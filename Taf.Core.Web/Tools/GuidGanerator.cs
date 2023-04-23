// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidGanerator.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MassTransit;
using MassTransit.NewIdProviders;
using System.Collections.Concurrent;

// 何翔华
// Taf.Core.Extension
// GuidGanerator.cs

namespace Taf.Core.Web;

/// <summary>
/// Guid生成器
/// </summary>
public static  class GuidGanerator {
    private static readonly ConcurrentQueue<Guid> _queue  = new ();
    

    /// <summary>
    /// 生成500条Guid
    /// </summary>
    /// <param name="count"></param>
    public static void Ganerate(int count = 500){
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
        for(var i = 0; i < count; i++){
            _queue.Enqueue(NewId.NextGuid());
        }
    }

    /// <summary>
    /// 从队列中推送1条已生成成功的Guid
    /// 如果队列条数为0,则生成500条Guid到队列
    /// </summary>
    /// <returns></returns>
    public static Guid NextGuid(){
        Fx.If(_queue.Count == 0).Then(() => Ganerate());
        if(_queue.TryDequeue(out var tmp)){
            tmp = NewId.NextGuid();
        }

        return tmp;
    }

    /// <summary>
    /// 从队列中推送多条Guid
    /// </summary>
    /// <remarks>
    /// 当请求数量>队列中剩余条数,则生成申请数量的Guid
    ///推送后原队列数量不变
    /// </remarks>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<Guid> NextGuid(int count){
        if(_queue.Count <= count){
            Ganerate(count);
        }
        return YieldReturnGuid(count);
    }

    private static IEnumerable<Guid> YieldReturnGuid(int count){
        for(var i = 0; i < count; i++){
            if(_queue.TryDequeue(out var tmp)){
                yield return tmp;
            }

            yield return NextGuid();
        } 
    }
    
    
    public static int Count => _queue.Count;
}

