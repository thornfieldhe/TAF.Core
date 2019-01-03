// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomsTest.cs" company="">
//   
// </copyright>
// <summary>
//   The randoms test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using TAF.Utility;
using Xunit;

namespace TAF.Core.Test
{
    using System;
    
    using TAF.Core.Utility;

    /// <summary>
    /// The randoms test.
    /// </summary>
    
    public class RandomsTest
    {
        /// <summary>
        /// The test randoms int.
        /// </summary>
        [Fact]
        public void TestRandomsInt()
        {
            var a = Randoms.GetRandomInt(10, 20);
            Assert.True(a.Between(10, 20));
        }

        /// <summary>
        /// The test randoms double.
        /// </summary>
        [Fact]
        public void TestRandomsDouble()
        {
            var a = Randoms.GetRandomDouble();
            Assert.True(a.Between(0.0, 1.0));
        }

        /// <summary>
        /// The test randoms random array.
        /// </summary>
        [Fact]
        public void TestRandomsRandomArray()
        {
            var a = new[] { 1, 2, 3, 4, 5, 6 ,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
            Randoms.GetRandomArray(a);
            var m1=a[0];  
            Randoms.GetRandomArray(a);
            var m2=a[0];
            Assert.True(m1 != m2);
        }

        /// <summary>
        /// The test randoms generate check code num.
        /// </summary>
        [Fact]
        public void TestRandomsGenerateCheckCodeNum()
        {
            var a = Randoms.GenerateCheckCodeNum(5);
            Console.WriteLine(a);
            Assert.NotNull(a);
            var b = Randoms.GenerateCheckCode(5);
            Console.WriteLine(b);
            Assert.NotNull(b);
            var c = Randoms.GetRandomCode(5);
            Console.WriteLine(c);
            Assert.NotNull(c);
            var d = Randoms.GenerateChinese(10);
            Console.WriteLine(d);
            Assert.NotNull(d);
            var e = Randoms.GenerateLetters(10);
            Console.WriteLine(e);
            Assert.NotNull(e);
        }

        /// <summary>
        /// The test rnd.
        /// </summary>
        [Fact]
        public void TestRnd()
        {
            var a = Randoms.GetDateRnd();
            Console.WriteLine(a);
            Assert.NotNull(a);
            var b = Randoms.GetRndKey();
            Console.WriteLine(b);
            Assert.NotNull(b);
            var c = Randoms.GetRndNum(5, true);
            Console.WriteLine(c);
            Assert.NotNull(c);
            var d = Randoms.GetRndNum(5);
            Console.WriteLine(d);
            Assert.NotNull(d);
            var e = Randoms.GenerateDate();
            Console.WriteLine(e);
            Assert.NotNull(e);
            var f = Randoms.GenerateBool();
            Console.WriteLine(f);
            Assert.NotNull(f);
            var g = Randoms.GenerateEnum<DayOfWeek>();
            Console.WriteLine(g.Description());
            Assert.NotNull(g);
            var h = Randoms.GenerateChinese(15);
            Console.WriteLine(h);
            Assert.NotNull(h);
        }

        /// <summary>
        /// The test get rnd next.
        /// </summary>
        [Fact]
        public void TestGetRndNext()
        {
            var a = Randoms.GetRndNext(10, 27);
            var b = new[] { 11, 12, 13, 14,15,16,17,18,19,20,21,22,23,24,25,26 };
            Console.WriteLine(a);
            Assert.True(a.In(b));
        }
    }
}