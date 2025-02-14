using Family.Application.Models.Family;
using Family.Application.Models.UserInfo;

namespace Family.Application.Abstractions;

public interface IFamilyService
{
    public Task<FamilyModel> CreateFamilyAsync(FamilyCreateModel family);
    public Task<FamilyModel> GetFamilyByIdAsync(Guid familyId);
    public Task<FamilyModel> UpdateFamilyAsync(Guid familyId, FamilyUpdateModel familyUpdateModel);
    public IEnumerable<FamilyModel> GetFamilyByUserId(Guid userId);
    public IEnumerable<FamilyModel> GetAll();
}