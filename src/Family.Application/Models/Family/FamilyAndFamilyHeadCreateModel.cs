using Family.Application.Models.Base;

namespace Family.Application.Models.Family;

public record FamilyAndFamilyHeadCreateModel : ICreateModel
{
    public FamilyAndFamilyHeadCreateModel(string familyName, Guid familyHeadUserId, string familyHeadUserName)
    {
        FamilyName = familyName;
        FamilyHeadUserId = familyHeadUserId;
        FamilyHeadUserName = familyHeadUserName;
    }

    public string FamilyName { get; init; }
    public Guid FamilyHeadUserId { get; init; }
    public string FamilyHeadUserName { get; init; }
}