namespace Family.Contracts.FamilyMember
{
    public record FamilyMemberCreated(Guid UserId, string Name, string Role, Guid FamilyId);
}
