﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationHandler.cs" company="">
//   
// </copyright>
// <summary>
//   默认验证处理器，直接抛出异常
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Validation
{
    using System;
    using System.Linq;

    using Taf.Core;

    /// <summary>
    /// 默认验证处理器，直接抛出异常
    /// </summary>
    public class ValidationHandler : IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">
        /// 验证结果集合
        /// </param>
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
            {
                return;
            }

            throw new Exception(results.First().ErrorMessage);
        }
    }
}