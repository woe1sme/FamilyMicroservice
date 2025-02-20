using Family.Domain.Entities.Base;
using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyRepository(FamilyDbContext context) : EfRepository<Domain.Entities.Family, Guid>(context), IFamilyRepository
{
    private readonly FamilyDbContext _context = context;

    public new IQueryable<Domain.Entities.Family> GetAll()
    {
        return context.Set<Domain.Entities.Family>().AsQueryable().Include(x => x.FamilyMembers);
    }
}