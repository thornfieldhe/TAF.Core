// 何翔华
// Taf.Core.Extension
// IAuditedEntity.cs

namespace Taf.Core.Extension;

public interface IAuditedEntity{
    /// <summary>Id of the creator user of this entity.</summary>
    long? CreatorUserId{ get; set; }
    
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreationTime{ get; set; }
    
    /// <summary>Last modifier user for this entity.</summary>
    long? LastModifierUserId{ get; set; }
    
    /// <summary>
    /// The last modified time for this entity.
    /// </summary>
    DateTime? LastModificationTime{ get; set; }
}