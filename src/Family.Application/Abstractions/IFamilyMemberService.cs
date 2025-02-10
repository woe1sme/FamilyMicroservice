using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;

namespace Family.Application.Abstractions;

public interface IFamilyMemberService
{
    public Task<FamilyMemberModel> AddMemberToFamilyAsync(FamilyMemberCreateModel familyMemberCreateModel, UserInfoModel userInfo, Guid familyId);
    public Task RemoveMemberFromFamilyAsync(FamilyMemberModel familyMember, Guid familyId);
    public Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel);
    public Task<IEnumerable<FamilyMemberModel>> GetAllMembersByFamilyIdAsync(Guid familyId);
    public Task<FamilyMemberModel> GetMemberByIdAsync(Guid memberId);
    public IEnumerable<FamilyMemberModel> GetFamilyMemberByUserInfo(UserInfoModel userInfo);
    public Task<FamilyMemberModel> GetFamilyMemberByUserIdAsync(Guid userInfoId, Guid familyId);
}