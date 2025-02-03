namespace Family.Domain.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IFamilyRepository FamilyRepository { get; }
    IFamilyMemberRepository FamilyMemberRepository { get; }
    IUserInfoRepository UserInfoRepository { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}