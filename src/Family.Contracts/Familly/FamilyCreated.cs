namespace Family.Contracts.Familly;

public record FamilyCreated(Guid FamilyId, string FamilyName, Guid FamilyHeadUserId) : IContractMessage;
