using Family.Application.Models.Base;

namespace Family.Application.Models.Family;

public record FamilyUpdateModel : IUpdateModel
{
    public string Name { get; init; }

    public FamilyUpdateModel(string familyName)
    {
        Name = familyName;
    }
}