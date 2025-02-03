using AutoMapper;
using Family.Application.Abstractions;
using Family.Application.Exceptions;
using Family.Application.Models.Family;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;
using Family.Domain.Repositories.Abstractions;

namespace Family.Application.Services;

public class FamilyService(IUnitOfWork unitOfWork, IMapper mapper) : IFamilyService
{
    public async Task<FamilyModel> CreateFamilyAsync(UserInfo userInfo, string familyName)
    {
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var family = new Domain.Entities.Family(familyName);
            var familyHead = new FamilyMember(userInfo.UserName, family.Id, Role.Head);
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
            throw;
        }
    }

    public async Task<FamilyModel> GetFamilyByIdAsync(Guid familyId)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyId);
        
        if(family is null)
            throw new FamilyNotFoundException(familyId);
        
        return mapper.Map<FamilyModel>(family);
    }

    public async Task<FamilyModel> UpdateFamilyAsync(FamilyUpdateModel familyUpdateModel)
    {
        var family = await unitOfWork.FamilyRepository.GetByIdAsync(familyUpdateModel.Id);
        
        if(family is null)
            throw new FamilyNotFoundException(familyUpdateModel.Id);

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            family.FamilyName = familyUpdateModel.Name;
            await unitOfWork.FamilyRepository.UpdateAsync(family);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            
            return mapper.Map<FamilyModel>(family);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

