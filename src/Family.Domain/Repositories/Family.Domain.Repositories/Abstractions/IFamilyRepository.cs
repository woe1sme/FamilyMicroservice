using Family.Domain.Entities;

namespace Family.Domain.Repositories.Abstractions;

public interface IFamilyRepository : IRepository<Family.Domain.Entities.Family>
{
    public Task<IEnumerable<Entities.Family>> GetAllMemberFamiliesAsync(FamilyMember member);
}