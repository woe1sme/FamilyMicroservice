using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;
using Family.Domain.Repositories.Abstractions;

namespace Family.Application.Services;

public class FamilyService : IFamilyService
{
    private IFamilyRepository _familyRepository;
    private IFamilyMemberService _familyMemberService;

    public async Task<FamilyCreateModel> CreateFamilyAsync(UserInfoModel userInfo, string FamilyName)
    {

        var familyHeadModel = await _familyMemberService.CreateFamilyMemberAsync(userInfo.UserName);
         
        var family = new FamilyCreateModel
        {
            FamilyName = FamilyName,
            FamilyMembers = new List<FamilyMemberModel>()
        };

        family.FamilyMembers.Add(familyHeadModel);

        return family;
    }
}