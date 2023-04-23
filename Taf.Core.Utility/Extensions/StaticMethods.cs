// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticMethods.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Utility
// StaticMethods.cs

namespace Taf.Core.Utility;

/// <summary>
/// 扩展的静态方法
/// </summary>
public static  class StaticMethods{
            
    /// <summary>
    /// 将数组转换为列表    
    /// </summary>
    /// <param name="elements"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>
    ///var arr = new [] { param1, param2,... };  =>  var arr = Arr(param1, param2,...);
    /// </returns>
    public static IEnumerable<T> Arr<T>(params T[] elements) => elements;
}
