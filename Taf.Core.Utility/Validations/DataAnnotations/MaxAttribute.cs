// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaxAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   最大值 value < max
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using Globalization;

    /// <summary>
    /// 最大值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxAttribute : DataTypeAttribute
    {
        /// <summary>
        /// 最大值不包含边界
        /// </summary>
        protected  double Max;

        /// <summary>
        /// 最大值不包含边界
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public MaxAttribute(int max)
            : base("max") =>
            Max = max;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttribute"/> class.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public MaxAttribute(double max)
            : base("max") =>
            Max = max;

        /// <summary>
        /// The format error message.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "属性 {0}应小于属性{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Max);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var isDouble = double.TryParse(Convert.ToString(value), out var valueAsDouble);

            return isDouble && valueAsDouble < Max;
        }
    }
    
    /// <summary>
    /// 最大值包含边界
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Max2Attribute:MaxAttribute
    {
        /// <summary>
        /// 最大值包含边界
        /// </summary>
        /// <param name="max"></param>
        public Max2Attribute(int max) : base(max) => Max = max;

        /// <summary>
        /// 最大值包含边界
        /// </summary>
        /// <param name="max"></param>
        public Max2Attribute(double max) : base(max) => Max = max;

        /// <summary>
        /// The format error message.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "属性 {0}应小于等于属性{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Max);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var isDouble = double.TryParse(Convert.ToString(value), out var valueAsDouble);

            return isDouble && valueAsDouble <= Max;
        }
    }
}