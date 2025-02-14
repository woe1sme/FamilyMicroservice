using Family.Domain.Entities.Base;
using Family.Domain.Entities.Enums;
using Family.Domain.Entities.Exceptions;

namespace Family.Domain.Entities;

public class FamilyMember(Guid id) : Entity<Guid>(id)
{
    public string Name { get; init; }
    public Role Role { get; private set; }
    public Guid FamilyId { get; init; }
    public Family Family { get; set; }
    
    public Guid UserId { get; init; }

    public FamilyMember(string name, Guid familyId, Role role) : this(Guid.NewGuid())
    {
        FamilyId = familyId;
        Role = role;
        Name = name;
    }
}