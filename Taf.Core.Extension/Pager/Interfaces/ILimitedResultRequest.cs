// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CLASS.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   This interface is defined to standardize to request a limited result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Net.Utility
// ILimitedResultRequest.cs

namespace Taf.Core.Extension;

/// <summary>
/// This interface is defined to standardize to request a limited result.
/// </summary>
public interface ILimitedResultRequest{
    /// <summary>
    /// Maximum result count should be returned.
    /// This is generally used to limit result count on paging.
    /// </summary>
    int PageSize{ get; set; } 
}
