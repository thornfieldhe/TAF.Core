// 何翔华
// Taf.Core.Extension
// IEntity.cs

namespace Taf.Core.Extension;

public interface IEntity<TPrimaryKey>{
    /// <summary>Unique identifier for this entity.</summary>
    TPrimaryKey Id{ get; set; }
}

public interface IEntity : IEntity<Guid>{ }
