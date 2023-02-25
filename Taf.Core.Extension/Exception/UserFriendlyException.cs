// 何翔华
// Taf.Core.Web
// UserFriendlyException.cs

namespace Taf.Core.Extension;

/// <summary>
/// 用户友好异常
/// </summary>
public class UserFriendlyException : Exception, IHasErrorCode{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    public UserFriendlyException(string message, Guid errorId, string details =null) : base(message){
        Details   = details;
        ErrorCode = errorId;
    }


    /// <summary>
    /// Additional information about the exception.
    /// </summary>
    public string Details{ get; }

    /// <summary>
    /// 用于标记异常发生位置,便于查找
    /// </summary>
    public Guid ErrorCode{ get; }
}
