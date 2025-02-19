using Family.Application.Models.Base;

namespace Family.Application.Models.FamilyMember;

public record FamilyMemberUpdateModel : IUpdateModel
{
    public string Name { get; init; }
    public string Role { get; init; }

    public FamilyMemberUpdateModel(string name, string role)
    {
        Name = name;
        Role = role;
    }
}