using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyRepository(FamilyDbContext context) : EfRepository<Domain.Entities.Family>(context), IFamilyRepository
{
    public IEnumerable<Domain.Entities.Family> GetAllMemberFamilies(FamilyMember member)
    {
        return context.Family.Where(f => f.FamilyMembers.Contains(member)).AsEnumerable();
    }
}