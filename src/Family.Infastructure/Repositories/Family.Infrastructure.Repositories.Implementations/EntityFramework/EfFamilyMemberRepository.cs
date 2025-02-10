using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyMemberRepository(FamilyDbContext context) : EfRepository<FamilyMember, Guid>(context), IFamilyMemberRepository
{
    private readonly FamilyDbContext _context = context;

    public async Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(Guid familyId)
    {
        var family = await _context.Family.FindAsync(familyId);
        return family is null ? new List<FamilyMember>() : family.FamilyMembers;
    }

    public async Task<FamilyMember> GetFamilyMemberByUserIdAsync(Guid userInfoId, Guid familyId)
    {
        return await _context.FamilyMember
            .Where(fm => fm.FamilyId == familyId && fm.UserId == userInfoId)
            .FirstOrDefaultAsync();
    }

    public IEnumerable<FamilyMember> GetAllFamilyMembersAsync(Domain.Entities.Family family)
    {
        return _context.FamilyMember
            .Where(m => family.FamilyMembers.Contains(m))
            .AsEnumerable();
    }
}