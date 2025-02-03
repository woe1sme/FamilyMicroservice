using Family.Domain.Entities.Base;
using Family.Domain.Entities.Exceptions;

namespace Family.Domain.Entities;

public class Family(Guid id) : Entity<Guid>(id)
{
    public string FamilyName { get; set; }

    private readonly IList<FamilyMember> _familyMembers = [];
    public IList<FamilyMember> FamilyMembers => _familyMembers;

    public Family(string familyName) : this(Guid.NewGuid())
    {
        FamilyName = familyName;
    }

    public void AddFamilyMember(FamilyMember familyMember)
    {
        if(FamilyMembers.Contains(familyMember))
            throw new MemberAlreadyExistException(this, familyMember);
        _familyMembers.Add(familyMember);
    }

    public void RemoveFamilyMember(FamilyMember familyMember)
    {
        if(!FamilyMembers.Contains(familyMember))
            throw new NoSuchMemberInFamilyException(this, familyMember);
        _familyMembers.Remove(familyMember);
    }
}