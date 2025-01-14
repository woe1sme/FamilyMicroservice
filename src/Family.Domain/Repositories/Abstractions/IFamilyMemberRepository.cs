using System.Collections.Generic;
using System.Threading.Tasks;
using Family.Domain;
using Family.Domain.Entities;

namespace Family.Domain.Repositories.Abstractions;

public interface IFamilyMemberRepository : IRepository<FamilyMember>
{
    public Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(long familyId);
    public IEnumerable<FamilyMember> GetAllFamilyMembersAsync(Entities.Family family);
}