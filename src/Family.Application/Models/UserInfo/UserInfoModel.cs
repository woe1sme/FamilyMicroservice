using Family.Application.Models.Base;

namespace Family.Application.Models.UserInfo
{
    public class UserInfoModel : IModel
    {
        public Guid Id { get; init; }

        public required string UserName { get; init; }
    }
}
