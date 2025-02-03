using Family.Domain.Entities.Base;

namespace Family.Domain.Entities
{
    public class UserInfo(Guid id) : Entity<Guid>(id)
    {
        public string UserName { get; set; }
    }
}
