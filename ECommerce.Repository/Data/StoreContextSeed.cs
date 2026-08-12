using System.Text.Json;
using ECommerce.Core.Models;

namespace ECommerce.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../ECommerce.Repository/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    await context.ProductBrands.AddRangeAsync(brands);
                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../ECommerce.Repository/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types?.Count > 0)
                {
                    await context.ProductTypes.AddRangeAsync(types);
                }
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../ECommerce.Repository/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    await context.Products.AddRangeAsync(products);
                }
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}