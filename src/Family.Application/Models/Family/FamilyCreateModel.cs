using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel : ICreateModel
{
    public string Name { get; init; }

    public IList<FamilyMemberModel> FamilyMembers { get; init; }
}