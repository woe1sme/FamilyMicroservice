using Family.Domain.Entities;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;
using Family.Infrastructure.Repositories.Implementations.Exceptions;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfFamilyMemberRepository(FamilyDbContext context) : EfRepository<Domain.Entities.FamilyMember>(context), IFamilyMemberRepository
{
    public async Task<IEnumerable<FamilyMember>> GetAllMembersByFamilyIdAsync(long familyId)
    {
        var family = await context.Family.FindAsync(familyId);
        if (family is null)
            throw new FamilyNotFoundException(familyId);
            
        return family.FamilyMembers.AsEnumerable();
    }

    public IEnumerable<FamilyMember> GetAllFamilyMembersAsync(Domain.Entities.Family family)
    {
        return context.FamilyMember
            .Where(m => family.FamilyMembers.Contains(m))
            .AsEnumerable();
    }
}