using System;

namespace Family.Domain.Entities.Base;

public abstract class Entity(long id): IEquatable<Entity>
{
    public long Id { get; protected set; } = id;
    
    public override string ToString() => Id.ToString();

    public override bool Equals(object? obj) => obj is Entity other && Id.Equals(other.Id);
    
    public override int GetHashCode() => Id.GetHashCode();
    
    public bool Equals(Entity? other) => other is not null && Id.Equals(other.Id);
    
    public static bool operator ==(Entity? left, Entity right)
    {
        if(ReferenceEquals(left, right)) 
            return true;
        return left.Id == right.Id;
    }

    public static bool operator !=(Entity left, Entity right) => left.Id != right.Id;
}