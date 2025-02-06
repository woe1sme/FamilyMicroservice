using Family.Domain.Entities.Base;

namespace Family.Domain.Entities
{
    public class UserInfo(Guid id, string userName) : Entity<Guid>(id)
    {
        public string UserName { get; set; } = userName;
    }
}
