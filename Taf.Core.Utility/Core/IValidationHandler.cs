﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationHandler.cs" company="">
//   
// </copyright>
// <summary>
//   验证处理器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Validation;

namespace Taf.Core
{
    /// <summary>
    /// 验证处理器
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">
        /// 验证结果集合
        /// </param>
        void Handle(ValidationResultCollection results);
    }
}