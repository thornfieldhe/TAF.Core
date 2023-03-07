using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using Taf.Core.Utility;

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public static class LocalConfigurationBuilderExt{
    /// <summary>
    /// 添加本地配置appsetting.json作为文件源
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static IConfigurationBuilder AddLocalConfiguration(
        this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange = true){
        if(builder == null){
            throw new ArgumentNullException(nameof(builder));
        }

        if(string.IsNullOrEmpty(path)){
            throw new ArgumentException("配置文件路径不能为空", nameof(path));
        }

        var source = new LocalConfigurationSource(){
            FileProvider = null, Optional = optional, Path = $"appsettings.{path}.json", ReloadOnChange = reloadOnChange
        };
 
        source.ResolveFileProvider();
        
        builder.Add(source);
        return builder;
    }
    
    public static void AddLocalConfiguration(this ConfigureWebHostBuilder builder)=>
        builder.ConfigureAppConfiguration((contex, configBuilder)=> {
            configBuilder.AddLocalConfiguration(contex.HostingEnvironment.EnvironmentName, true); //注入本地配置
        });
}
