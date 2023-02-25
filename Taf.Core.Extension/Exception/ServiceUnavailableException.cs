// 何翔华
// Taf.Core.Extension
// ServiceUnavailableException.cs

namespace Taf.Core.Extension;

/// <summary>
/// 远程服务不可用异常,返回503
/// </summary>
public class ServiceUnavailableException:System.Exception{
    
    public string Url{ get; set; }

    public ServiceUnavailableException(string? message, string url) : base(message) => Url = url;

    public ServiceUnavailableException(string? message, Exception? innerException, string url) : base(message, innerException) => Url = url;
}
