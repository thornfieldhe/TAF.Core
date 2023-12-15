// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validator.cs" company="">
//   
// </copyright>
// <summary>
//   验证操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Taf.Core.Utility{
    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validator<T> : IValidator<T>{
        private readonly T                          _target;
        private readonly ValidationResultCollection _result = new();
        public Validator(T target) => _target = target;

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">
        /// 验证目标
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResultCollection"/>.
        /// </returns>
        public ValidationResultCollection Validate(){
            _target.CheckNull("target");
            var validationResults = new List<ValidationResult>();
            var context           = new ValidationContext(_target, null, null);
            var isValid           = Validator.TryValidateObject(_target, context, validationResults, true);
            if(!isValid){
                _result.AddResults(validationResults);
            }

            return _result;
        }

        public Validator<T> AddRule(Predicate<T> validator,  string errMessage, string[] memberNames){
            if(!validator(_target)){
                _result.Add(new ValidationResult(errMessage, memberNames));
            }

            return this;
        }
    }
}
