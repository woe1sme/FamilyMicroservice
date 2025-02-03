using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;

namespace Family.Application.Services;

public class FamilyMemberService(IUnitOfWork unitOfWork, IMapper mapper) : IFamilyMemberService
{
    public async Task<FamilyMemberModel> AddMemberToFamilyAsync(FamilyMemberCreateModel familyMemberCreateModel, UserInfoModel userInfo, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);

        if (family is null)
            throw new FamilyNotFoundException(familyId);

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var newFamilyMember = mapper.Map<FamilyMember>((familyMemberCreateModel, userInfo));
            
            await unitOfWork.FamilyMemberRepository.AddAsync(newFamilyMember);
            family.FamilyMembers.Add(newFamilyMember);
            await unitOfWork.FamilyRepository.UpdateAsync(family);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<FamilyMemberModel>(newFamilyMember);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RemoveMemberFromFamilyAsync(FamilyMemberModel familyMember, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        
        if(family is null)
            throw new FamilyNotFoundException(familyId);
        
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var removedFamilyMember = mapper.Map<FamilyMember>(familyMember);
            family.FamilyMembers.Remove(removedFamilyMember);
        
            await unitOfWork.FamilyMemberRepository.DeleteAsync(removedFamilyMember);
            await unitOfWork.FamilyRepository.UpdateAsync(family);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel)
    {
        var familyMember = await unitOfWork.FamilyRepository.GetByIdAsync(familyMemberUpdateModel.Id);

        if (familyMember is null)
            throw new FamilyMemberNotFoundException(familyMemberUpdateModel.Id);
        
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var updatedFamilyMember = await unitOfWork.FamilyMemberRepository
                .UpdateAsync(mapper.Map<FamilyMember>(familyMemberUpdateModel));
            
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<FamilyMemberModel>(updatedFamilyMember);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IEnumerable<FamilyMemberModel>> GetAllMembersAsync(Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        if(family is null)
            throw new FamilyNotFoundException(familyId);

        return mapper.Map<IEnumerable<FamilyMemberModel>>(
            await unitOfWork.FamilyMemberRepository.GetAllMembersByFamilyIdAsync(familyId));
    }
}