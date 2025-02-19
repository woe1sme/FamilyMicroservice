using Family.Application.Models.Base;

namespace Family.Application.Models.FamilyMember;

public record FamilyMemberCreateModel : ICreateModel
{
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
    public required string Role { get; init; }
}