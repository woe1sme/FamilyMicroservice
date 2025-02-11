using Family.Application.Models.Family;
using Family.Application.Models.UserInfo;

namespace Family.Application.Abstractions;

public interface IFamilyService
{
    public Task<FamilyModel> CreateFamilyAsync(FamilyCreateModel family, UserInfoModel userInfo);
    public Task<FamilyModel> GetFamilyByIdAsync(Guid familyId);
    public Task<FamilyModel> UpdateFamilyAsync(FamilyUpdateModel familyUpdateModel);
    public IEnumerable<FamilyModel> GetFamilyByUserInfo(UserInfoModel userInfo);
    public IEnumerable<FamilyModel> GetAll();
}