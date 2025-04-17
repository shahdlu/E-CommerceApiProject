using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productsBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert data "string" => C# objects (ProductBrand)
                    var productsBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productsBrandData);
                    if (productsBrands != null && productsBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productsBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productsTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var productsTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productsTypeData);
                    if (productsTypes != null && productsTypes.Any())
                    {
                       await _dbContext.ProductTypes.AddRangeAsync(productsTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products != null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Go to Error Page
            }
        }
    }
}
