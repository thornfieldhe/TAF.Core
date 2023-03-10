// 何翔华
// Taf.Core.Web
// DefaultDataSeedContributor.cs

using SqlSugar;
using Taf.Core.Extension;

namespace Taf.Core.Web;

public abstract class DefaultDataSeedContributor : IDataSeedContributor{
    private readonly   ILogger         _logger;
    protected readonly ISqlSugarClient _sqlSugarClient;

    protected DefaultDataSeedContributor(ILogger logger, ISqlSugarClient sqlSugarClient){
        _logger         = logger;
        _sqlSugarClient = sqlSugarClient;
    }

    private string _key => GetType().Name;

    protected abstract void Excute();

    public async Task SeedAsync(){
        try{
            if(!await _sqlSugarClient.Queryable<DataSeedContributor>().AnyAsync(r => r.Key == _key.Trim().ToLower())){
                Excute();
                _sqlSugarClient.Insertable<DataSeedContributor>(new DataSeedContributor{ Key = _key })
                               .ExecuteCommandAsync();
            }
        } catch(Exception e){ }
    }
}
