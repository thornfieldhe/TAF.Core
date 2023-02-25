// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BussinessException.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// BussinessException.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// 业务异常
/// </summary>
public class BussinessException : Exception,IHasErrorCode{
    public BussinessException(string message, Guid errorCode,string details =null) : base(message){
        Details   = details;
        ErrorId   = $"ERR_{Randoms.GetRandomCode(6, "1234567890")}";
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
    
    /// <summary>
    /// 生成唯一异常Id,用于日志追溯 
    /// </summary>
    public string ErrorId{ get; }
}