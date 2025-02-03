using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel : ICreateModel
{
    public required string FamilyName { get; init; }
}