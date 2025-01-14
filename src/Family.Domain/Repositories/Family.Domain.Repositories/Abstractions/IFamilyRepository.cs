using Family.Domain.Entities;

namespace Family.Domain.Repositories.Abstractions;

public interface IFamilyRepository : IRepository<Family.Domain.Entities.Family>
{
    public IEnumerable<Entities.Family> GetAllMemberFamilies(FamilyMember member);
}