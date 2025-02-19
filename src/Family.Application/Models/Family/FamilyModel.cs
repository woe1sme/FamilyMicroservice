using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyModel : IModel
{
    public FamilyModel(string familyName, IList<FamilyMemberModel> familyMembers, Guid id)
    {
        FamilyName = familyName;
        FamilyMembers = familyMembers;
        Id = id;
    }

    public string FamilyName { get; init; }
    public required IList<FamilyMemberModel> FamilyMembers { get; init; }
    public Guid Id { get; init; }
}