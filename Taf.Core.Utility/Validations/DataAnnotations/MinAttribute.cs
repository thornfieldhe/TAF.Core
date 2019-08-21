// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   最小值 min < value
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;

    /// <summary>
    /// 最小值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The _min.
        /// </summary>
        protected double Min;

        /// <summary>
        /// 目标值大于最小值不包含最小值
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        public MinAttribute(int min)
            : base("min")
        {
            Min = min;
        }

        public MinAttribute() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttribute"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        public MinAttribute(double min)
            : base("min")
        {
            Min = min;
        }

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
                ErrorMessage = "属性 {0}应大于{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Min);
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

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble > Min;
        }
    }
    
    
    /// <summary>
    /// 最小值包含
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Min2Attribute:MinAttribute
    {
        /// <summary>
        /// 目标值大于最小值包含最小值
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        public Min2Attribute(int min)
            : base(min)
        {
            Min = min;
        }

        public Min2Attribute() : this(0)
        {
        }
        
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
                ErrorMessage = "属性 {0}应大于等于{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Min);
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

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble >= Min;
        }
    }
}