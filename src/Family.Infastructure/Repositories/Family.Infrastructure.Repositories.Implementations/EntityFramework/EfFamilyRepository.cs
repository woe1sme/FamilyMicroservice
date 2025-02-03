using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyRepository(FamilyDbContext context) : EfRepository<Domain.Entities.Family, Guid>(context), IFamilyRepository
{
    private readonly FamilyDbContext _context = context;

    public IEnumerable<Domain.Entities.Family?> GetAllMemberFamilies(FamilyMember member)
    {
        return _context.Family.Where(f => f.FamilyMembers.Contains(member)).AsEnumerable();
    }

    public async Task<Domain.Entities.Family?> GetMemberFamilyAsync(FamilyMember member)
    {
        return await _context.Family.FindAsync(member.FamilyId);
    }
}