// 何翔华
// Taf.Core.Extension
// ISoftDelete.cs

namespace Taf.Core.Extension;

/// <summary>
/// Used to standardize soft deleting entities.
/// Soft-delete entities are not actually deleted,
/// marked as IsDeleted = true in the database,
/// but can not be retrieved to the application.
/// </summary>
public interface ISoftDelete:IEntity
{
    /// <summary>
    /// Used to mark an Entity as 'Deleted'. 
    /// </summary>
    bool IsDeleted{ get; set; }
    
    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    DateTime? DeletionTime{ get; set; }
}
