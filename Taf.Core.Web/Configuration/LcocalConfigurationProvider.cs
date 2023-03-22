// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LcocalConfigurationLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// LcocalConfigurationLoader.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 本地配置文档加载器
/// </summary>
public class LcocalConfigurationProvider : JsonConfigurationProvider{
    public LcocalConfigurationProvider(LocalConfigurationSource source) : base(source){ }


    public override void Load(Stream stream){
        base.Load(stream);
        if(IsEncrypted()){
            foreach(var item in Data){
                if(item.Key != ConfigurationKey.IsEncrypted){
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

    private bool IsEncrypted() =>
        Data.TryGetValue(ConfigurationKey.IsEncrypted, out var encryptedValue)
     && bool.TryParse(encryptedValue, out var isEncrypted);
}

public class DaprConfigurationProvider : JsonConfigurationProvider{
    private DaprClient _daprClient;

    public DaprConfigurationProvider(JsonConfigurationSource source, DaprClient daprClient) : base(source) =>
        _daprClient = daprClient;

    public override void Load(){
        base.Load();
        if(IsEncrypted()){
           
        }

        var sb = new StringBuilder();
        foreach (var d in Data)
        {
            if(d.As<IStringReg>().IsMatch(ConfigurationKey.DaprConfigKeys +@"}\d{1,}$")){
                sb.Append($"&keys={d.Value}");
            }
        }

        var parameters = sb.ToStr();
        Fx.If(!string.IsNullOrWhiteSpace(parameters))
          .Then(() => {
               var dic = _daprClient.InvokeMethodAsync<Dictionary<string, string>>(
                   HttpMethod.Get,
                   "Configuration",
                   "getConfigs").Result;
           });
    }

    private bool IsEncrypted() =>
        Data.TryGetValue(ConfigurationKey.IsEncrypted, out var encryptedValue)
     && bool.TryParse(encryptedValue, out var isEncrypted);
}
