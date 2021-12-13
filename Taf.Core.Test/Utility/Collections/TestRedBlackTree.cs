// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRedBlackTree.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   红黑树测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Taf.Core.Utility;
using Taf.Utility;
using Xunit;

namespace Taf.Core.Test{
    using System;

    /// <summary>
    /// 红黑树测试
    /// </summary>
    public class TestRedBlackTree{
        [Fact]
        public void TestSpeed(){
            var list1    = new RedBlackTree<int>();
            var list2    = new SortedSet<int>();
            var testlist = new int[100000];
            for(var index = 0; index < testlist.Length; index++){
                testlist[index] = Randoms.GetRandomInt(0, 10000);
            }

            for(var index = 0; index < testlist.Length; index++){
                list1.Add(testlist[index]);
                list2.Add(testlist[index]);
            }


            var watch = new Stopwatch();
            watch.Start();
            var max = testlist.Max();
            Trace.TraceInformation(max.ToString());
            System.Console.WriteLine($"max is:{max}");
            watch.Stop();
            var time1 = watch.ElapsedMilliseconds;
            watch.Restart();
            max = list1.Max;
            Trace.TraceInformation(max.ToString());
            watch.Stop();
            var time2 = watch.ElapsedMilliseconds;
            Assert.True(time1 > time2);
            watch.Restart();
            max = list2.Last();
            System.Console.WriteLine(max);
            watch.Stop();
            var time3 = watch.ElapsedMilliseconds;
            Assert.True(time1 > time3);
            Assert.False(time2 > time3);
        }
    }
}
