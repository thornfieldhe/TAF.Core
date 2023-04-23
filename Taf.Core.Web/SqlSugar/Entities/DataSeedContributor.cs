// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;

// 何翔华
// Taf.Core.Extension
// Migration.cs

namespace Taf.Core.Web;

/// <summary>
/// 数据库初始种子 
/// </summary>
[SugarTable("sys_data_seed_contributors")]
public class DataSeedContributor : DbEntity{
    [SugarColumn( ColumnDataType = "nvarchar(50)")]
    public string Key{ get; set; }
}
