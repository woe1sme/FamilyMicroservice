using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.Family;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;
using Family.Domain.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Family.Application.Services;

public class FamilyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FamilyService> logger) : IFamilyService
{
    public async Task<FamilyModel> CreateFamilyAsync(FamilyCreateModel familyCreateModel, UserInfoModel userInfoModel)
    {
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var userInfoData = await unitOfWork.UserInfoRepository.GetByIdAsync(userInfoModel.Id);
        
            if (userInfoData is null)
            {
                await unitOfWork.UserInfoRepository.AddAsync(mapper.Map<UserInfo>(userInfoModel));
                logger.LogInformation($"UserInfo {userInfoModel.Id}, {userInfoModel.UserName} has been created.");
            }
            
            var family = new Domain.Entities.Family(familyCreateModel.FamilyName);
            var familyHead = new FamilyMember(userInfoModel.UserName, family.Id, Role.Head);
            family.FamilyMembers.Add(familyHead);
            
            await unitOfWork.FamilyRepository.AddAsync(family);
            await unitOfWork.FamilyMemberRepository.AddAsync(familyHead);
            
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<FamilyModel>(family);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Failed to create family");
            throw;
        }
    }

    public async Task<FamilyModel> GetFamilyByIdAsync(Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);

        if (family is null)
        {
            logger.LogError("Family not found");
            throw new FamilyNotFoundException(familyId);
        }

        return mapper.Map<FamilyModel>(family);
    }

    public async Task<FamilyModel> UpdateFamilyAsync(FamilyUpdateModel familyUpdateModel)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyUpdateModel.Id);
        
        if(family is null)
        {
            logger.LogError("Failed to update family. Family to update not found");
            throw new FamilyNotFoundException(familyUpdateModel.Id);
        }

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            family.FamilyName = familyUpdateModel.FamilyName;
            await unitOfWork.FamilyRepository.UpdateAsync(family);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<FamilyModel>(family);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Failed to update family");
            throw;
        }
    }

    public IEnumerable<FamilyModel> GetFamilyByUserInfo(UserInfoModel userInfo)
    {
        if (userInfo is null)
        {
            logger.LogInformation("User is null");
            throw new ArgumentNullException(nameof(userInfo));
        }

        try
        {
            var familyMembers = unitOfWork.FamilyMemberRepository.GetAll().Where(fm => fm.UserId == userInfo.Id);
            var families = unitOfWork.FamilyRepository.GetAll()
                .Where(f => familyMembers
                    .Any(fm => f.FamilyMembers
                        .Contains(fm)));
            
            return mapper.Map<IEnumerable<FamilyModel>>(families);
        } 
        catch(Exception ex)
        {
            logger.LogError(ex, "Failed to get families");
            throw;
        }
    }

    public IEnumerable<FamilyModel> GetAll()
    {
        return mapper.Map<IEnumerable<FamilyModel>>(unitOfWork.FamilyRepository.GetAll());
    }
}