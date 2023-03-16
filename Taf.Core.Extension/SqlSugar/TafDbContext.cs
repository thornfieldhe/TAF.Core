// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TafDbContext.cs" company="" author="何翔华">
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
// TafDbContext.cs

namespace Taf.Core.Extension;

/// <summary>
/// 基类DbContext
/// </summary>
public class TafDbContext : SugarUnitOfWork{
    public TafDbContext(){ }
    
    public DbSet<DataSeedContributor> DataSeedContributors{ get; set; }
    
} 