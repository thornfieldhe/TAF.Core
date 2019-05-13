// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   范围  min <= value <= max
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;

    /// <summary>
    /// The range attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The _min.
        /// </summary>
        private readonly double _min;
        
        /// <summary>
        /// The _max
        /// </summary>
        private readonly double _max;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max"></param>
        public RangeAttribute(double min,double max)
            : base("min")
        {
            _min = min;
            _max = max;
        }

        public RangeAttribute() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class.
        /// </summary>
        /// <param name="max">
        /// The min.
        /// </param>
        public RangeAttribute(double max)
            : base("max")
        {
            _max = max;
            _min = 0;
        }

        /// <summary>
        /// Gets the min.
        /// </summary>
        public object Min => _min;

        /// <summary>
        /// Gets the max.
        /// </summary>
        public object Max => _max;

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

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _min,_max);
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

            return isDouble && valueAsDouble >= _min && valueAsDouble<=_max;
        }
    }
}