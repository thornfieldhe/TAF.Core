// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LcocalConfigurationLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration.Json;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// LcocalConfigurationLoader.cs

namespace Taf.Core.Web;

/// <summary>
/// 本地配置文档加载器
/// </summary>
public class LcocalConfigurationProvider : JsonConfigurationProvider{
    public LcocalConfigurationProvider(LocalConfigurationSource source) : base(source){ }


    public override void Load(Stream stream){
        base.Load(stream);
        if(IsEncrypted()){
            foreach(var item in Data){
                if(item.Key != SystemKeys.IsEncrypted){
                    Data[item.Key] = Encrypt.DesDecrypt(item.Value);
                }
            }
        }
    }
    

    public override void Set(string key, string? value){
        if(IsEncrypted()){
            base.Set(key, Encrypt.DesEncrypt(value));
        } else{
            base.Set(key, value);
        }
    }

    private bool IsEncrypted(){
        if(Data.TryGetValue(SystemKeys.IsEncrypted, out var encryptedValue)
        && bool.TryParse(encryptedValue, out var isEncrypted)){
            return isEncrypted;
        }

        return false;
    }
}