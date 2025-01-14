using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyMemberRepository(FamilyDbContext context) : EfRepository<FamilyMember>(context), IFamilyMemberRepository
{
    private readonly FamilyDbContext _context = context;

    public async Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(long familyId)
    {
        var family = await _context.Family.FindAsync(familyId);
        return family == null ? new List<FamilyMember>() : family.FamilyMembers;
    }

    public IEnumerable<FamilyMember> GetAllFamilyMembersAsync(Domain.Entities.Family family)
    {
        return _context.FamilyMember
            .Where(m => family.FamilyMembers.Contains(m))
            .AsEnumerable();
    }
}