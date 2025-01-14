using System;

namespace Family.Domain.Entities.Exceptions;

public class MemberAlreadyExistException(Family family, FamilyMember familyMember) : 
    InvalidOperationException($"{familyMember.Name} already in {family.FamilyName} family")
{
    public Family Family => family;
    public FamilyMember FamilyMember => familyMember;
}