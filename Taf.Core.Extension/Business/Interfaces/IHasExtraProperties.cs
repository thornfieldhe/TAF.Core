// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHasExtraProperties.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   是否包含扩展属性
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Net.Utility
// IHasExtraProperties.cs

namespace Taf.Core.Extension;

/// <summary>
/// 是否包含扩展属性
/// </summary>
public interface IHasExtraProperties{
   /// <summary>
   /// json序列化存储额外属性
   /// </summary>
   public string? ExtensionData{ get; set; } 
}
