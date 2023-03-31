// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemConfig.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;
using System.Collections.Generic;
using System.Text.Json;

// 何翔华
// Taf.Core.Extension
// SystemConfig.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// 系统配置
/// </summary>
[SugarTable("sys_system_configs")]
public class SystemConfig : DbEntity{
    /// <summary>
    /// 关键字
    /// </summary>
    [SugarColumn( ColumnDataType = "nvarchar(50)")]
    public string Key{ get;  set; }
    
    /// <summary>
    /// 配置序列化对象
    /// </summary>
    [SugarColumn( ColumnDataType = "nvarchar(2000)")]
    public string Data{ get; set; }
    
    public T GetData<T>() => JsonSerializer.Deserialize<T>(Data);
    
    public SystemConfig SetData(object data){
        Data = JsonSerializer.Serialize(data);
        return this;
    }
}
