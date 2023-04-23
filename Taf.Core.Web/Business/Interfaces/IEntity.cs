// 何翔华
// Taf.Core.Extension
// IEntity.cs

namespace Taf.Core.Web;

public interface IEntity<TPrimaryKey>:IHasConcurrencyStamp{
    /// <summary>Unique identifier for this entity.</summary>
    TPrimaryKey Id{ get; set; }
}

public interface IEntity : IEntity<Guid>{ }
