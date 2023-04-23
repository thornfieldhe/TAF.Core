// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyErrorValidateionRule.cs" company="">
//   
// </copyright>
// <summary>
//   空白验证规则，只需要传入需要返回的错误信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Taf.Core.Utility
{
    /// <summary>
    /// 空白验证规则，只需要传入需要返回的错误信息
    /// </summary>
    public class EmptyErrorValidateionRule : IValidationRule
    {
        /// <summary>
        /// The error message.
        /// </summary>
        private readonly string _errorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyErrorValidateionRule"/> class.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public EmptyErrorValidateionRule(string errorMessage) => _errorMessage = errorMessage;

        /// <summary>
        /// The validate.
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate() => new(_errorMessage);
    }
}