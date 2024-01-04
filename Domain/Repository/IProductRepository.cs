using Domain.Entities;

namespace Domain.Repository;

public interface IProductRepository
{ 
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductAsync();
}