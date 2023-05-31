// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions_Xml.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

// 何翔华
// Taf.Core.Utility
// Extensions.Xml.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
/// 简化Linq获取Xml节点数据
/// </summary>
public partial  class Extensions{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Guid AttributeToGuid(this XElement node, string name) =>
        node.Attribute(name)?.Value.ToGuid() ?? Guid.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static int AttributeToInt(this XElement node, string name) => node.Attribute(name)?.Value.ToInt() ?? 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static double AttributeToDouble(this XElement node, string name) =>
        node.Attribute(name)?.Value.ToDouble() ?? 0D;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string AttributeToString(this XElement node, string name) => node.Attribute(name)?.Value ?? "";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DateTime AttributeToDateTime(this XElement node, string name) =>
        node.Attribute(name)?.Value.ToDate() ?? new DateTime();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DateOnly AttributeToDateOnly(this XElement node, string name){
        DateOnly.TryParse(node.Attribute(name)?.Value, out var date);
        return date;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool AttributeToBool(this XElement node, string name) =>
        node.Attribute(name)?.Value.ToBool() ?? false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string? FirstOrDefaultDescendantValue(this XElement node, string name) =>
        node.Descendants().FirstOrDefault(r => r.Name.LocalName == name)?.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static XElement? FirstOrDefaultDescendant(this XElement node, string name) =>
        node.Descendants().FirstOrDefault(r => r.Name.LocalName == name); 
}
