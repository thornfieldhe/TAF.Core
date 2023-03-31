// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_String_StringReg.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Coding4Fun.PluralizationServices;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// 何翔华
// Taf.Core.Utility
// Extensions.String.StringReg.cs

namespace Taf.Core.Utility;

/// <summary>
/// 字符串正则操作
/// </summary>
public interface IStringReg : IExtension<string>{ }

/// <summary>
/// $Summary$
/// </summary>
public static class StringReg{
     /// <summary>
    /// 截取字符串中开始和结束字符串中间的字符串
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="startStr">开始字符串</param>
    /// <param name="endStr">结束字符串</param>
    /// <returns>中间字符串</returns>
    public static string Substring(this IStringReg source, string startStr, string endStr){
        var rg = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))"
                         , RegexOptions.Multiline | RegexOptions.Singleline);
        return rg.Match(source.GetValue()).Value;
    }

    /// <summary>
    /// 验证输入与模式是否匹配
    /// </summary>
    /// <param name="input">
    /// 输入字符串
    /// </param>
    /// <param name="pattern">
    /// 模式字符串
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsMatch(this IStringReg input, string pattern) => IsMatch(input, pattern, RegexOptions.IgnoreCase);

    /// <summary>
    /// 验证输入与模式是否匹配
    /// </summary>
    /// <param name="input">
    /// 输入的字符串
    /// </param>
    /// <param name="pattern">
    /// 模式字符串
    /// </param>
    /// <param name="options">
    /// 筛选条件,比如是否忽略大小写
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsMatch(this IStringReg input, string pattern, RegexOptions options) =>
        Regex.IsMatch(input.GetValue(), pattern, options);

    /// <summary>
    /// 替换最后一个匹配的字符串
    /// in this instance are replaced with another specified string.  
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <param name="oldValue">
    /// </param>
    /// <param name="newValue">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceLast(this IStringReg @this, string oldValue, string newValue){
        var s     = @this.GetValue();
        var index = s.LastIndexOf(oldValue);
        if(index > -1){
            var newString = s.Remove(index, oldValue.Length).Insert(index, newValue);
            return newString;
        }

        return s;
    }

    /// <summary>
    /// 子字符串出现次数
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <param name="match">
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int CountOccurences(this IStringReg @this, string match){
        if(!match.IsNullOrEmpty()){
            var s     = @this.GetValue();
            var count = (s.Length - s.Replace(match, string.Empty).Length) / match.Length;
            return count;
        }

        return 0;
    }

    /// <summary>
    /// 替换第一个匹配的字符串
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <param name="oldValue">
    /// </param>
    /// <param name="newValue">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceFirst(this IStringReg @this, string oldValue, string newValue){
        var s     = @this.GetValue();
        var index = s.IndexOf(oldValue);
        if(index > -1){
            var newString = s.Remove(index, oldValue.Length).Insert(index, newValue);
            return newString;
        }

        return s;
    }

    /// <summary>
    /// 查询字符串匹配的子串
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<string> FindSubstringAsString(
        this IStringReg strText,
        string      matchPattern,
        bool        findAllUnique = true){
        var matchArry = FindSubstring(strText.GetValue(), matchPattern, findAllUnique);
        var retArry   = from match in matchArry select match.Value;
        return retArry.ToList();
    }

    /// <summary>
    /// 查询字符串匹配的子串
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="groupId">
    /// 分组Id
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<string> FindSubstringAsString(
        this IStringReg strText,
        string      matchPattern,
        int         groupId,
        bool        findAllUnique){
        var matchArry = FindSubstring(strText.GetValue(), matchPattern, findAllUnique);
        var retArry   = from match in matchArry select match.Groups[groupId].Value;
        return retArry.ToList();
    }

    /// <summary>
    /// 查询字符串匹配的数字
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<int> FindSubstringAsSInt(this IStringReg strText, string matchPattern, bool findAllUnique = true){
        var matchArry = FindSubstring(strText.GetValue(), matchPattern, findAllUnique);
        var retArry   = from match in matchArry select int.Parse(match.Value);
        return retArry.ToList();
    }

    /// <summary>
    /// 查询字符串匹配的小数
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<double> FindSubstringAsDouble(this IStringReg strText, string matchPattern, bool findAllUnique){
        var matchArry = FindSubstring(strText.GetValue(), matchPattern, findAllUnique);
        var retArry   = from match in matchArry select double.Parse(match.Value);
        return retArry.ToList();
    }

    /// <summary>
    /// 查询字符串匹配的Decimal
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<decimal> FindSubstringAsDecimal(this IStringReg strText, string matchPattern, bool findAllUnique){
        var matchArry = FindSubstring(strText.GetValue(), matchPattern, findAllUnique);
        var retArry   = from match in matchArry select decimal.Parse(match.Value);
        return retArry.ToList();
    }

    /// <summary>
    /// 正则表达式替换分组内内容
    /// </summary>
    /// <param name="strText">
    /// 字符串源
    /// </param>
    /// <param name="pattern">
    /// 匹配正则式
    /// </param>
    /// <param name="target">
    /// 替换后内容
    /// </param>
    /// <param name="groupId">
    /// 分组Id
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceReg(this IStringReg strText, string pattern, string target, int groupId){
        var myEvaluator = new MatchEvaluator(match => CustomReplace(match, groupId, target));
        var reg         = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return reg.Replace(strText.GetValue(), myEvaluator);
    }

    /// <summary>
    /// 单词变成单数形式
    /// </summary>
    /// <param name="word">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToSingular(this IStringReg word){
        var s      = word.GetValue();
        var server = PluralizationService.CreateService(new CultureInfo("en"));
        return server.IsPlural(s) ? server.Singularize(s) : s;
    }

    /// <summary>
    /// 单词变成复数形式
    /// </summary>
    /// <param name="word">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToPlural(this IStringReg word){
        var s      = word.GetValue();
        var server = PluralizationService.CreateService(new CultureInfo("en"));
        return server.IsPlural(s) ? s : server.Pluralize(s);
    }

    /// <summary>
    /// The custom replace.
    /// </summary>
    /// <param name="m">
    /// The m.
    /// </param>
    /// <param name="groupId">
    /// The group id.
    /// </param>
    /// <param name="target">
    /// The target.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string CustomReplace(Match m, int groupId, string target){
        var index  = m.Groups[groupId].Index;
        var length = m.Groups[groupId].Length;
        return m.Value.Substring(m.Index - m.Index, index - m.Index)
             + target
             + m.Value.Substring(index + length - m.Index, m.Index + m.Length - (index + length));
    }

    /// <summary>
    /// 查询字符串匹配的子串
    /// </summary>
    /// <param name="strText">
    /// 目标字符串
    /// </param>
    /// <param name="matchPattern">
    /// 匹配文本
    /// </param>
    /// <param name="findAllUnique">
    /// 是否返回不重复的匹配
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    private static IEnumerable<Match> FindSubstring(string strText, string matchPattern, bool findAllUnique){
        var     uniqueMatches = new SortedList();
        Match[] retArry       = null;
        var     re            = new Regex(matchPattern, RegexOptions.Multiline);
        var     theMatches    = re.Matches(strText);
        if(findAllUnique){
            for(var counter = 0; counter < theMatches.Count; counter++){
                if(!uniqueMatches.ContainsKey(theMatches[counter].Value)){
                    uniqueMatches.Add(theMatches[counter].Value, theMatches[counter]);
                }
            }

            retArry = new Match[uniqueMatches.Count];
            uniqueMatches.Values.CopyTo(retArry, 0);
        } else{
            retArry = new Match[theMatches.Count];
            theMatches.CopyTo(retArry, 0);
        }

        return retArry;
    }
}
