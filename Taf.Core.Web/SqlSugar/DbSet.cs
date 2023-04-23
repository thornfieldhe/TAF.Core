// 何翔华
// Taf.Core.Extension
// DbSet.cs

using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Linq.Expressions;

namespace Taf.Core.Web;

/// <summary>
/// 自定义仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class DbSet<T> : SimpleClient<T>, IRepository<T> where T : DbEntity, new(){
    protected readonly IMapper Mapper;



    public DbSet() => Mapper = ServiceLocator.Instance.ServiceProvider.GetService<IMapper>();

#region query

    public ISqlSugarClient GetDbContex() => Context;

    public ISugarQueryable<T> Queryable => Context.Queryable<T>();

    public virtual async Task<TR> FindAsync<TR>(Guid id){
        var result = await Context.Queryable<T>().InSingleAsync(id);
        if(result == null){
            return default;
        }

        if(result is TR o){
            return o;
        }

        return Mapper.Map<TR>(result);
    }

    public virtual async Task<List<TR>> GetAllListAsync<TR>(Expression<Func<T, bool>> whereExpression) =>
        (await Context.Queryable<T>().Where(whereExpression).ToListAsync()).Select(r => Mapper.Map<TR>(r)).ToList();

    public virtual ISugarQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> whereExpression) =>
        Context.Queryable<T>().Where(whereExpression);

    /// <summary>
    /// 使用Map映射Dto对象
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <typeparam name="TR"></typeparam>
    /// <returns></returns>
    public virtual async Task<TR> FirstOrDefaultAsync<TR>(Expression<Func<T, bool>> whereExpression){
        var result = await Context.Queryable<T>().FirstAsync(whereExpression);
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
            Context.EntityMaintenance.GetDbColumnName<T>(string.IsNullOrWhiteSpace(query.Sorting)
                                                             ? "Id"
                                                             : query.Sorting);
        var isAsc = false;
        query.Asc.HasValue.IfTrue(() => isAsc = query.Asc.Value);
        var list = (await Context.Queryable<T>().Where(whereExpression)
                                 .OrderBy(orderByFileName + $"{(isAsc ? "" : " desc ")}")
                                 .ToPageListAsync(query.PageIndex, query.PageSize, total))
                  .Select(r => Mapper.Map<TR>(r)).ToList();

        return new PagedResultDto<TR>(total, list);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression) =>
        await Context.Queryable<T>().CountAsync(whereExpression);

#endregion

#region insert

    public virtual async Task<bool> InsertAsync(T item) => await Context.Insertable(item).ExecuteCommandAsync()==1;

#endregion

#region update

    public virtual async Task<bool> UpdateAsync(T item){
        var concurrencyStamp = item.ConcurrencyStamp;
        return await Context.Updateable(item).Where(i => i.Id == item.Id && i.ConcurrencyStamp == concurrencyStamp)
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

            Mapper.Map(item, data);
            return await UpdateAsync(data);
        }

        data = new T();
        Mapper.Map(item, data);
        return await InsertAsync(data);
    }

#endregion

#region delete

    public virtual async Task DeleteAllAsync(Expression<Func<T, bool>> whereExpression) =>
        await Context.Deleteable<T>().Where(whereExpression).ExcuteDeleteAsync();

    public async Task<bool> DeleteAsync(Guid id) => await Context.Deleteable<T>().In(id).ExcuteDeleteAsync();

    public async Task<bool> DeleteAsync(T item) => await DeleteAsync(item.Id);

#endregion
}
