// 何翔华
// Taf.Core.Extension
// IDeletionAudited.cs

namespace Taf.Core.Extension;

/// <summary>
/// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
/// </summary>
public interface IDeletionAudited :  ISoftDelete
{
    /// <summary>Which user deleted this entity?</summary>
    long? DeleterUserId{ get; set; }
}