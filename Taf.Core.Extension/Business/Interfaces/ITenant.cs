// 何翔华
// Taf.Core.Extension
// IMustHaveTenant.cs

namespace Taf.Core.Extension;

/// <summary>
/// Implement this interface for an entity which must have TenantId.
/// </summary>
public interface ITenant
{
    /// <summary>
    /// TenantId of this entity.
    /// </summary>
    int TenantId{ get; set; }
}
