// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TLString.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   双语字符串
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Taf.Core.Extension;

/// <summary>
/// 双语字段
/// </summary>
public class MlString{
    public MlString(string english, string local, string langKey){
        English = english;
        Local   = local;
        LangKey = langKey;
    }

    public MlString(string local,string langKey="zh-CN"){
        English = String.Empty;
        Local   = local;
        LangKey = langKey;
    }

    public MlString(){
        English = string.Empty;
        Local   = string.Empty;
        LangKey = "zh-CN";
    }

    /// <summary>
    /// 英语
    /// </summary>
    public string English{ get; set; }

    /// <summary>
    /// 本地语言
    /// </summary>
    public string Local{ get; set; }

    /// <summary>
    /// 本地语言代码
    /// </summary>
    public string LangKey{ get; set; }

    public string Current{
        get{
            var cultureName = Thread.CurrentThread.CurrentCulture.Name;
            if(string.IsNullOrWhiteSpace(cultureName)
            || cultureName.Length < 2
            || cultureName.Substring(0, 2).ToLower().Equals("en")){
                return string.IsNullOrEmpty(English) ? Local : English;
            }

            return string.IsNullOrEmpty(Local) ? English : Local;
        }
    }


    /// <summary>
    /// 得到空对象
    /// </summary>
    public static MlString Empty => new MlString(string.Empty);

    public override string ToString() => Current;
}

