using Family.Application.Models.Family;

namespace Family.Application.Abstractions;

public interface IFamilyService
{
    public Task<FamilyModel> CreateFamilyAsync(FamilyAndFamilyHeadCreateModel family);
    public Task<FamilyModel> GetFamilyByIdAsync(Guid familyId);
    public Task<FamilyModel> UpdateFamilyAsync(Guid familyId, FamilyUpdateModel familyUpdateModel);
    public IEnumerable<FamilyModel> GetFamilyByUserId(Guid userId);
    public IEnumerable<FamilyModel> GetAll();
}