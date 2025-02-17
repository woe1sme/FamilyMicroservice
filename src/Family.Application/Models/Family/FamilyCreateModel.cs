using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel : ICreateModel
{
    public string FamilyName { get; init; }
    
    public Guid CreatorUserId { get; init; }
    
    public string CreatorUserName { get; init; }
}