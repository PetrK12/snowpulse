using Domain.Entities;

namespace Domain.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> Get(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    Task Delete(int id);
    Task Update(T entity);
    Task Create(T entity);
}