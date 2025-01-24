using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel : ICreateModel
{
    public required string FamilyName { get; init; }

    public required IList<FamilyMemberModel> FamilyMembers { get; init; }
}