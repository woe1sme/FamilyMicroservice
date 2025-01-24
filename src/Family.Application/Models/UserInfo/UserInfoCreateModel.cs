using Family.Application.Models.Base;

namespace Family.Application.Models.UserInfo
{
    public class UserInfoCreateModel : ICreateModel
    {
        public required string UserName { get; init; }
    }
}
