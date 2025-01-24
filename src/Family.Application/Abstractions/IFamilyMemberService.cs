using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;

namespace Family.Application.Abstractions;

public interface IFamilyMemberService
{
    public Task<FamilyMemberModel> CreateFamilyMemberAsync(string Name);

    public Task<FamilyMemberModel> GetFamilyMemberByIdAsync(long Id);

}