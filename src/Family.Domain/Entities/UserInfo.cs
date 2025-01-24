using Family.Domain.Entities.Base;

namespace Family.Domain.Entities
{
    public class UserInfo(long id) : Entity(id)
    {
        public string UserName { get; set; }
    }
}
