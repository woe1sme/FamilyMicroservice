using Family.Application.Models.Base;

namespace Family.Application.Models.Family;

public record FamilyUpdateModel(string familyName) : FamilyModelBase(familyName), IUpdateModel
{
    public Guid Id { get; init; }
}