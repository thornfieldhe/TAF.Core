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
// IHasTotalCount.cs

namespace Taf.Core.Web;

/// <summary>
/// This interface is defined to standardize to set "Total Count of Items" to a DTO.
/// </summary>
public interface IHasTotalCount
{
    /// <summary>
    /// Total count of Items.
    /// </summary>
    int TotalCount{ get; set; }
}