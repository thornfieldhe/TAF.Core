// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationRule.cs" company="">
//   
// </copyright>
// <summary>
//   验证规则
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Taf.Core
{
    /// <summary>
    /// 验证规则
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        ValidationResult Validate();
    }
}