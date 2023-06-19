// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LruCacheTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Test
// LruCacheTest.cs

namespace Taf.Core.Test;

using System;

/// <summary>
/// Lru缓存测试
/// </summary>
public class LruCacheTest{
   [Fact]
   public void TestLru(){
      var list = new List<int>(){
         1
       , 2
       , 3
       , 4
       , 5
       , 6
       , 4
       , 2
       , 5
      };
      var cache = new LruCache<int, int>(4);
      foreach(var l in list){
         cache.Put(l, l);
      }

      Assert.Equal(0, cache.Get(1));
      Assert.Equal(2, cache.Get(2));
      Assert.Equal(4, cache.Get(4));
      Assert.Equal(5, cache.Get(5));
      Assert.Equal(6, cache.Get(6,(s)=>6));
   }
}
