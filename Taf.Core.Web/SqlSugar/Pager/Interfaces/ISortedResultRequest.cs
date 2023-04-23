// 何翔华
// Taf.Core.Net.Utility
// ISortedResultRequest.cs

namespace Taf.Core.Web;

/// <summary>
/// This interface is defined to standardize to request a sorted result.
/// </summary>
public interface ISortedResultRequest
{
    /// <summary>
    /// Sorting information.
    /// </summary>
    string Sorting{ get; set; }
    
    
    /// <summary>
    /// Asc=true   DESC=false  Defalue=DESC
    /// </summary>
    bool? Asc{ get; set; }
}
