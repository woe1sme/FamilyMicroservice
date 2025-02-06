using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Family.Application.Services;

public class FamilyMemberService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FamilyMemberService> logger) : IFamilyMemberService
{
    public async Task<FamilyMemberModel> AddMemberToFamilyAsync(FamilyMemberCreateModel familyMemberCreateModel, UserInfoModel userInfo, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);

        if (family is null)
        {
            logger.LogError($"Family with id: {familyId} not found");
            throw new FamilyNotFoundException(familyId);
        }

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
            logger.LogError(ex, "Failed to create family member");
            throw;
        }
    }

    public async Task RemoveMemberFromFamilyAsync(FamilyMemberModel familyMember, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);

        if (family is null)
        {
            logger.LogError($"Family with id: {familyId} not found");
            throw new FamilyNotFoundException(familyId);
        }

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
            logger.LogError(ex, "Failed to remove family member");
            throw;
        }
    }

    public async Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel)
    {
        var familyMember = await unitOfWork.FamilyRepository.GetByIdAsync(familyMemberUpdateModel.Id);

        if (familyMember is null)
        {
            logger.LogError($"Family with id: {familyMemberUpdateModel.Id} not found");
            throw new FamilyMemberNotFoundException(familyMemberUpdateModel.Id);
        }
        
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
            logger.LogError(ex, "Failed to update family member");
            throw;
        }
    }

    public async Task<IEnumerable<FamilyMemberModel>> GetAllMembersByFamilyIdAsync(Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        
        if(family is null)
        {
            logger.LogError($"Family with id: {familyId} not found");
            throw new FamilyNotFoundException(familyId);
        }

        return mapper.Map<IEnumerable<FamilyMemberModel>>(
            await unitOfWork.FamilyMemberRepository.GetAllMembersByFamilyIdAsync(familyId));
    }

    public async Task<FamilyMemberModel> GetMemberByIdAsync(Guid memberId)
    {
        var familyMember = await unitOfWork.FamilyMemberRepository.GetByIdAsync(memberId);
        
        if(familyMember is null)
        {
            logger.LogError($"Family with id: {memberId} not found");
            throw new FamilyMemberNotFoundException(memberId);
        }

        return mapper.Map<FamilyMemberModel>(familyMember);
    }

    public IEnumerable<FamilyMemberModel> GetFamilyMemberByUserInfo(UserInfoModel userInfo)
    {
        if (userInfo is null)
        {
            logger.LogError("User is null");
            throw new ArgumentNullException(nameof(userInfo));
        }

        try
        {
            var familyMembers = unitOfWork.FamilyMemberRepository.GetAll().Where(fm => fm.UserId == userInfo.Id);

            return mapper.Map<IEnumerable<FamilyMemberModel>>(familyMembers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get family members");
            throw;
        }
    }
}