// --------------------------------------------------------------------------------------------------------------------
// <copyright file="$CLASS$.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



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
