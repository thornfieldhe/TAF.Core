// 何翔华
// Taf.Core.Extension
// IAuditedEntity.cs

namespace Taf.Core.Extension;

public interface IAuditedEntity:IHasCreationTime,IHasModificationTime{
    /// <summary>Id of the creator user of this entity.</summary>
    long? CreatorUserId{ get; set; }
    
    /// <summary>Last modifier user for this entity.</summary>
    long? LastModifierUserId{ get; set; }
}