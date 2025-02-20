using Family.Domain.Entities.Base;
using Family.Domain.Entities.Enums;

namespace Family.Domain.Entities;

public class FamilyMember(Guid id) : Entity<Guid>(id)
{
    public string Name { get; set; }
    public Role Role { get; set; }
    public Guid FamilyId { get; set; }
    public Family Family { get; set; }
    public Guid UserId { get; set; }

    public FamilyMember(Guid id, string name, Guid userId, Role role, Guid familyId) : this(id)
    {
        FamilyId = familyId;
        Role = role;
        Name = name;
        UserId = userId;
    }
}