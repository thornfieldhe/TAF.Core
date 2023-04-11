// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExprsssionTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

// 何翔华
// Taf.Core.Test
// ExprsssionTest.cs

namespace Taf.Core.Test;

using System;

/// <summary>
/// $Summary$
/// </summary>
public class ExprsssionTest{
    /// <summary>
    /// 测试表达式And
    /// </summary>
    [Fact]
    public void TestExpressionAnd()
    {
        var users = new List<User>();
        users.Add(new User(){ Age = 10, Sex = true, UserName  = "zhangsan" });
        users.Add(new User(){ Age = 11, Sex = false, UserName = "zhangsan1" });
        users.Add(new User(){ Age = 12, Sex = true, UserName  = "zhangsan2" });
        users.Add(new User(){ Age = 13, Sex = true, UserName  = "zhangsan3" });
        users.Add(new User(){ Age = 14, Sex = false, UserName = "zhangsan4" });
        Expression<Func<User, bool>> express  = s => s.Sex;
        Expression<Func<User, bool>> express2 = s => s.Age > 12;
        express = express.And(express2);
        var ll = users.Where(express.Compile()).ToList();
        Assert.Equal(ll.Count,1);
    }
    
    /// <summary>
    /// 测试表达式Or
    /// </summary>
    [Fact]
    public void TestExpressionOr()
    {
        var users = new List<User>();
        users.Add(new User(){ Age = 10, Sex = true, UserName  = "zhangsan" });
        users.Add(new User(){ Age = 11, Sex = false, UserName = "zhangsan1" });
        users.Add(new User(){ Age = 12, Sex = true, UserName  = "zhangsan2" });
        users.Add(new User(){ Age = 13, Sex = true, UserName  = "zhangsan3" });
        users.Add(new User(){ Age = 14, Sex = false, UserName = "zhangsan4" });
        users.Add(new User(){ Age = 15, Sex = false, UserName = "zhangsan5" });
        Expression<Func<User, bool>> express  = s => s.Sex;
        Expression<Func<User, bool>> express2 = s => s.Age > 12;
        express = express.Or(express2);
        var ll = users.Where(express.Compile()).ToList();
        Assert.Equal(ll.Count,5);
    }

    private class User{
        public string UserName{ get; set; }
        public int    Age     { get; set; }
        public bool   Sex     { get; set; }
    }
}
