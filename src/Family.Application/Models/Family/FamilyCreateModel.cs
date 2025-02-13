using Family.Application.Models.Base;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Models.Family;

public record FamilyCreateModel(string familyName) : FamilyModelBase(familyName), ICreateModel
{
    
}