using Domain.Entities.BusinessEntities;
using Domain.Repository;

namespace Domain;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
}