// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayTest.cs" company="">
//   
// </copyright>
// <summary>
//   数组扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Taf.Test;
using Xunit;
using System.Collections.Generic;
using System.Linq;

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
            Assert.Equal(u1.Name, list.Min(u => u.Name)); // User需要继承IComparable<User>接口
            Assert.Equal(u2.Name, list.Max(u => u.Name));
            Assert.Equal(u2.Email, list.Min(u => u.Email));
            Assert.Equal(u1.Email, list.Max(u => u.Email));

            Assert.Equal(u1.Name, list.MinBy(u => u.Name).Name); // User不需要继承IComparable接口即可实现
            Assert.Equal(u2.Name, list.MaxBy(u => u.Name).Name);
            Assert.Equal(u2.Email, list.MinBy(u => u.Email).Email);
            Assert.Equal(u1.Email, list.MaxBy(u => u.Email).Email);

            var list2 = new List<int> { 1, 2, 3 };
            Assert.Equal(1, list2.Min());
            Assert.Equal(3, list2.Max());
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
    }

}