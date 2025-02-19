using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore.Storage;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly FamilyDbContext _dbContext;
    private IDbContextTransaction _transaction;
    public IFamilyRepository FamilyRepository { get; }
    public IFamilyMemberRepository FamilyMemberRepository { get; }
    
    public EfUnitOfWork(FamilyDbContext dbContext,
        IFamilyRepository familyRepository,
        IFamilyMemberRepository familyMemberRepository)
    {
        _dbContext = dbContext;
        FamilyRepository = familyRepository;
        FamilyMemberRepository = familyMemberRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if(_transaction == null)
            _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if(_transaction != null)
        {
            await _transaction.CommitAsync();
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if(_transaction != null)
        {
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }
    }
    
    private async Task DisposeTransactionAsync()
    {
        if(_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}