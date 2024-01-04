using Domain.Entities;
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

    public async Task<T> Get(int id) => await _context.Set<T>().FindAsync(id);


    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();


    public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => 
        await ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) =>
        await ApplySpecification(spec).ToListAsync();

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }


}