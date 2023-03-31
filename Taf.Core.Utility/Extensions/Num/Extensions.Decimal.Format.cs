// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_Decimal_Format.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Utility
// Extensions.Decimal.Format.cs

namespace Taf.Core.Utility;

/// <summary>
/// decimal格式化
/// </summary>
public interface IDecimalFormat : IExtension<decimal>{ }

/// <summary>
/// 
/// </summary>
public static class DecimalFormat{
        /// <summary>
        /// 获取格式化字符串：x.xx
        /// </summary>
        /// <param name="s">
        /// 数值
        /// </param>
        /// <param name="defaultValue">
        /// 空值显示的默认文本
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Format(this IDecimalFormat s, string defaultValue = ""){
            var number = s.GetValue();
          return number == 0 ? defaultValue : $"{number:0.##}"; 
        } 

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <param name="defaultValue">
        /// 空值显示的默认文本
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Format(this decimal? number, string defaultValue = "") => Format(number.SafeValue(), defaultValue);

        /// <summary>
        /// 获取格式化字符串, ￥xx
        /// </summary>
        /// <param name="s">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatRmb(this IDecimalFormat s){
            var number = s.GetValue();
            return number == 0 ? "￥0" : $"￥{number:0.##}";
        }

        /// <summary>
        /// 获取格式化字符串, ￥xx
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatRmb(this decimal? number) => FormatRmb(number.SafeValue());

        /// <summary>
        /// 获取格式化字符串,x.xx%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this decimal? number,int sd=0) => FormatPercent(number.SafeValue(),sd);

        /// <summary>
    /// 把Decimal表示为科学计数法的字符串
    /// </summary>
    /// <param name="v">数值</param>
    /// <param name="sd">有效位数</param>
    /// <returns></returns>
    public static string FormatScience(this IDecimalFormat v, int sd =3) => v.GetValue().ToString("E" + sd);

    public static string FormatPercent(this IDecimalFormat v, int sd =2) => v.GetValue().ToString("P" + sd);
}
