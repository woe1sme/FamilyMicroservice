using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.Family;
using Family.Contracts.Familly;
using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Family.Application.Services;

public class FamilyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FamilyService> logger, IPublishService publishService) : IFamilyService
{
    public async Task<FamilyModel> CreateFamilyAsync(FamilyAndFamilyHeadCreateModel familyAndFamilyHeadCreateModel)
    {
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var family = await unitOfWork.FamilyRepository.AddAsync(mapper.Map<Domain.Entities.Family>(familyAndFamilyHeadCreateModel));
            
            var familyHeadMapped = mapper.Map<FamilyMember>(familyAndFamilyHeadCreateModel);
            familyHeadMapped.FamilyId = family.Id;

            var familyHead = await unitOfWork.FamilyMemberRepository.AddAsync(familyHeadMapped);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            await publishService.PublishAsync(message: new FamilyCreated(family.Id, family.FamilyName, familyHead.UserId));

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

    public async Task<FamilyModel> UpdateFamilyAsync(Guid familyId, FamilyUpdateModel familyUpdateModel)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        
        if(family is null)
        {
            logger.LogError("Failed to update family. Family to update not found");
            throw new FamilyNotFoundException(familyId);
        }

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            family.FamilyName = familyUpdateModel.Name;
            await unitOfWork.FamilyRepository.UpdateAsync(family);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            await publishService.PublishAsync(message: new FamilyUpdated(family.Id, family.FamilyName));

            return mapper.Map<FamilyModel>(family);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Failed to update family");
            throw;
        }
    }

    public IEnumerable<FamilyModel> GetFamilyByUserId(Guid userId)
    {
        try
        {
            var familyMembers = unitOfWork.FamilyMemberRepository.GetAll().Where(fm => fm.UserId == userId);
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