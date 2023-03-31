// 何翔华
// Taf.Core.Web
// RegisterExt.cs

using Taf.Core.Extension;

namespace Taf.Core.Web;

public static class RegisterExt{
    /// <summary>
    /// 初始化系统,获取DbEntity对象和IDataSeedContributor对象
    /// </summary>
    /// <param name="allTypes"></param>
    /// <returns></returns>
    public static (Type[] DbEntityTypes, List<IDataSeedContributor> DataSeedContributors) RegisterTypes(
        this WebApplicationBuilder builder, params Type[] allTypes){
        var DbEntityTypes        = new List<Type>();
        var DataSeedContributors = new List<IDataSeedContributor>();
        foreach(var classType in allTypes){
            foreach(var type in classType.Assembly.GetTypes()){
                if(typeof(DbEntity).IsAssignableFrom(type)
                && !type.IsAbstract){
                    DbEntityTypes.Add(type); //数据库对象
                } else if(typeof(IDataSeedContributor).IsAssignableFrom(type) && !type.IsAbstract){
                    DataSeedContributors.Add(Activator.CreateInstance(type) as IDataSeedContributor); // 种子对象
                }
            }
        }

        return (DbEntityTypes.ToArray(), DataSeedContributors);
    }
}
