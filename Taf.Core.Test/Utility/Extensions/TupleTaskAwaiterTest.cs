// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TupleTaskAwaiterTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Taf.Core.Extension;

// 何翔华
// Taf.Core.Extension.Test
// TupleTaskAwaiterTest.cs

namespace Taf.Core.Extension.Test;

/// <summary>
/// 测试同时等待多个任务的方法
/// </summary>
public class TupleTaskAwaiterTest{
    [Fact]
    public async Task TestAwait_2_Task(){
        var st = new Stopwatch();
        st.Start();
        var (m1, m2) =  (Task1(), Task2());
        var second = st.Elapsed.TotalSeconds;
        Assert.Equal(4, (int)second);
        
        st.Restart();
        var (m11, m12, m13) = await (Task1(), Task2(), Task3());
        second              = st.Elapsed.TotalSeconds;
        Assert.Equal(6, (int)second);
        
        st.Restart();
        var (m21, m22, m23,m24) = await (Task1(), Task2(), Task3(),Task4());
        second              = st.Elapsed.TotalSeconds;
        Assert.Equal(8, (int)second);
        st.Stop();
    }

    private Task<string> Task1() =>
        Task.Run(() => {
            Thread.Sleep(2000);
            return "Foo1";
        });

    private Task<string> Task2() =>
        Task.Run(() => {
            Thread.Sleep(4000);
            return "Foo2";
        });


    private Task<string> Task3() =>
        Task.Run(() => {
            Thread.Sleep(6000);
            return "Foo3";
        });
    
    private Task<string> Task4() =>
        Task.Run(() => {
            Thread.Sleep(8000);
            return "Foo4";
        });
}
