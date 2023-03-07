// 何翔华
// Taf.Core.Net.Utility
// IPassivable.cs

using System.Data.SqlTypes;

namespace Taf.Core.Extension;

/// <summary>
/// This interface is used to make an entity active/passive.
/// </summary>
public interface IPassivable
{
    /// <summary>
    /// True: This entity is active.
    /// False: This entity is not active.
    /// </summary>
    bool IsActive{ get; set; }
}
