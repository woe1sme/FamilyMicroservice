using Family.Application.Models.Base;

namespace Family.Application.Models.Family;

public record FamilyUpdateModel : IUpdateModel
{
    public Guid Id { get; init; }
    
    public string FamilyName { get; init; }
}