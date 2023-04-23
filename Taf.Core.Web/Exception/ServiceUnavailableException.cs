// 何翔华
// Taf.Core.Extension
// ServiceUnavailableException.cs

namespace Taf.Core.Web;

/// <summary>
/// 远程服务不可用异常,返回503
/// </summary>
public class ServiceUnavailableException : Exception{
    public string Url{ get;  }

    /// <summary>
    /// 用于标记异常发生位置,便于查找
    /// </summary> 
    public Guid ErrorCode{ get; }


    public ServiceUnavailableException(string message, string url, Guid errorCode, Exception? innerException=null ) : base(
        message, innerException){
        Url = url;
        ErrorCode=errorCode;
    }
    
}
