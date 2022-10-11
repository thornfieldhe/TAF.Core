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
                                           new(){ Id = Guid.NewGuid(), Name = "m" },
                                           new(){ Id = Guid.NewGuid(), Name = "b" },
                                           new(){ Id = Guid.NewGuid(), Name = "a" },
                                           new(){ Id = Guid.NewGuid(), Name = "w" },
                                           new(){ Id = Guid.NewGuid(), Name = "o" },
                                           new(){ Id = Guid.NewGuid(), Name = "p" },
                                       };

            var comparer = Utility.Comparison<TestInfo>.CreateComparer(m => m.Name);
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
                                           new(){ Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "m" },
                                           new(){ Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "b" },
                                           new(){ Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "a" },
                                           new(){ Id = Guid.NewGuid(), Name                                   = "w" },
                                           new(){ Id = Guid.NewGuid(), Name                                   = "A" },
                                           new(){ Id = Guid.NewGuid(), Name                                   = "a" },
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
                new() {Name = "A"}, new() {Name = "B"}, new() {Name = "C"}
              , new() {Name = "F"}, new() {Name = "H"}
            };

            students.Sort(new CompareWithDefaultSortedArray<TempData,string>(sortedName,x=>x.Name));
            Assert.Equal(students[0].Name,"H");
            Assert.Equal(students[1].Name,"F");
        }
        /// <summary>
        /// 根据已知顺序列表对当前列表排序
        /// 根据已知属性顺序对对象排序
        /// </summary>
        [Fact]
        public void OrderByCollectionTest()
        {
            var sortedName = new string[] {"H", "F", "B","A","C"};
            var students = new List<TempData>()
            {
                new() {Name = "A"}, new() {Name = "B"}, new() {Name = "C"}
              , new() {Name = "F"}, new() {Name = "H"}
            };

            students=students.OrderBy(x=>x.Name,sortedName).ToList();
            Assert.Equal(students[0].Name,"H");
            Assert.Equal(students[1].Name,"F");
        }
        
        /// <summary>
        /// 根据已知顺序列表对当前列表排序
        /// 根据已知属性顺序对对象排序
        /// </summary>
        [Fact]
        public void ThenByCollectionTest()
        {
            var sortedName = new string[] {"A", "H", "C","F","B"};
            var students = new List<TempData>()
            {
                new() {Name = "A",Age = 10}, new() {Name = "B",Age = 14}, new() {Name = "C",Age = 10}
              , new() {Name = "F",Age = 10}, new() {Name = "H",Age = 14}
            };

            students=students.OrderBy(x=>x.Age).ThenBy(x=>x.Name,sortedName).ToList();
            Assert.Equal(students[1].Name,"C");
            Assert.Equal(students[4].Name,"B");
        }
        
        /// <summary>
        /// 针对KevValue对象进行排序
        /// </summary>
        [Fact]
        public void SortByCollectionTest2()
        {
            var sortedString = new [] {"H", "F", "B","A","C"};
            var sortedInt = new [] {4, 8, 3,7,9};
            var students = new List<KeyValue<int,string>>()
            {
                new() {Value = "A",Key = 1}
              , new() {Value = "B",Key = 2}
              , new() {Value = "C",Key = 3}
              , new() {Value = "F",Key = 4}
              , new() {Value = "H",Key = 5}
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
            public int Age { get; set; }
            public int CompareTo(object obj)
            {
                var compare = obj as TempData;
                if (compare ==null)
                {
                    return -1;
                }
                return Name.CompareTo(compare.Name);
            }
        }
    }
}
