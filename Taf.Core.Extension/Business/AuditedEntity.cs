// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuditedEntity.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Extension
// AuditedEntity.cs

namespace Taf.Core.Extension;

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class AuditedEntity : DbEntity, IAuditedEntity
{
    
    protected AuditedEntity() => CreationTime = DateTime.UtcNow;
    /// <summary>
    /// Last modification date of this entity.
    /// </summary>
    public virtual DateTime? LastModificationTime{ get; set; }

    /// <summary>
    /// Last modifier user of this entity.
    /// </summary>
    public virtual long? LastModifierUserId{ get; set; }
    
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    public virtual DateTime CreationTime{ get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual long? CreatorUserId{ get; set; }
}
