// 何翔华
// Taf.Core.Web
// DaprConfigurationProvider.cs

using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Taf.Core.Extension;
using Taf.Core.Utility;

namespace Taf.Core.Web;

public class RemoteConfigurationProvider : JsonConfigurationProvider{
    public RemoteConfigurationProvider(RemoteConfigurationSource source) : base(source){
        if(Source.ReloadOnChange
        && Source.FileProvider != null){
            ChangeToken.OnChange(
                () => RemoteConfigurationChangeToken.Watch(),
                () => {
                    Thread.Sleep(Source.ReloadDelay);
                    Load();
                });
        }
    }

    public override void Load(Stream stream){
        base.Load(stream);
        RemoteConfigurationChangeToken.Keys.Clear();//清理配置
        var isEncrypted = IsEncrypted();
        var appsettingsJsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.cfg");
        var sb = new StringBuilder();
        if(Data.TryGetValue(SystemKeys.RemoteConfigAddress, out var address)){
            foreach(var d in Data){
                if(d.Key.As<IStringReg>().IsMatch(SystemKeys.RemoteConfigKeys + @":\d{1,}$")){
                    var value = isEncrypted ? Encrypt.DesDecrypt(d.Value) : d.Value;
                    sb.Append($"&keys={value}");
                    RemoteConfigurationChangeToken.Keys.Add(d.Value);
                }
            }
        }

        var parameters = sb.ToStr();
        Fx.If(!string.IsNullOrWhiteSpace(parameters))
          .Then(() => {
               try{
                   var url = $"/getConfigs?{parameters.Trim('&')}";
                   Trace.TraceInformation(">>>>>>>>>>>>>>>>>>>>>>>url:" + url);
                   var client = new HttpClient();
                   client.BaseAddress = new Uri(address);
                   var dic            = client.GetFromJsonAsync<R<Dictionary<string, string>>>(url).Result.Data;
                   var hasRedisClient = false;
                   foreach(var d in dic){
                       if(!Data.ContainsKey(d.Key)){
                           Data.TryAdd(d.Key, d.Value);
                           if (d.Key=="Redis:ConnectionString")//如果有注入redis,则在此处初始化redis
                           {
                               hasRedisClient = true;
                           }
                       }
                   }

                   Fx.If(hasRedisClient).Then(() => {
                       RedisClientServer.Instance.LoadConfig(Data["Redis:ConnectionString"]
                                                           , Data["Redis:DatabaseId"].ToInt());
                   });

                   //保存本地配置文件,方便在远程配置拉取失败时,从本地缓存中获取上次正确配置
                   var encryptedString = Encrypt.DesEncrypt(JsonSerializer.Serialize(Data));
                   File.WriteAllTextAsync(appsettingsJsonPath, encryptedString).GetAwaiter();
               } catch(Exception ex){
                   var data = File.ReadAllText(appsettingsJsonPath);
                   Data = JsonSerializer.Deserialize<Dictionary<string, string>>(Encrypt.DesDecrypt(data));
               }
           });
    }


    private bool IsEncrypted(){
        if(Data.TryGetValue(SystemKeys.IsEncrypted, out var encryptedValue)
        && bool.TryParse(encryptedValue, out var isEncrypted)){
            return isEncrypted;
        }

        return false;
    }
}
