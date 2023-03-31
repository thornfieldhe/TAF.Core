// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BussinessException.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Web
// BussinessException.cs

namespace Taf.Core.Extension;

/// <summary>
/// 业务异常
/// </summary>
public class BussinessException : Exception,IHasErrorCode{
    public BussinessException(string message, Guid errorCode,string details =null) : base(message){
        Details   = details;
        ErrorCode = errorCode;
    }
    
    /// <summary>
    /// 用于标记异常发生位置,便于查找
    /// </summary> 
    public Guid ErrorCode{ get; }

    /// <summary>
    /// Additional information about the exception.
    /// </summary>
    public string Details{ get; }
}