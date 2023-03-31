// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidator.cs" company="">
//   
// </copyright>
// <summary>
//   验证操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Validation;

namespace Taf.Core
{
    /// <summary>
    /// 验证操作
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">
        /// 验证目标
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResultCollection"/>.
        /// </returns>
        ValidationResultCollection Validate(object target);
    }
}