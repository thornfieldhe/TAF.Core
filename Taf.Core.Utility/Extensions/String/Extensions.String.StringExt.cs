// --------------------------------------------------------------------------------------------------------------------
// <copyright file="$CLASS$.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// 何翔华
// Taf.Core.Utility
// Extensions.String.StringExt.cs

namespace Taf.Core.Utility;

/// <summary>
/// string 扩展方法
/// </summary>
public interface IStringExt : IExtension<string>{ }

/// <summary>
/// string的扩展方法
/// </summary>
public static class StringExt{

    /// <summary>
    /// 将字符串移除最后一个分隔符并转换为列表
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <param name="separator">
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<string> SplitToList(this IStringExt source, char separator = ','){
        var s = RemoveFinalChar(source.GetValue(),separator);
        return s.Split(separator).ToList();
    }

    /// <summary>
    /// 得到字符串长度，一个汉字长度为2
    /// </summary>
    /// <param name="inputString">
    /// 参数字符串
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int StrLength(this IStringExt inputString){
        var ascii   = new ASCIIEncoding();
        var tempLen = 0;
        var s       = ascii.GetBytes(inputString.GetValue());
        for(var i = 0; i < s.Length; i++){
            if(s[i] == 63){
                tempLen += 2;
            } else{
                tempLen += 1;
            }
        }

        return tempLen;
    }

    /// <summary>
    /// 如果字符串不为空则执行 
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <param name="action">
    /// </param>
    public static void IfIsNotNullOrEmpty(this IStringExt source, Action<string> action){
        var s = source.GetValue();
        if(!string.IsNullOrWhiteSpace(s)){
            action(s);
        }
    }

    /// <summary>
    /// 如果字符串为空则执行 
    /// </summary>
    /// <param name="source">
    /// </param>
    /// <param name="action">
    /// </param>
    public static void IfIsNullOrEmpty(this IStringExt source, Action action){
        if(string.IsNullOrWhiteSpace(source.GetValue())){
            action();
        }
    }

    /// <summary>
    /// 取左边n个字符串
    /// </summary>
    /// <param name="obj">
    /// </param>
    /// <param name="length">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Left(this IStringExt obj, int length) => obj.GetValue().Substring(0, length);

    /// <summary>
    /// 取右边n个字符串
    /// </summary>
    /// <param name="obj">
    /// </param>
    /// <param name="length">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Right(this IStringExt obj, int length){
        var s = obj.GetValue();
        return s.Substring(s.Length - length, length);
    } 

    /// <summary>
    /// 格式化字符串，是string.Format("",xx)的变体
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <param name="args">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatWith(this IStringExt @this, params object[] args) => string.Format(@this.GetValue(), args);

    /// <summary>
    /// 判断字符串是否相等，忽略字符情况
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <param name="compareOperand">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IgnoreCaseEqual(this IStringExt @this, string compareOperand) =>
        @this.GetValue().Equals(compareOperand, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 返回一个字符串用空格分隔如: thisIsGood =&gt; this Is Good
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Wordify(this IStringExt @this){
        var s = @this.GetValue();
        // if the word is all upper, just return it
        return !Regex.IsMatch(s, "[a-z]") ? s : string.Join(" ", Regex.Split(s, @"(?<!^)(?=[A-Z])"));
    }

    /// <summary>
    /// 翻转字符串
    /// </summary>
    /// <param name="this">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Reverse(this IStringExt @this){
        var s = @this.GetValue();
        if(s        == null
        || s.Length < 2){
            return s;
        }

        var length    = s.Length;
        var loop      = (length >> 1) + 1;
        var charArray = new char[length];
        for(var i = 0; i < loop; i++){
            var j = length - i - 1;
            charArray[i] = s[j];
            charArray[j] = s[i];
        }

        return new string(charArray);
    }

    /// <summary>
    /// 去除重复
    /// </summary>
    /// <param name="value">
    /// 值，范例1："5555",返回"5",范例2："4545",返回"45"
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Distinct(this IStringExt value){
        var s     = value.GetValue();
        var array = s.ToCharArray();
        return new string(array.Distinct().ToArray());
    }

    /// <summary>
    /// 截断字符串
    /// </summary>
    /// <param name="text">
    /// 文本
    /// </param>
    /// <param name="length">
    /// 返回长度
    /// </param>
    /// <param name="endCharCount">
    /// 添加结束符号的个数，默认0，不添加
    /// </param>
    /// <param name="endChar">
    /// 结束符号，默认为省略号
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Truncate(this IStringExt text, int length, int endCharCount = 0, string endChar = "."){
        var s = text.GetValue();
        if(string.IsNullOrWhiteSpace(s)){
            return string.Empty;
        }

        if(s.Length < length){
            return s;
        }

        return s.Substring(0, length) + GetEndString(endCharCount, endChar);
    }

    /// <summary>
    /// 是否包含中文
    /// </summary>
    /// <param name="text">
    /// 文本
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ContainsChinese(this IStringExt text){
        const string pattern = "[\u4e00-\u9fa5]+";
        return Regex.IsMatch(text.GetValue(), pattern);
    }

    /// <summary>
    /// 是否包含数字
    /// </summary>
    /// <param name="text">
    /// 文本
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ContainsNumber(this IStringExt text){
        const string pattern = "[0-9]+";
        return Regex.IsMatch(text.GetValue(), pattern);
    }

    /// <summary>
    /// 指定字符串是否在集合中
    /// </summary>
    /// <param name="str">
    /// 字符串("C")
    /// </param>
    /// <param name="stringList">
    /// 字符串("A,B,C,D,E")
    /// </param>
    /// <param name="separator">
    /// 分隔符
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsInArryString(this IStringExt str, string stringList, char separator){
        var list = stringList.Split(separator);
        return list.Any(t => t.Equals(str.GetValue()));
    }

    /// <summary>
    /// 重复字符串输出
    /// </summary>
    /// <param name="this">
    /// The this.
    /// </param>
    /// <param name="times">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Repeat(this IStringExt @this, int times){
        var repeat = @this.GetValue();
        for(var i = 0; i < times; i++){
            repeat += repeat;
        }

        return repeat;
    }

    /// <summary>
    /// 移除末尾字符串
    /// </summary>
    /// <param name="this"></param>
    /// <param name="targetStr"></param>
    /// <returns></returns>
    public static string RemoveLastString(this IStringExt @this, string targetStr){
        var repeat = @this.GetValue();
        if(repeat.EndsWith(targetStr)
        && repeat.Length > 1){
            repeat = repeat.Substring(0, repeat.Length - targetStr.Length);
        }

        return repeat;
    }

    /// <summary>
    /// 移除起始字符串
    /// </summary>
    /// <param name="this"></param>
    /// <param name="targetStr"></param>
    /// <returns></returns>
    public static string RemoveStartString(this IStringExt @this, string targetStr){
        var repeat = @this.GetValue();
        if(repeat.StartsWith(targetStr)
        && repeat.Length > 1){
            repeat = repeat.Substring(targetStr.Length, repeat.Length - targetStr.Length);
        }

        return repeat;
    }

    /// <summary>
    /// ToString并且去掉空格
    /// </summary>
    /// <param name="v">v为空时,不抛错,并返回空字符</param>
    /// <returns></returns>
    public static string ToStringAndTrim(this object v) => v == null ? string.Empty : v.ToString().Trim();

    /// <summary>
    /// 生成短链码
    /// </summary>
    /// <param name="longStr"></param>
    /// <returns></returns>
    public static string GetShortCode(this IStringExt longStr){
        //生成逻辑:
        //1.将长链字符串 + 时间戳,进行32位的Md5加密(降低短链码重复的概率);
        //2.将32位的Md5值,分为4组,每组都和16进制的3fffffff进行位与运算.
        //3.每组数据又和16进制0000003D进行6次位运算.
        //4.将6次位运算获得的索引,去chars中匹配一个字符,最终得到一组短链码.
        //5.总共生成4组短链码,随机返回一组即可.
        var chars = new[]{
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v"
          , "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H"
          , "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
        var ticks  = DateTime.Now.Ticks;
        var hex    = Encrypt.Md5By16(longStr.GetValue() + ticks);
        var resUrl = new string[4];
        for(int i = 0; i < 4; i++){
            var sTempSubString = hex.Substring(i * 8, 8);
            var lHexLong       = 0x3FFFFFFF & Convert.ToInt64(sTempSubString, 16);
            var outChars       = "";
            for(int j = 0; j < 6; j++){
                var index = 0x0000003D & lHexLong;
                outChars += chars[(int)index];
                lHexLong =  lHexLong >> 5;
            }

            resUrl[i] = outChars;
        }

        var rd = new Random();
        return resUrl[rd.Next(0, 4)];
    }
 
    /// <summary>
    /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
    /// </summary>
    /// <param name="source">
    /// 要转换的值,即原值
    /// </param>
    /// <param name="from">
    /// 原值的进制,只能是2,8,10,16四个值。
    /// </param>
    /// <param name="to">
    /// 要转换到的目标进制，只能是2,8,10,16四个值。
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ConvertBase(this IStringExt source, int from, int to){
        try{
            var value    = source.GetValue(); 
            var intValue = Convert.ToInt32(value, from);  // 先转成10进制
            var result   = Convert.ToString(intValue, to); // 再转成目标进制
            if(to != 2){
                return result;
            }

            var resultLength = result.Length; // 获取二进制的长度
            switch(resultLength){
                case 7:
                    result = "0" + result;
                    break;
                case 6:
                    result = "00" + result;
                    break;
                case 5:
                    result = "000" + result;
                    break;
                case 4:
                    result = "0000" + result;
                    break;
                case 3:
                    result = "00000" + result;
                    break;
            }

            return result;
        } catch{
            return "0";
        }
    }

    /// <summary>
    /// 获取字符串数组里最后一个Item
    /// </summary>
    /// <param name="propertyName">
    /// 属性名，范例，A.B.C,返回"C"
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetLastProperty(this IStringExt source){
        var propertyName = source.GetValue();
        if(string.IsNullOrWhiteSpace(propertyName)){
            return string.Empty;
        }

        var lastIndex = propertyName.LastIndexOf(".", StringComparison.Ordinal) + 1;
        return propertyName.Substring(lastIndex);
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
    private static string RemoveFinalChar( string source, char separator){
        if(source.EndsWith(separator.ToString(), StringComparison.Ordinal)
        && source.Length > 1){
            source = source.Substring(0, source.Length - 1);
        }

        return source;
    }

    /// <summary>
    /// 获取结束字符串
    /// </summary>
    /// <param name="endCharCount">
    /// The end Char Count.
    /// </param>
    /// <param name="endChar">
    /// The end Char.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string GetEndString(int endCharCount, string endChar){
        var result = new StringBuilder();
        for(var i = 0; i < endCharCount; i++){
            result.Append(endChar);
        }

        return result.ToString();
    }
}
