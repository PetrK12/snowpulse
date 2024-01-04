using System.Text.Json;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class StoreDataSeed
{
    public static async Task SeedAsync(StoreDbContext context)
    {
        if (!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }
        
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastructure/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            context.ProductTypes.AddRange(types);
        }
        
        if (!context.Products.Any())
        {
            var productData = File.ReadAllText("../Infrastructure/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            context.Products.AddRange(products);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }    
}