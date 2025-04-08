namespace Family.Contracts.FamilyMember;

public record FamilyMemberUpdated(Guid UserId, string Name, string Role, Guid FamilyId) : IContractMessage;