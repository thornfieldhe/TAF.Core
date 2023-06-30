// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomsTest.cs" company="">
//   
// </copyright>
// <summary>
//   The randoms test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Taf.Core.Test
{
    /// <summary>
    /// The randoms test.
    /// </summary>
    
    [SuppressMessage("Assertions", "xUnit2002", MessageId = "Do not use null check on value type")]
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
            var a  = new[] { 1, 2, 3, 4, 5, 6 ,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
            var m1 = a[1];
            a.GetRandomArray();
            var m2=a[1];
            Assert.True(m1 != m2);
            
            var b  = new List<int> { 1, 2, 3, 4, 5, 6 ,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
            var m3 =b[0];  
            b.GetRandom();
            var m4=b[0];
            Assert.True(m3 != m4);
        }
        
        [Fact]
        public void TestArrary(){
            var a=new []{"1","2","3"};
            a=a.Append("4");
            Assert.Equal(4,a.Length);
            Assert.Equal("4",a[3]);
        }

        /// <summary>
        /// The test randoms generate check code num.
        /// </summary>
        [Fact]
        public void TestRandomsGenerateCheckCodeNum()
        {
            var a = Randoms.GenerateCheckCodeNum(5);
            Assert.NotNull(a);
            var b = Randoms.GenerateCheckCode(5);
            Assert.NotNull(b);
            var c = Randoms.GetRandomCode(5);
            Assert.NotNull(c);
            var d = Randoms.GenerateChinese(10);
            Assert.NotNull(d);
            var e = Randoms.GenerateLetters(10);
            Assert.NotNull(e);
        }

        /// <summary>
        /// The test rnd.
        /// </summary>
        [Fact]
        public void TestRnd()
        {
            var a = Randoms.GetDateRnd();
            Assert.NotNull(a);
            var b = Randoms.GetRndKey();
            Assert.NotNull(b);
            var c = Randoms.GetRndNum(5, true);
            Assert.NotNull(c);
            var d = Randoms.GetRndNum(5);
            Assert.NotNull(d);
            var f = Randoms.GenerateBool();
            Assert.NotNull(f);
            var g = Randoms.GenerateEnum<DayOfWeek>();
            Assert.NotNull(g);
            var h = Randoms.GenerateChinese(15);
            Assert.NotNull(h);
        }

        /// <summary>
        /// The test get rnd next.
        /// </summary>
        [Fact]
        public void TestGetRndNext()
        {
            var a = Randoms.GetRndNext(10, 27);
            var b = new[] { 10,11, 12, 13, 14,15,16,17,18,19,20,21,22,23,24,25,26,27 };
            Assert.True(a.In(b));
        }
    }
}