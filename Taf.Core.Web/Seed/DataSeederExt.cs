// 何翔华
// Taf.Core.Web
// DataSeeder.cs

using SqlSugar;
using System.Reflection;
using Taf.Core.Extension;
using Taf.Core.Utility;

namespace Taf.Core.Web;

public static class DataSeederExt{
    public static void SeedAsync(
        this WebApplication app, TafDbContext dbContext, List<IDataSeedContributor> contributors){
        var keys =  dbContext.DataSeedContributors.GetAllAsQueryable(s => true).Select(r => r.Key).ToArray();
        foreach(var contributor in contributors){
            if(!keys.Contains(contributor.Key)){
                 contributor.Seed(dbContext);
            }
        }

        dbContext.Commit(); //使用事务一定要记得写提交
    }
}