namespace Family.Domain.Entities.Exceptions;

public class NoSuchMemberInFamilyException(Family family, FamilyMember familyMember) : 
    InvalidOperationException($"No {familyMember.Name} in {family.FamilyName} family.")
{
    public FamilyMember FamilyMember => familyMember;
    public Family Family => family;
}