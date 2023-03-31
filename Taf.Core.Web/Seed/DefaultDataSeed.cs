// 何翔华
// Taf.Core.Web
// DefaultDataSeedContributor.cs

using Taf.Core.Extension;

namespace Taf.Core.Web;

public abstract class DefaultDataSeed : IDataSeedContributor{
    public             string Key => GetType().Name;
    protected abstract void   Excute(TafDbContext dbContext);

    public void Seed(TafDbContext dbContext){
        Excute(dbContext);
        dbContext.DataSeedContributors.InsertAsync(new DataSeedContributor{ Key = Key }).GetAwaiter();
    }
}
