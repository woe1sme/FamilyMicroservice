using Family.Application.Models.FamilyMember;

namespace Family.Application.Abstractions;

public interface IFamilyMemberService
{
    public Task<FamilyMemberModel> CreateMemberAsync(FamilyMemberCreateModel familyMemberCreateModel, Guid familyId);
    public Task RemoveMemberAsync(Guid familyMemberId, Guid familyId);
    public Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel, Guid familyMemberId);
    public Task<IEnumerable<FamilyMemberModel>> GetAllMembersByFamilyIdAsync(Guid familyId);
    public Task<FamilyMemberModel> GetMemberByIdAsync(Guid memberId);
    public Task<FamilyMemberModel> GetFamilyMemberByUserIdAsync(Guid userId, Guid familyId);
    public IEnumerable<FamilyMemberModel> GetAllFamilyMembers();
    public IEnumerable<FamilyMemberModel> GetFamilyMemberByUserInfo(Guid userId);
}