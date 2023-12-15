// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidator.cs" company="">
//   
// </copyright>
// <summary>
//   验证操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Taf.Core.Utility
{
    /// <summary>
    /// 验证操作
    /// </summary>
    public interface IValidator<T>
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResultCollection"/>.
        /// </returns>
        ValidationResultCollection Validate();
    }
}