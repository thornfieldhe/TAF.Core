// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtend.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   List扩展方法
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

// 何翔华
// Taf.Core.Utility
// CollectionExtend.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
/// List扩展方法
/// </summary>
public static class CollectionExtention{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ids">传入的Id列表</param>
    /// <param name="query">具体的查询数据库逻辑</param>
    /// <param name="defaultList">系统默认已加载列表</param>
    /// <param name="maxcount">分批查询,每批次查询条数</param>
    /// <param name="where">返回结果的过滤条件</param>
    /// <typeparam name="Tk"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<Tk> GetList<Tk, T>(
        HashSet<T>? ids, Func<IEnumerable<T>, List<Tk>> query, int maxcount, List<Tk> defaultList
      , Func<Tk, T> where) where T : class where Tk : class{
        var locker = new object();
        lock(locker){
            if(ids == null){
                return defaultList;
            }

            if(!ids.Any()){
                return new List<Tk>();
            }

            var result   = new List<Tk>();
            var otherIds = new List<T>();
            foreach(var id in ids){
                var l = defaultList.FirstOrDefault(r => where(r) == id);
                if(l == null){
                    otherIds.Add(id);
                } else{
                    result.Add(l);
                }
            }
            if (result.Count==ids.Count){
                return result;
            }

            var groupCount = otherIds.Count % maxcount == 0
                ? otherIds.Count / maxcount
                : otherIds.Count / maxcount + 1;
            var ls = new List<Tk>[groupCount];
            if(otherIds.Count > 0){
                for(var index = 0; index < groupCount; index++){
                    var newList = otherIds.Skip(index * maxcount).Take(maxcount).ToList();
                    ls[index] = query(newList);
                }

                foreach(var l in ls){
                    defaultList.AddRange(l);
                    result.AddRange(l);
                }
            }

            return result;
        }
    }
}
