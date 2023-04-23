// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;
using System.Reflection;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Extension
// BaseEntity.cs

namespace Taf.Core.Web;

/// <summary>
/// Entity只允许数据库对象继承
/// Dto对象请使用Engity
/// </summary>
[Serializable]
public abstract class DbEntity : Entity, IEntity{
}


/// <summary>
/// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public  class Entity : Entity<Guid>, IEntity{
    protected Entity(){
        Id               = GuidGanerator.NextGuid();
    }
}

/// <summary>
/// Basic implementation of IEntity interface.
/// An entity can inherit this class of directly implement to IEntity interface.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    [SugarColumn(IsPrimaryKey = true,CreateTableFieldSort = -1)]
    public virtual TPrimaryKey Id{ get; set; }


    /// <inheritdoc/>
    public override bool Equals(object? obj){
        if(obj == null
        || !(obj is Entity<TPrimaryKey>)){
            return false;
        }

        //Same instances must be considered as equal
        if(ReferenceEquals(this, obj)){
            return true;
        }

        //Transient objects are not considered as equal
        var other = (Entity<TPrimaryKey>)obj;

        //Must have a IS-A relation of types or must be same type
        var typeOfThis  = GetType();
        var typeOfOther = other.GetType();
        if(!IntrospectionExtensions.GetTypeInfo(typeOfThis).IsAssignableFrom(typeOfOther)
        && !IntrospectionExtensions.GetTypeInfo(typeOfOther).IsAssignableFrom(typeOfThis)){
            return false;
        }

        if(this is ITenant
        && other is ITenant
        && this.As<ITenant>().TenantId != other.As<ITenant>().TenantId){
            return false;
        }

        return   Id==null?false:Id.Equals(other.Id);
    }

    public string ConcurrencyStamp{ get; set; }
    
    /// <inheritdoc/>
    public override int GetHashCode(){
        if(Id == null){
            return 0;
        }

        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right){
        if(Equals(left, null)){
            return Equals(right, null);
        }

        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right) => !(left == right);

    /// <inheritdoc/>
    public override string ToString() => $"[{GetType().Name} {Id}]";


}