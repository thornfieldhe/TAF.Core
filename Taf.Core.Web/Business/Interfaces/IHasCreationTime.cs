// 何翔华
// Taf.Core.Extension
// IHasCreationTime.cs

namespace Taf.Core.Web;

/// <summary>
/// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
/// <see cref="CreationTime"/> is automatically set when saving <see cref="Entity"/> to database.
/// </summary>
public interface IHasCreationTime:IEntity
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreationTime{ get; set; }
}
