// 何翔华
// Taf.Core.Extension
// SqlSugarRepositoryExtend.cs

using SqlSugar;

namespace Taf.Core.Web;

public static class SqlSugarRepositoryExtend{
    
    /// <summary>
    /// 扩展删除方法,同时支持软删除和直接删除
    /// </summary>
    /// <param name="deleteable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<bool> ExcuteDeleteAsync<T>(this IDeleteable<T> deleteable) where T : DbEntity, new(){
        var result = 0;
        if(typeof(ISoftDelete).IsAssignableFrom(typeof(T))){
            result = await deleteable.IsLogic()
                                     .ExecuteCommandAsync(nameof(ISoftDelete.IsDeleted), true
                                                        , nameof(ISoftDelete.DeletionTime));
        } else{
            result = await deleteable.ExecuteCommandAsync();
        }

        return result > 0;
    }
}
