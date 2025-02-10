using System.Collections.Generic;
using System.Threading.Tasks;
using Family.Domain;
using Family.Domain.Entities;

namespace Family.Domain.Repositories.Abstractions;

public interface IFamilyMemberRepository : IRepository<FamilyMember, Guid>
{
    public Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(Guid familyId);
    public Task<FamilyMember> GetFamilyMemberByUserIdAsync(Guid userInfoId, Guid familyId);
}