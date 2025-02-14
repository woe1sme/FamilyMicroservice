namespace Family.Application.Abstractions
{
    public interface IUserContextService
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}
