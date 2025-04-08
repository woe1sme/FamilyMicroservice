using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.FamilyMember;
using Family.Contracts.FamilyMember;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;
using Family.Domain.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Family.Application.Services;

public class FamilyMemberService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FamilyMemberService> logger, IPublishService publishService) : IFamilyMemberService
{
    public async Task<FamilyMemberModel> CreateMemberAsync(FamilyMemberCreateModel familyMemberCreateModel, Guid familyId)
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
            var mappedFamilyMember = mapper.Map<FamilyMember>(familyMemberCreateModel);
            mappedFamilyMember.FamilyId = family.Id;

            await unitOfWork.FamilyMemberRepository.AddAsync(mappedFamilyMember);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            await publishService.PublishAsync(message: new FamilyMemberCreated(
                familyMemberCreateModel.UserId,
                familyMemberCreateModel.Name,
                familyMemberCreateModel.Role,
                familyId));

            return mapper.Map<FamilyMemberModel>(mappedFamilyMember);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Failed to create family member");
            throw;
        }
    }

    public async Task RemoveMemberAsync(Guid familyMemberId, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        if (family is null)
        {
            logger.LogError($"Family with id: {familyId} not found");
            throw new FamilyNotFoundException(familyId);
        }

        var familyMember = await unitOfWork.FamilyMemberRepository.GetByIdAsync(familyMemberId);
        if (familyMember is null)
        {
            logger.LogError($"Family with id: {familyMemberId} not found");
            throw new FamilyMemberNotFoundException(familyMemberId);
        }

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            family.FamilyMembers.Remove(familyMember);
            await unitOfWork.FamilyMemberRepository.DeleteAsync(familyMember);
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

    public async Task<FamilyMemberModel> UpdateMemberAsync(FamilyMemberUpdateModel familyMemberUpdateModel, Guid familyMemberId)
    {
        var familyMember = await unitOfWork.FamilyMemberRepository.GetByIdAsync(familyMemberId);

        if (familyMember is null)
        {
            logger.LogError($"Family with id: {familyMemberId} not found");
            throw new FamilyMemberNotFoundException(familyMemberId);
        }
        
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            familyMember.Name = familyMemberUpdateModel.Name ?? familyMember.Name;

            if (!string.IsNullOrEmpty(familyMemberUpdateModel.Role))
            {
                if (Enum.TryParse(typeof(Role), familyMemberUpdateModel.Role, out var role))
                {
                    familyMember.Role = (Role)role;
                }
                else
                {
                    logger.LogError("Invalid role value: {Role}", familyMemberUpdateModel.Role);
                    throw new ArgumentException($"Invalid role value: {familyMemberUpdateModel.Role}");
                }
            }

            var updatedFamilyMember = await unitOfWork.FamilyMemberRepository.UpdateAsync(familyMember);
            
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            await publishService.PublishAsync(message: new FamilyMemberUpdated(
                familyMember.UserId,
                familyMember.Name,
                familyMember.Role.ToString(),
                familyMember.FamilyId ));

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

    public IEnumerable<FamilyMemberModel> GetFamilyMemberByUserInfo(Guid userId)
    {
        try
        {
            var familyMembers = unitOfWork.FamilyMemberRepository.GetAll().Where(fm => fm.UserId == userId);
            return mapper.Map<IEnumerable<FamilyMemberModel>>(familyMembers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get family members");
            throw;
        }
    }

    public async Task<FamilyMemberModel> GetFamilyMemberByUserIdAsync(Guid userInfoId, Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);

        if (family is null)
        {
            logger.LogError($"Family with id: {familyId} not found");
            throw new FamilyNotFoundException(familyId);
        }
        
        return mapper.Map<FamilyMemberModel>(
            await unitOfWork.FamilyMemberRepository.GetFamilyMemberByUserIdAsync(userInfoId, familyId));
    }

    public IEnumerable<FamilyMemberModel> GetAllFamilyMembers()
    {
        return mapper.Map<IEnumerable<FamilyMemberModel>>(unitOfWork.FamilyMemberRepository.GetAll());
    }
}