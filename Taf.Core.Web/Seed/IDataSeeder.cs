// 何翔华
// Taf.Core.Web
// IDataSeeder.cs

using SqlSugar;
using Taf.Core.Extension;

namespace Taf.Core.Web;

public interface IDataSeeder:ISingletonDependency{
    Task SeedAsync(ISqlSugarClient db);
}
public class DataSeeder : IDataSeeder{
    private List<IDataSeedContributor> _contributors = new();

    public void AddSeeder(IDataSeedContributor contributor){
        _contributors.Add(contributor);
    }

    public async Task SeedAsync(ISqlSugarClient db){
        var allContributors = db.Queryable<DataSeedContributor>().Select(s => s.Key).ToList();
        foreach(var contributor in _contributors){
            await contributor.SeedAsync();
        }
    }
}