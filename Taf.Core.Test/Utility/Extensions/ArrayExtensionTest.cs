// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayTest.cs" company="">
//   
// </copyright>
// <summary>
//   数组扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Taf.Test;
using  static Taf.Core.Utility.StaticMethods;

namespace Taf.Core.Test
{
 
    /// <summary>
    /// The array test.
    /// </summary>
    
    public class ArrayTest
    {
        /// <summary>
        /// 转换为用分隔符拼接的字符串
        /// </summary>
        [Fact]
        public void TestSplice()
        {
            Assert.Equal("1,2,3", new List<int> { 1, 2, 3 }.Splice());
            Assert.Equal("'1','2','3'", new List<int> { 1, 2, 3 }.Splice("'"));
            Assert.Equal("1,2,3", new List<int> { 1, 2, 3 }.ToString(","));
            Assert.Equal("2pp,3pp,4pp", new List<int> { 1, 2, 3 }.ToString(r => ((++r) + "pp").ToString(), ","));
        }

        /// <summary>
        /// The test_ foreach.
        /// </summary>
        [Fact]
        public void Test_Foreach()
        {
            var data = new List<string> { "A", "B", "C", "D" };
            var count = 0;
            data.ForEach(item =>
            {
                count++;
            });
            Assert.Equal(count, 4);
        }

        /// <summary>
        /// The test_ random.
        /// </summary>
        [Fact]
        public void Test_Random()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.True(list.Contains(list.Random()));
        }

        /// <summary>
        /// The test_ contains.
        /// </summary>
        [Fact]
        public void Test_Contains()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.True(list.Random().In(list));
            Assert.True(5.NotIn(list));
        }

        /// <summary>
        /// The test_ is empty.
        /// </summary>
        [Fact]
        public void Test_IsEmpty()
        {
            var list = new List<int>();
            Assert.True(list.IsNullOrEmpty());
            list.Add(2);
            Assert.False(list.IsNullOrEmpty());
            list = null;
            Assert.True(list.IsNullOrEmpty());
        }

        /// <summary>
        /// The test_ max or min value.
        /// </summary>
        [Fact]
        public void Test_MaxOrMinValue()
        {
            var u1 = new User { Name = "AB", Email = "234" };
            var u2 = new User { Name = "BC", Email = "123" };
            var list = new List<User> { u1, u2 };
            Assert.Equal(u1.Name, Extensions.MinBy(list, u => u.Name).Name); // User不需要继承IComparable接口即可实现
            Assert.Equal(u2.Name, Extensions.MaxBy(list, u => u.Name).Name);
            Assert.Equal(u2.Email, Extensions.MinBy(list, u => u.Email).Email);
            Assert.Equal(u1.Email, Extensions.MaxBy(list, u => u.Email).Email);
        }

        /// <summary>
        /// The test_ shuffle.
        /// </summary>
        [Fact]
        public void Test_Shuffle()
        {
            var list = new List<int> { 1, 2, 3 };
            var newList = list.Shuffle().ToList();
            Assert.False((newList[0] == list[0]).And(newList[1] == list[1]).And(newList[2] == list[2]));
        }

        /// <summary>
        /// The test_ reversal.
        /// </summary>
        [Fact]
        public void Test_Reversal()
        {
            var list = new[] { 1, 2, 3 };
            list.Reversal();
            Assert.Equal(list[0], 3);
        }

        /// <summary>
        /// The test_ swap.
        /// </summary>
        [Fact]
        public void Test_Swap()
        {
            var list = new[] { 1, 2, 3 };
            list.Swap(0, 2);
            Assert.Equal(list[0], 3);
        }
        
        /// <summary>
        /// 测试将不定长对象数组转换成数组的方法
        /// 需要引入using static Taf.Core.Utility.Extensions;
        /// </summary>
        [Fact]
        public void Test_Arr(){
            var list = Arr(1, 2, 3, 4).ToArray();
            Assert.Equal(list.Length, 4);
        }
    }

}