namespace Family.Application.Models.Family;

public abstract record FamilyModelBase(string familyName)
{
    public string FamilyName { get; init; } = familyName;
}