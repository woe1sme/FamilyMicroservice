using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;
using Family.Domain.Repositories.Abstractions;

namespace Family.Application.Services;

public class FamilyService : IFamilyService
{
    private IFamilyMemberRepository _familyMemberRepository;
    private IFamilyRepository _familyRepository;
    public async Task<FamilyModel> CreateFamilyAsync(long familyHeadId, string FamilyName)
    {
        var familyHeadModel = await _familyMemberRepository.GetByIdAsync(familyHeadId);

        if (familyHeadModel == null)
            throw new NullReferenceException();

        var family = new FamilyModel
            {
                FamilyName = FamilyName,
                FamilyMembers = new List<FamilyMemberModel>()
            };

        

    }
}