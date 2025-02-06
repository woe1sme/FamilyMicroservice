using Family.Infrastructure.EntityFramework;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfUserInfoRepository(FamilyDbContext context) : EfRepository<Domain.Entities.UserInfo, Guid>(context), IUserInfoRepository
{
    
}