// 何翔华
// Taf.Core.Web
// DataSeeder.cs

using SqlSugar;
using Taf.Core.Extension;

namespace Taf.Core.Web;

public static class DataSeederExt{
    
    /// <summary>
    /// 初始化数据库和数据脚本
    /// </summary>
    /// <param name="application"></param>
    /// <param name="entityTypes"></param>
    /// <param name="contributors"></param>
    /// <param name="dbName"></param>
    /// <param name="isDisabledUpdateAll"></param>
    /// <typeparam name="DbContex"></typeparam>
    public static void Seed<DbContex>(
        this WebApplication        application
      , List<Type>                 entityTypes
      , List<IDataSeedContributor> contributors
      , string                     dbName              = "MainConnection"
      , bool                       isDisabledUpdateAll = false)
        where DbContex : TafDbContext, new(){
        var connection = application.Configuration.GetConnectionString(dbName);
        SqlSugarConfigure.InitDatabase(connection, entityTypes, isDisabledUpdateAll);
        var unitOfWork = application.Services.GetService<ISugarUnitOfWork<DbContex>>();
        var dbContext  = unitOfWork.CreateContext();
        var keys       = dbContext.DataSeedContributors.GetAllAsQueryable(s => true).Select(r => r.Key).ToArray();
        foreach(var contributor in contributors){
            if(!keys.Contains(contributor.Key)){
                contributor.Seed(dbContext);
            }
        }

        dbContext.Commit(); //使用事务一定要记得写提交
    }
}
