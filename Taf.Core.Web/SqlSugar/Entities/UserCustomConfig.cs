// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemConfig.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;

// 何翔华
// Taf.Core.Extension
// SystemConfig.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 系统配置
/// </summary>
[SugarTable("sys_user_configs")]
public class UserCustomConfig : DbEntity{
    /// <summary>
    /// 关键字
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string Key{ get; set; }


    /// <summary>
    /// 配置序列化对象
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string Data{ get; set; }


    /// <summary>
    /// 
    /// </summary>
    public Guid UserId{ get; set; }
}
