using Family.Application.Models.Family;
using Family.Domain.Entities;

namespace Family.Application.Abstractions;

public interface IFamilyService
{
    public Task<FamilyModel> CreateFamilyAsync(UserInfo userInfo, string familyName);
    public Task<FamilyModel> GetFamilyByIdAsync(Guid familyId);
    public Task<FamilyModel> UpdateFamilyAsync(FamilyUpdateModel familyUpdateModel);
}