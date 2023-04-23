// 何翔华
// Taf.Core.Web
// UserFriendlyException.cs

namespace Taf.Core.Web;

/// <summary>
/// 用户友好异常
/// </summary>
public class UserFriendlyException : Exception{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    public UserFriendlyException(string message, int code) : base(message) => Code    = code;


    /// <summary>
    /// Additional information about the exception.
    /// </summary>
    public string Details{ get; }


    /// <summary>
    /// 用于标记异常发生位置,便于查找
    /// </summary>
    public int Code{ get; }
}
