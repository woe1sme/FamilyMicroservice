using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyRepository(FamilyDbContext context) : EfRepository<Domain.Entities.Family>(context), IFamilyRepository
{
    public async Task<IEnumerable<Domain.Entities.Family>> GetAllMemberFamiliesAsync(FamilyMember member)
    {
        throw new NotImplementedException();
    }
}