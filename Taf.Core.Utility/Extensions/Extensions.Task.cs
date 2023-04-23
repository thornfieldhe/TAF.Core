// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_Task.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;

// 何翔华
// Taf.Core.Utility
// Extensions.Task.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
/// $Summary$
/// </summary>
public static partial class Extensions{
    public static TupleTaskAwaiter<T1, T2, T3> GetAwaiter<T1, T2, T3>(this (Task<T1>, Task<T2>, Task<T3>) tasks) =>
        new(tasks);

    public readonly record struct TupleTaskAwaiter<T1, T2, T3> : ICriticalNotifyCompletion{
        private readonly (Task<T1>, Task<T2>, Task<T3>) _tasks;
        private readonly TaskAwaiter                    _whenAllAwaiter;

        public TupleTaskAwaiter((Task<T1>, Task<T2>, Task<T3>) tasks){
            _tasks          = tasks;
            _whenAllAwaiter = Task.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
        }

        public bool IsCompleted => _whenAllAwaiter.IsCompleted;

        public void OnCompleted(Action continuation) => _whenAllAwaiter.OnCompleted(continuation);

        [SecurityCritical]
        public void UnsafeOnCompleted(Action continuation) => _whenAllAwaiter.UnsafeOnCompleted(continuation);

        public (T1, T2, T3) GetResult(){
            _whenAllAwaiter.GetResult();
            return (_tasks.Item1.Result, _tasks.Item2.Result, _tasks.Item3.Result);
        }
    }
}
/// <summary>
/// 同时等待4个任务扩展类
/// </summary>
public static class TupleTaskAwaiterWith4Methords{
    public static TupleTaskAwaiter<T1, T2, T3,T4> GetAwaiter<T1, T2, T3,T4>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>) tasks) =>
        new(tasks);

    public readonly record struct TupleTaskAwaiter<T1, T2, T3, T4> : ICriticalNotifyCompletion{
        private readonly (Task<T1>, Task<T2>, Task<T3>, Task<T4>) _tasks;
        private readonly TaskAwaiter                    _whenAllAwaiter;

        public TupleTaskAwaiter((Task<T1>, Task<T2>, Task<T3>, Task<T4>) tasks){
            _tasks          = tasks;
            _whenAllAwaiter = Task.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4).GetAwaiter();
        }

        public bool IsCompleted => _whenAllAwaiter.IsCompleted;

        public void OnCompleted(Action continuation) => _whenAllAwaiter.OnCompleted(continuation);

        [SecurityCritical]
        public void UnsafeOnCompleted(Action continuation) => _whenAllAwaiter.UnsafeOnCompleted(continuation);

        public (T1, T2, T3, T4) GetResult(){
            _whenAllAwaiter.GetResult();
            return (_tasks.Item1.Result, _tasks.Item2.Result, _tasks.Item3.Result, _tasks.Item4.Result);
        }
    }
}
