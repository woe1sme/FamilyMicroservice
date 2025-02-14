using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel(string FamilyName) : ICreateModel
{
    public string FamilyName { get; init; } = FamilyName;
}