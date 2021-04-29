// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using Taf.Core.Utility;
using Xunit;

// 何翔华
// Taf.Core.Test
// CopyTest.cs

namespace Taf.Core.Test{
    using System;

    /// <summary>
    /// Summary
    /// </summary>
    public class CopyTest{
        [Fact]
        public void TestDeepCoppy(){
            var e1 = new CopyEntity(){
                Age   = 20
              , Ext   = new Dictionary<string, string>(){{"aaa", "bbb"}}
              , Id    = Guid.NewGuid()
              , Name  = "zs"
              , Name2 = "ls"
              , Name3 = "wem"
                ,Xx = "abc"
                ,User = new User2(){S = new List<string>(){"12","22"}}
            };
                var m1= e1.DeepCopy();
                Assert.Equal(m1.Age,e1.Age);
                Assert.Equal(m1.Name3,e1.Name3);
                var m = e1.Copy<CopyEntity,CopyEntity2>();
                Assert.Equal(m.Age,   e1.Age);
                Assert.Equal(m.Name3, e1.Name3);
                Assert.Equal(m.User.S.Count,e1.User.S.Count);
                m.Ext.Add("333","444");
                Assert.NotEqual(m.Ext.Count,e1.Ext.Count);

                Assert.NotNull(e1.Xx);
                Assert.NotNull(m1.Xx);

                e1.User.S.Add("ffff");
                Assert.NotEqual(m1.User.S.Count, e1.User.S.Count);
        }
    }

    [Serializable]
    public class CopyEntity{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Age{ get; set; }
        public Dictionary<string,string> Ext{ get; set; }
        public string Name2{ get; set; }
        public string Name3{ get; set; }

        public string Xx  { get; set; }
        public User2  User{ get; set; }
    }

    public class CopyEntity2{
        public Guid                      Id   { get; set; }
        // public string                    Name { get; set; }
        public int                       Age  { get; set; }
        public Dictionary<string,string> Ext  { get; set; }
        public string                    Name2{ get; set; }
        public string                    Name3{ get; set; }

        public string Xx  { get; set; }
        public User2  User{ get; set; }
    }

    [Serializable]
    public class  User2{
        public List<string> S{ get; set; }
    }
}
