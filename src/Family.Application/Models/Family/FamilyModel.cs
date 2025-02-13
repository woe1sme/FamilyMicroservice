using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyModel(string familyName) : FamilyModelBase(familyName), IModel
{
    public required IList<FamilyMemberModel> FamilyMembers { get; init; }
    public Guid Id { get; init; }
}