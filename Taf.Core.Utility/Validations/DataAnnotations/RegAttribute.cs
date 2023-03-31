// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   手机号验证
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 手机号验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MobilePhoneAttribute : RegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobilePhoneAttribute"/> class.
        /// </summary>
        public MobilePhoneAttribute(string properityName =null)
            : base(StringRegExpression.Mobile,"应为手机号码",properityName)
        {
        }
    }

    /// <summary>
    /// 整数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InteAttribute : RegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteAttribute"/> class.
        /// </summary>
        public InteAttribute(string properityName =null)
            : base(StringRegExpression.Integer,"应为整数",properityName)
        {
        }
    }

    /// <summary>
    /// 数字和字母
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LetterAndNumbersAttribute : RegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterAndNumbersAttribute"/> class.
        /// </summary>
        public LetterAndNumbersAttribute(string properityName =null)
            : base(StringRegExpression.LetterAndNumbers,"应为数字和字母",properityName)
        {
        }
    }
}