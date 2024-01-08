using Domain.Entities.BusinessEntities;

namespace Domain.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void Delete(T entity);
    void Update(T entity);
    void Create(T entity);
}