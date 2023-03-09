// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_Double_Math.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Utility
// Extensions.Double.Math.cs

namespace Taf.Core.Utility.Double;

using System;
/// <summary>
/// double格式化
/// </summary>
public interface IDoubleFormat : IExtension<double>{ }


/// <summary>
/// 
/// </summary>
public static class DoubleFormat{

        /// <summary>
        /// 获取格式化字符串x.xx
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
        public static string Format(this IDoubleFormat s, string defaultValue = ""){
            var number = s.GetValue();
          return  number == 0 ? defaultValue : string.Format("{0:0.##}", number);
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
        public static string Format(this double? number, string defaultValue = "") =>
            Format(number.SafeValue(), defaultValue);
        
        /// <summary>
        /// 获取格式化字符串,x.xx%
        /// </summary>
        /// <param name="s">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this IDoubleFormat s){
            var number = s.GetValue();
           return number == 0 ? string.Empty : string.Format("{0:0.##}%", number); 
        }
            
        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this double? number, int sd = 2) => FormatPercent(number.SafeValue(), sd);

        /// <summary>
        /// 转换成科学计数法
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static string FormatScience(this IDoubleFormat s, int sd = 3){
            var v = s.GetValue();
            if(v == 0) return "0";
            var result = v.ToString();
            if(result.Contains("E")){
                var index      = result.IndexOf('.');
                var indexE     = result.IndexOf("E", StringComparison.Ordinal);
                var replaceStr = result.Substring(index + 1, indexE - index - 1);
                if(replaceStr.Length > sd){
                    var firstStr = result.Substring(0, index + 1);
                    var lastStr  = result.Substring(indexE);
                    var delStr   = replaceStr.Substring(0, sd);
                    if(double.TryParse(firstStr + delStr + lastStr, out v)){
                        result = v.ToString();
                    }
                }
            } else if(result.Contains(".")){
                var index = result.IndexOf('.');
                if(index <= 1 && v     < 1){
                    //是小于1的数字
                    if(v < Math.Pow(10, -sd)){
                        result = v.ToString("E" + sd);
                        var resultG = result.Split("E-");
                        result = resultG[0] + "E-" + resultG[1].PadLeft(sd,'0');
                    } else if(result.Substring(index + 1).Length > sd){
                        result = Math.Round(v, sd).ToString();
                    }
                } else if(result.Substring(0, index).Length > sd+1){
                    result = v.ToString("E" + sd);
                    var resultG = result.Split("E+");
                    result = resultG[0] + "E+" + resultG[1].PadLeft(sd,'0');
                } else if(result.Substring(index + 1).Length > sd){
                    result = Math.Round(v, sd).ToString();
                }
            } else if(result.Length > sd+1){
                result = v.ToString("E" + sd);
                var resultG = result.Split("E+");
                result = resultG[0] + "E+" + resultG[1].PadLeft(sd,'0');
            }

            return result;
        }
}
