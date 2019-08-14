// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   范围  min < value < max
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;

    /// <summary>
    /// 浮点数范围(x0,x1)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleRangeAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The _min.
        /// </summary>
        protected readonly double Min;
        
        /// <summary>
        /// The _max
        /// </summary>
        protected readonly double Max;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRangeAttribute"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max"></param>
        public DoubleRangeAttribute(double min,double max)
            : base("min")
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// 
        /// </summary>
        public DoubleRangeAttribute() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRangeAttribute"/> class.
        /// </summary>
        /// <param name="max">
        /// The min.
        /// </param>
        public DoubleRangeAttribute(double max)
            : this(0,max)
        {
            Max = max;
            Min = 0;
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
                ErrorMessage = "属性 {0}应大于{1},应小于{2}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Min,Max);
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

            return isDouble && valueAsDouble >= Min && valueAsDouble<=Max;
        }
    }
    
    
    
    /// <summary>
    /// 浮点数范围[x0,x1]包含边界值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleRange2Attribute :DoubleRangeAttribute
    {
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
                ErrorMessage = "属性 {0}应大于等于{1},应小于等于{2}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Min, Max);
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

            return isDouble && valueAsDouble > Min && valueAsDouble <Max;
        }
    }
}