using Domain.Entities.BusinessEntities;
using Domain.Repository;
using Infrastructure.DataAccess.Specification;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories;

public class StoreRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly StoreDbContext _context;

    public StoreRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
    public async Task<IReadOnlyList<T>> ListAllAsync() => await _context.Set<T>().ToListAsync();
    
    public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => 
        await ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) =>
        await ApplySpecification(spec).ToListAsync();

    public async Task<int> CountAsync(ISpecification<T> spec) => await ApplySpecification(spec).CountAsync();
    public void Delete(T entity) => _context.Set<T>().Remove(entity);
    public void Create(T entity) => _context.Set<T>().Add(entity);
    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    
    private IQueryable<T> ApplySpecification(ISpecification<T> spec) => 
        SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    


}