using Family.Domain.Entities.Base;

namespace Family.Domain.Entities;

public class Family(Guid id) : Entity<Guid>(id)
{
    public string FamilyName { get; set; }

    private readonly IList<FamilyMember> _familyMembers = [];

    public IList<FamilyMember> FamilyMembers => _familyMembers;

    public Family(string familyName, Guid id): this(id)
    {
        FamilyName = familyName; 
    }
}