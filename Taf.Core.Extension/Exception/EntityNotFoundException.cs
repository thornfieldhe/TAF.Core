// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityNotFoundException.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Extension
// EntityNotFoundException.cs

namespace Taf.Core.Extension;

/// <summary>
/// 对象不存在异常,返回404
/// </summary>
public class EntityNotFoundException : Exception{
    public string Details{ get; set; }
    /// <summary>
    /// 用于标记异常发生位置,便于查找
    /// </summary> 
    public Guid ErrorCode{ get; }
    
    /// <summary>
    /// 对象类型
    /// </summary> 
    public Type Type{ get; }
    public EntityNotFoundException(string message,Type type, Guid errorCode, string? details =null) :base(message){
        ErrorCode = errorCode;
        Details   = details;
        Type      = type;
    }
}
