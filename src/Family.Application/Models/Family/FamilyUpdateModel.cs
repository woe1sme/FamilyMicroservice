using Family.Application.Models.Base;

namespace Family.Application.Models.Family;

public record FamilyUpdateModel(string FamilyName) : IUpdateModel
{
    public string FamilyName { get; init; } = FamilyName;
    public Guid Id { get; init; }
}