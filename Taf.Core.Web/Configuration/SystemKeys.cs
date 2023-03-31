// 何翔华
// Taf.Core.Web
// ConfigurationKey.cs

namespace Taf.Core.Web;

/// <summary>
/// 配置默认键
/// </summary>
public static class SystemKeys{
#region 配置

    /// <summary>
    /// 是否启用加密保存
    /// </summary>
    public static string IsEncrypted => "IsEncrypted";


    /// <summary>
    /// Dapr远程配置:AppId
    /// </summary>
    public static string RemoteConfigAddress => "RemoteConfigAddress:Path";

    /// <summary>
    /// Dapr远程配置:MethodName
    /// </summary>
    public static string RemoteConfigKeys => "RemoteConfigAddress:Keys";

#endregion

#region Redis配置

    public static string KeyUserPermissions => "userPermissions:{0}";

#endregion

#region 系统重要常量

    public static string SecurityKey = "GQDstcKsx0NHjPOuxOYg5MbeJ1XT0UFiwDVvVBrk";

#endregion
}
