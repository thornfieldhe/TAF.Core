namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public static class RemoteConfigurationBuilderExt{
    /// <summary>
    /// 添加本地配置appsetting.json作为文件源
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static IConfigurationBuilder AddRemoteConfiguration(
        this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange = true){
        if(builder == null){
            throw new ArgumentNullException(nameof(builder));
        }

        if(string.IsNullOrEmpty(path)){
            throw new ArgumentException("配置文件路径不能为空", nameof(path));
        }

        var source = new RemoteConfigurationSource{
            FileProvider = null, Optional = optional, Path = $"appsettings.{path}.json", ReloadOnChange = reloadOnChange
        };
 
        source.ResolveFileProvider();
        
        builder.Add(source);
        return builder;
    }
    
    /// <summary>
    /// 通过Dapr远程加载配置
    /// </summary>
    /// <param name="host"></param>
    public static void AddRemoteConfiguration(this WebApplicationBuilder builder) =>
        builder.WebHost.ConfigureAppConfiguration((contex, configBuilder)=> {
            configBuilder.AddRemoteConfiguration(contex.HostingEnvironment.EnvironmentName, true); //注入本地配置
        });
}
