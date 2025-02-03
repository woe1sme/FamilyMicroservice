using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyModel : IModel
{
    public required string FamilyName { get; init; }
    public required IList<FamilyMemberModel> FamilyMembers { get; init; }
    public Guid Id { get; init; }
}