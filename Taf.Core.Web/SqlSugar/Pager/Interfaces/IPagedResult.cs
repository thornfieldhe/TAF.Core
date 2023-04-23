// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CLASS.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Net.Utility
// IPagedResult.cs

namespace Taf.Core.Web;

/// <summary>
/// This interface is defined to standardize to return a page of items to clients.
/// </summary>
/// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items"/> list</typeparam>
public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
{

}
