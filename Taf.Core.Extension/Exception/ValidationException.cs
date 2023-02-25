// 何翔华
// Taf.Core.Net.Utility
// ValidationException.cs

using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Taf.Core.Extension;

/// <summary>
/// 数据校验异常
/// </summary>
public class ValidationException : System.Exception{
    public ValidationException(string message) : base(message){
        ValidationErrors = new List<ValidationResult>();
        LogLevel         = LogLevel.Warning;
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="validationErrors">Validation errors</param>
    public ValidationException(string message, IList<ValidationResult> validationErrors)
        : base(message)
    {
        ValidationErrors = validationErrors;
        LogLevel         = LogLevel.Warning;
    }

    /// <summary>
    /// Detailed list of validation errors for this exception.
    /// </summary>
    public IList<ValidationResult> ValidationErrors{ get; }

    /// <summary>
    /// Exception severity.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel{ get; set; }

}