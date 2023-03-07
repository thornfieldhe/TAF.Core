// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LcocalConfigurationLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// LcocalConfigurationLoader.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 本地配置文档加载器
/// </summary>
public class LcocalConfigurationProvider: JsonConfigurationProvider{

    public LcocalConfigurationProvider(LocalConfigurationSource source) : base(source){ }
    

    public override void Load(Stream stream){
        base.Load(stream);
        if (IsEncrypted()){
            foreach(var item in Data){
                if(item.Key != ConfigurationKey.IsEncrypted){
                    Data[item.Key] = Encrypt.DesDecrypt(item.Value);
                }
            }
        } 
    }

    public override void Set(string key, string? value){
        if (IsEncrypted()){
            base.Set(key,Encrypt.DesEncrypt(value));
        }else{
            base.Set(key, value);
        }
    }
    
private bool IsEncrypted() =>
        Data.TryGetValue(ConfigurationKey.IsEncrypted, out var encryptedValue)
     && bool.TryParse(encryptedValue, out var isEncrypted);

}