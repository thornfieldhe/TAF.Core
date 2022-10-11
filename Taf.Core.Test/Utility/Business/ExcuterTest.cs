// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExcuterTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   测试执行者
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test.Business
{
    using System;

    /// <summary>
    /// Summary
    /// </summary>
    public class ExcuterTest
    {
        [Fact]
        public void Test1()
        {
            var hander1=new ExcutorA();
            var hander2=new ExcutorB();
            var request=new ComparisonObject<People,People>(new People(){Age = 10,Name = "aa"}
               ,new People() {Age = 10,Name = "bb"});
            request.AddExecutor(hander1);
            request.AddExecutor(hander2);
            request.Excute();
            Assert.Equal(request.Source.Age,10);
            Assert.Equal(request.Source.Name,"xx");
        }
    }
    
    public class ExcutorA: CompareAndExecutor<People, People>
    {
        public override bool AllowExcute(ComparisonObject<People, People> comparison)
        {
            return comparison.Compare(r => r.Name, s => s.Name);
        }

        public override void Execute(ComparisonObject<People, People> comparison)
        {
            comparison.Source.Name = "xx";
        }
    }
    
     
    public class ExcutorB: CompareAndExecutor<People, People>
    {
        public override bool AllowExcute(ComparisonObject<People, People> comparison)
        {
            return comparison.Compare(r => r.Age, s => s.Age);
        }

        public override void Execute(ComparisonObject<People, People> comparison)
        {
            comparison.Source.Age = 10;
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

  
}
