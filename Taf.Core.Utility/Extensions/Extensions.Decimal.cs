﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Decimal.cs" company="">
//   
// </copyright>
// <summary>
//   decimal扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Core.Utility
{
    using System;

    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 将小数值按指定的小数位数截断
        /// </summary>
        /// <param name="d">
        /// 要截断的小数
        /// </param>
        /// <param name="s">
        /// 小数位数，s大于等于0，小于等于28
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public static decimal ToFixed(this decimal d, int s)
        {
            var sp = Convert.ToDecimal(Math.Pow(10, s));

            if (d < 0)
            {
                return Math.Truncate(d) + Math.Ceiling((d - Math.Truncate(d)) * sp) / sp;
            }

            return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        }

        /// <summary>
        /// 按照位数四舍五入
        /// </summary>
        /// <param name="d">
        /// </param>
        /// <param name="s">
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public static decimal Round(this decimal d, int s)
        {
            return Math.Round(d, s);
        }

        /// <summary>
        /// 是否在范围之间
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <param name="max">
        /// </param>
        /// <param name="min">
        /// </param>
        /// <param name="allowEqual">
        /// 是否包含等于
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsBetween(this decimal obj, decimal max, decimal min, bool allowEqual = false)
        {
            if (allowEqual)
            {
                return obj >= min && obj <= max;
            }

            return obj > min && obj < max;
        }

        /// <summary>
        /// 获取格式化字符串：x.xx
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
        public static string Format(this decimal number, string defaultValue = "")
        {
            return number == 0 ? defaultValue : string.Format("{0:0.##}", number);
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
        public static string Format(this decimal? number, string defaultValue = "")
        {
            return Format(number.SafeValue(), defaultValue);
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
        public static string FormatRmb(this decimal number)
        {
            return number == 0 ? "￥0" : string.Format("￥{0:0.##}", number);
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
        public static string FormatRmb(this decimal? number)
        {
            return FormatRmb(number.SafeValue());
        }

        /// <summary>
        /// 获取格式化字符串,x.xx%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this decimal number)
        {
            return number == 0 ? string.Empty : string.Format("{0:0.##}%", number);
        }

        /// <summary>
        /// 获取格式化字符串,x.xx%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this decimal? number)
        {
            return FormatPercent(number.SafeValue());
        }

        /// <summary>
        /// 移除尾随0
        /// </summary>
        /// <param name="value">
        /// 值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RemoveEnd0(this decimal value)
        {
            var result = value.ToString();
            if (result.IndexOf('.') < 0)
            {
                return result;
            }

            if (result.EndsWith("0"))
            {
                result = result.TrimEnd('0');
            }

            if (result.EndsWith("."))
            {
                result = result.TrimEnd('.');
            }

            return result;
        }
        
        /// <summary>
        /// 把Decimal表示为科学计数法的字符串
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="sd">有效位数</param>
        /// <returns></returns>
        public static string ToScienceString(this decimal v, int sd)
        {
            return v.ToString("E" + sd.ToString());
        }

        public static string ToPercentString(this decimal v, int sd)
        {
            return v.ToString("P" + sd.ToString());
        }

    }
}