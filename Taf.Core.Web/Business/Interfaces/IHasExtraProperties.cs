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

using System.Text.Json;
using Taf.Core.Utility;

namespace Taf.Core.Web;

/// <summary>
/// 是否包含扩展属性
/// </summary>
public interface IHasExtraProperties{
   /// <summary>
   /// json序列化存储额外属性
   /// </summary>
   public string? ExtensionData{ get; set; } 
   
   
   /// <summary>
   /// 数据库中需要过滤此属性
   /// </summary>
   public Dictionary<string, string> AllExtensions(){
         if(ExtensionData.IsNullOrEmpty()){
            return new Dictionary<string, string>();
         }

         Dictionary<string, string> et = null;
         try{
            et = JsonSerializer.Deserialize<Dictionary<string, string>>(ExtensionData);
         } catch(Exception ex){
            throw new BussinessException("扩展字段不能反序列化为Dictionary", new Guid("5551002F-26E5-4D9F-ABB0-14404374BD4F")
                                       , ex.Message);
         }

         return et;
   }

   /// <summary>
   /// 获取扩展属性
   /// </summary>
   /// <param name="key"></param>
   /// <returns></returns>
   public string? GetValue(string key){
      var extionsions = AllExtensions();
      extionsions.TryGetValue(key, out var value);
      return value;
   }
   
   /// <summary>
   /// 设置扩展属性
   /// </summary>
   /// <param name="key"></param>
   /// <param name="value"></param>
   public void SetKey(string key, string value){
      var extionsions = AllExtensions();
      extionsions[key] = value;
      ExtensionData  = JsonSerializer.Serialize(extionsions);
   }
}
