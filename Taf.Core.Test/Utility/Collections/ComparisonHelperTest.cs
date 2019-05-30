using System.Collections.Generic;
using Taf.Test;
using Xunit;
using System;
using System.Linq;
using Taf.Core.Utility;

namespace Taf.Core.Test
{



    /// <summary>
    /// 普通对象提供比较判定的扩展
    /// </summary>
    public class ComparisonHelperTest
    {
        [Fact]
        public void CreateComparerTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = Guid.NewGuid(), Name = "m" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "b" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "w" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "o" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "p" },
                                       };

            var comparer = Taf.Core.Utility.Comparison<TestInfo>.CreateComparer(m => m.Name);
            list1.Sort(comparer);
            Assert.Equal( "a",list1[0].Name);
        }

        /// <summary>
        /// 去除重复
        /// </summary>
        [Fact]
        public void DistinctTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "m" },
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "b" },
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "w" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "A" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                       };

            var count = list1.Distinct(r => r.Name).Count();
            Assert.Equal(5, count);
            count = list1.Distinct(r => r.Id).Count();
            Assert.Equal(4, count);
            Assert.Equal("m",list1.Distinct(r => r.Id).First().Name);
        }

        /// <summary>
        /// 根据已知顺序列表对当前列表排序
        /// 根据已知属性顺序对对象排序
        /// </summary>
        [Fact]
        public void SortByCollectionTest()
        {
            var sortedName = new string[] {"H", "F", "B","A","C"};
            var students = new List<TempData>()
            {
                new TempData() {Name = "A"}, new TempData() {Name = "B"}, new TempData() {Name = "C"}
              , new TempData() {Name = "F"}, new TempData() {Name = "H"}
            };

            students.Sort(new CompareWithDefaultSortedArray<TempData,string>(sortedName,x=>x.Name));
            Assert.Equal(students[0].Name,"H");
            Assert.Equal(students[1].Name,"F");
        }
        
        /// <summary>
        /// 针对KevValue对象进行排序
        /// </summary>
        [Fact]
        public void SortByCollectionTest2()
        {
            var sortedString = new string[] {"H", "F", "B","A","C"};
            var sortedInt = new int[] {4, 8, 3,7,9};
            var students = new List<KeyValue<int,string>>()
            {
                new KeyValue<int,string>() {Value = "A",Key = 1}
              , new KeyValue<int,string>() {Value = "B",Key = 2}
              , new KeyValue<int,string>() {Value = "C",Key = 3}
              , new KeyValue<int,string>() {Value = "F",Key = 4}
              , new KeyValue<int,string>() {Value = "H",Key = 5}
            };

            students.Sort(new CompareWithDefaultSortedArray<KeyValue<int,string>,string>(sortedString, x=>x.Value));
            Assert.Equal(students[0].Value, "H");
            Assert.Equal(students[1].Value, "F");
            
            students.Sort(new CompareWithDefaultSortedArray<KeyValue<int,string>,int>(sortedInt, x=>x.Key));
            Assert.Equal(students[0].Key, 4);
            Assert.Equal(students[1].Key, 3);
            Assert.Equal(students[3].Key, 2);
        }

        private class TempData:IComparable
        {
            public string Name { get; set; }
            public int CompareTo(object obj)
            {
                var compare = obj as TempData;
                if (compare ==null)
                {
                    return -1;
                }
                return this.Name.CompareTo(compare.Name);
            }
        }
    }
}
