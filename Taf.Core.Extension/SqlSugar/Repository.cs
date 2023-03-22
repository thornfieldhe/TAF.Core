// 何翔华
// Taf.Core.Extension
// Repository.cs

using MapsterMapper;
using SqlSugar;
using System.Linq.Expressions;
using Taf.Core.Utility;

namespace Taf.Core.Extension;

/// <summary>
///基础仓储 
/// </summary>
public class Repository<T> : IRepository<T> where T : DbEntity, new(){
    protected readonly ISqlSugarClient Db;
    protected readonly IMapper         Mapper;

    public Repository(ISqlSugarClient db, IMapper mapper){
        Db     = db;
        Mapper = mapper;
    }

    public ISqlSugarClient GetDbContex() => Db;

#region query

    public virtual async Task<TR> FindAsync<TR>(Guid id){
        var result = await Db.Queryable<T>().InSingleAsync(id);
        if(result == null){
            return default;
        }

        if(result is TR o){
            return o;
        }

        return Mapper.Map<TR>(result);
    }

    public virtual async Task<List<TR>> GetAllListAsync<TR>(Expression<Func<T, bool>> whereExpression) =>
        (await Db.Queryable<T>().Where(whereExpression).ToListAsync()).Select(r => Mapper.Map<TR>(r)).ToList();

    public virtual ISugarQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> whereExpression) =>
        Db.Queryable<T>().Where(whereExpression);

    /// <summary>
    /// 使用Map映射Dto对象
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <typeparam name="TR"></typeparam>
    /// <returns></returns>
    public virtual async Task<TR> FirstOrDefaultAsync<TR>(Expression<Func<T, bool>> whereExpression){
        var result = await Db.Queryable<T>().FirstAsync(whereExpression);
        if(result == null){
            return default;
        }

        if(result is TR o){
            return o;
        }

        return Mapper.Map<TR>(result);
    }

    public virtual async Task<PagedResultDto<TR>> Page<TR>(
        PagedAndSortedResultRequestDto query
      , Expression<Func<T, bool>>      whereExpression){
        RefAsync<int> total = 0;
        var orderByFileName =
            Db.EntityMaintenance.GetDbColumnName<T>(string.IsNullOrWhiteSpace(query.Sorting)
                                                        ? "CreationTime"
                                                        : query.Sorting);
        var isAsc = false;
        query.Asc.HasValue.IfTrue(() => isAsc = query.Asc.Value);
        var list = (await Db.Queryable<T>().Where(whereExpression)
                            .OrderBy(orderByFileName + $"{(isAsc ? "" : " desc ")}")
                            .ToPageListAsync(query.PageIndex, query.PageSize, total))
                  .Select(r => Mapper.Map<TR>(r)).ToList();

        return new PagedResultDto<TR>(total, list);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression) =>
        await Db.Queryable<T>().CountAsync(whereExpression);

#endregion

#region insert

    public virtual async Task<bool> InsertAsync(T item) => await Db.Insertable(item).ExecuteCommandAsync()==1;

#endregion

#region update

    public virtual async Task<bool> UpdateAsync(T item){
        var concurrencyStamp = item.ConcurrencyStamp;
        return await Db.Updateable(item)
                        .Where(i => i.Id == item.Id && i.ConcurrencyStamp == concurrencyStamp)
                        .ExecuteCommandAsync()==1;
    }

#endregion

#region save

    public virtual async Task<bool> SaveAsync<TR>(TR item) where TR : IDto{
        T data;
        if(item.Id.HasValue){
            data = await FindAsync<T>(item.Id.Value);
            if(data == null){
                throw new EntityNotFoundException($"entity is not found in database,entityId:{item.Id}", typeof(T)
                                                , new Guid("D62AAAAF-48D9-4BC0-868F-E5123115A39F"));
            }

            Mapper.Map<TR, T>(item, data);
            return await UpdateAsync(data);
        } else{
            data = new T();
            Mapper.Map<TR, T>(item, data);
            return await InsertAsync(data);
        }
    }

#endregion

#region delete

    public virtual async Task DeleteAllAsync(Expression<Func<T, bool>> whereExpression) =>
        await Db.Deleteable<T>().Where(whereExpression).ExcuteDeleteAsync<T>();

    public async Task<bool> DeleteAsync(Guid id) => await Db.Deleteable<T>().In(id).ExcuteDeleteAsync<T>();

    public async Task<bool> DeleteAsync(T item) => await DeleteAsync(item.Id);

#endregion
}
