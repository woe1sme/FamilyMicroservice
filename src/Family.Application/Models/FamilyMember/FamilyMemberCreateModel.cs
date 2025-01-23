using Family.Application.Models.Base;

namespace Family.Application.Models.FamilyMember;

public record FamilyMemberCreateModel : ICreateModel
{
    public string Name { get; init; }
    
    public long FamilyId { get; init; }
    
    public string Role { get; init; }
}