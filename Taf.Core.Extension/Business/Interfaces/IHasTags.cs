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

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Taf.Core.Utility;

namespace Taf.Core.Extension;

/// <summary>
/// 是否包含扩展属性
/// </summary>
public interface IHasTags{
   /// <summary>
   /// json序列化存储额外属性
   /// </summary>
   public string? TagData{ get; set; } 
   
   
   /// <summary>
   /// 数据库中需要过滤此属性
   /// Tags属性只允许通过SetTag方法来赋值
   /// </summary>
   public List<string> GetTags(){
      if(TagData.IsNullOrEmpty()){
            return new List<string>();
      }

      List <string> et = null;
      try{
         et = JsonSerializer.Deserialize<List<string>>(TagData);
      } catch(Exception ex){
         throw new BussinessException("标签字段不能反序列化为List<string>", new Guid("C5610460-F56F-46AC-A4A7-10C1BBDF4862")
                                    , ex.Message);
      }

      return et??new List<string>();
   }

   
   /// <summary>
   /// 设置扩展属性
   /// </summary>
   /// <param name="key"></param>
   /// <param name="value"></param>
   public void SetTag(string tag){
      if(!string.IsNullOrWhiteSpace(tag)){
         tag = tag.Trim();
         var tags = GetTags();
         if(!tags.Contains(tag)){
            tags.Add(tag);
            TagData = JsonSerializer.Serialize(tags);
         }
      }
   }
}
