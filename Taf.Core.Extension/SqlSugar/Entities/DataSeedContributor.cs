// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;
using System.Collections.Generic;

// 何翔华
// Taf.Core.Extension
// Migration.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// 数据库初始种子 
/// </summary>
[SugarTable("sys_dataSeedContributors", IsCreateTableFiledSort =true)]
public class DataSeedContributor : DbEntity{
    public DataSeedContributor(){ }

    [SugarColumn( ColumnDataType = "nvarchar(50)")]
    public string Key{ get; set; }
}
