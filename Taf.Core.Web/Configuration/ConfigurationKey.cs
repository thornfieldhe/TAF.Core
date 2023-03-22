// 何翔华
// Taf.Core.Web
// ConfigurationKey.cs

namespace Taf.Core.Web;

/// <summary>
/// 配置默认键
/// </summary>
public static class ConfigurationKey{
    /// <summary>
    /// 是否启用加密保存
    /// </summary>
    public static string IsEncrypted => "IsEncrypted";
    

    /// <summary>
    /// Dapr远程配置
    /// </summary>
    public static string DaprConfigKeys => "DaprConfigKeys";

}
