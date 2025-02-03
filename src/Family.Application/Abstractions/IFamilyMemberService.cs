using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;

namespace Family.Application.Abstractions;

public interface IFamilyMemberService
{
    public Task<FamilyMemberModel> AddMemberToFamilyAsync(FamilyMemberCreateModel familyMemberCreateModel, UserInfoModel userInfo, Guid familyId);
    public Task RemoveMemberFromFamilyAsync(FamilyMemberModel familyMember, Guid familyId);
    public Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel);
    public Task<IEnumerable<FamilyMemberModel>> GetAllMembersAsync(Guid familyId);
}