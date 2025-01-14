using System.Collections.Generic;
using Family.Domain.Entities.Base;
using Family.Domain.Entities.Exceptions;

namespace Family.Domain.Entities;

public class Family(long id) : Entity(id)
{
    public string FamilyName { get; }

    private readonly ICollection<FamilyMember> _familyMembers = [];
    public ICollection<FamilyMember> FamilyMembers => _familyMembers;

    public Family(long id, string familyName) : this(id)
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