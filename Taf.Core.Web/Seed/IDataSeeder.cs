// 何翔华
// Taf.Core.Web
// IDataSeeder.cs

using SqlSugar;

namespace Taf.Core.Web;

public interface IDataSeeder : ISingletonDependency{
    Task SeedAsync(ISqlSugarClient db);
}