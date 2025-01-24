using Family.Application.Abstractions;
using Family.Application.Models.Family;

namespace Family.Application.Services;

public class FamilyMemberService : IFamilyMemberService
{
    public async Task<FamilyCreateModel> CreateFamilyMemberAsync(long Id, string Name)
    {
        throw new NotImplementedException();
    }
}