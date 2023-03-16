// --------------------------------------------------------------------------------------------------------------------
// <copyright file="$CLASS$.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taf.Core.Extension;

// 何翔华
// Taf.Core.Extension
// IDataSeedContributor.cs

namespace Taf.Core.Web;

/// <summary>
/// 种子生产接口
/// </summary>
public interface IDataSeedContributor{
    void Seed(TafDbContext dbContext);

    string Key{ get; }
}
