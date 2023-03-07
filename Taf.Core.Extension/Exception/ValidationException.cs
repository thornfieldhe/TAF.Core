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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="validationErrors">Validation errors</param>
    public ValidationException(IEnumerable<string> validationErrors, string? message=null)
        : base(message) => ValidationErrors = validationErrors;

    /// <summary>
    /// Detailed list of validation errors for this exception.
    /// </summary>
    public IEnumerable<string> ValidationErrors{ get; }


}