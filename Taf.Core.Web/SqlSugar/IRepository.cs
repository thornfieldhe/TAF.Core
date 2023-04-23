// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRepository.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;
using System.Linq.Expressions;

// 何翔华
// Taf.Core.Net.Utility
// DefaultRepository.cs

namespace Taf.Core.Web;

public interface IRepository<T> where T : DbEntity{
    ISqlSugarClient GetDbContex();

    Task<TR> FindAsync<TR>(Guid id);

    Task<List<TR>> GetAllListAsync<TR>(Expression<Func<T, bool>> whereExpression);

    ISugarQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> whereExpression);

    Task<TR>  FirstOrDefaultAsync<TR>(Expression<Func<T, bool>> whereExpression);
    Task<int> CountAsync(Expression<Func<T, bool>>              whereExpression);

    Task<bool> InsertAsync(T                            item);
    Task<bool> UpdateAsync(T                            item);
    Task<bool> SaveAsync<TR>(TR                         item) where TR : IDto;
    Task<bool> DeleteAsync(T                            item);
    Task<bool> DeleteAsync(Guid                         id);
    Task       DeleteAllAsync(Expression<Func<T, bool>> whereExpression);


    Task<PagedResultDto<TR>> Page<TR>(PagedAndSortedResultRequestDto query, Expression<Func<T, bool>> whereExpression);
}