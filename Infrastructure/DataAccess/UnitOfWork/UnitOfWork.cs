using System.Collections;
using Domain;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;
        
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(StoreRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(
                typeof(TEntity)), _context);
            
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}