using Family.Application.Models.Base;

namespace Family.Application.Models.FamilyMember;

public record FamilyMemberUpdateModel : IUpdateModel
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public required Guid FamilyId { get; init; }
    
    public required string Role { get; init; }
}