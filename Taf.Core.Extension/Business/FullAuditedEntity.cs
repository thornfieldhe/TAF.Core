// 何翔华
// Taf.Core.Extension
// FullAuditedEntity.cs

namespace Taf.Core.Extension;

/// <summary>
/// A shortcut of <see cref="FullAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class FullAuditedEntity : FullAuditedEntity<Guid>, IEntity
{

}


/// <summary>
/// Implements <see cref="IFullAudited"/> to be a base class for full-audited entities.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAuditedEntity
{
    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted{ get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual long? DeleterUserId{ get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime{ get; set; }
}

