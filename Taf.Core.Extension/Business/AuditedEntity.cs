// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuditedEntity.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Extension
// AuditedEntity.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class AuditedEntity : AuditedEntity<Guid>, IEntity{
    protected AuditedEntity() => CreationTime = DateTime.UtcNow;
}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAuditedEntity
{
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
