// 何翔华
// Taf.Core.Utility
// StringFormat.cs

using Humanizer;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Taf.Core.Utility;

/// <summary>
/// 字符串格式化
/// </summary>
public interface IStringFormat : IExtension<string>{ }

public static class StringFormat{
    /// <summary>
    /// 移除_并首字母小写的Camel样式
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToCamel(this IStringFormat name){
        var clone = name.GetValue().TrimStart('_');
        clone = RemoveSpaces(ToProperCase(clone));
        return $"{char.ToLower(clone[0])}{clone.Substring(1, clone.Length - 1)}";
    }

    /// <summary>
    /// 移除_并首字母大写的Pascal样式
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToCapit(this IStringFormat name){
        var clone = name.GetValue().TrimStart('_');
        return RemoveSpaces(ToProperCase(clone));
    }

    /// <summary>
    /// 移除最后的字符
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <param name="separator"></param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RemoveFinalChar(this IStringFormat source, char separator){
        var s = source.GetValue();
        if(s.EndsWith(separator.ToString(), StringComparison.Ordinal)
        && s.Length > 1){
            s = s.Substring(0, s.Length - 1);
        }

        return s;
    }

    /// <summary>
    /// 移除最后的逗号
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RemoveFinalComma(this IStringFormat source){
        var s = source.GetValue();
        if(s.Trim().Length <= 0){
            return s;
        }

        var c = s.LastIndexOf(",", StringComparison.Ordinal);
        if(c > 0){
            s = s.Substring(0, s.Length - (s.Length - c));
        }

        return s;
    }

    /// <summary>
    /// 移除字符中的空格
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RemoveSpaces(this IStringFormat source) => ToProperCase(source.GetValue());

    private static string RemoveSpaces(string source){
        var s = source.Trim();
        return s.Replace(" ", string.Empty);
    }

    /// <summary>
    /// 字符串首字母大写
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToProperCase(this IStringFormat source) => ToProperCase(source.GetValue());

    /// <summary>
    /// 字符串首字母大写
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string ToProperCase(string source){
        var revised = string.Empty;
        if(source.Length <= 0){
            return revised;
        }

        var firstLetter = source.Substring(0, 1).ToUpper(new CultureInfo("en-US"));
        revised = firstLetter + source.Substring(1, source.Length - 1);
        return revised;
    }

    /// <summary>
    /// 大驼峰转下划线
    /// </summary>
    /// <param name="string"></param>
    /// <returns></returns> 
    public static string ToUnderLine(this IStringFormat source){
        var strItemTarget = ""; //目标字符串
        var s             = source.GetValue();
        foreach(var t in s){
            var temp = t.ToString();
            if(Regex.IsMatch(temp, "[A-Z]")){
                temp = "_" + temp.ToLower();
            }

            strItemTarget += temp;
        }

        return strItemTarget.Trim('_');
    }

    /// <summary>
    /// mysql数据库表名转换成C#大驼峰命名
    /// </summary>
    /// <example>database_informations  =>DatabaseInformation</example>
    /// <param name="string"></param>
    /// <returns></returns>
    public static string ToProperCaseFromUnderLine(this IStringFormat @string)=>
        @string.GetValue().Singularize(inputIsKnownToBePlural: false).Pascalize();
    
    /// <summary>
    /// 对象属性转下划线小写复数
    /// </summary>
    /// <example>database_informations  =>DatabaseInformation</example>
    /// <param name="string"></param>
    /// <returns></returns>
    public static string ToUnderLineFromProperCase(this IStringFormat @string) =>
        @string.GetValue().Underscore().Pluralize();
    
    /// <summary>
    /// 单词变成单数形式
    /// </summary>
    /// <param name="word">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToSingular(this IStringFormat word) => word.GetValue().Singularize();

    /// <summary>
    /// 单词变成复数形式
    /// </summary>
    /// <param name="word">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToPlural(this IStringFormat word) =>
         word.GetValue().Pluralize();
}
