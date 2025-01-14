using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyMemberRepository(FamilyDbContext context) : EfRepository<Domain.Entities.FamilyMember>(context), IFamilyMemberRepository
{
    public async Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(long familyId)
    {
        var family = await context.Family.FindAsync(familyId);
        
    }

    public async Task<IEnumerable<FamilyMember>> GetAllFamilyMembersAsync(Domain.Entities.Family family)
    {
        throw new NotImplementedException();
    }
}