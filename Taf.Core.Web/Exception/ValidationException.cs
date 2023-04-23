// 何翔华
// Taf.Core.Net.Utility
// ValidationException.cs

namespace Taf.Core.Web;

/// <summary>
/// 数据校验异常
/// </summary>
public class ValidationException : Exception{

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