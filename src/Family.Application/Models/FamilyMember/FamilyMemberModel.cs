using Family.Application.Models.Base;

namespace Family.Application.Models.FamilyMember;

public record FamilyMemberModel : IModel
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Name { get; init; }
    
    public Guid FamilyId { get; init; }
    
    public string Role { get; init; }
}