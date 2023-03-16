// 何翔华
// Taf.Core.Web
// DefaultDataSeedContributor.cs

using SqlSugar;
using Taf.Core.Extension;

namespace Taf.Core.Web;

public abstract class DefaultDataSeedContributor : IDataSeedContributor{
    public             string Key => GetType().Name;
    protected abstract void   Excute();

    public void Seed(TafDbContext dbContext){
        Excute();
        dbContext.DataSeedContributors.Insert(new DataSeedContributor{ Key = Key });
    }
}
