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
// IPagedResultRequest.cs

namespace Taf.Core.Web;

/// <summary>
/// This interface is defined to standardize to request a paged result.
/// </summary>
public interface IPagedResultRequest : ILimitedResultRequest{
    /// <summary>
    /// Skip count (beginning of the page).
    /// </summary>
    int PageIndex{ get; set; }
}
