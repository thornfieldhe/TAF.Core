// 何翔华
// Taf.Core.Extension
// IHasModificationTime.cs

namespace Taf.Core.Web;

/// <summary>
/// An entity can implement this interface if <see cref="LastModificationTime"/> of this entity must be stored.
/// <see cref="LastModificationTime"/> is automatically set when updating <see cref="Entity"/>.
/// </summary>
public interface IHasModificationTime:IEntity
{
    /// <summary>
    /// The last modified time for this entity.
    /// </summary>
    DateTime? LastModificationTime{ get; set; }
}
