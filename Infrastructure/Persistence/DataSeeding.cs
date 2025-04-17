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
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productsBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert data "string" => C# objects (ProductBrand)
                    var productsBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productsBrandData);
                    if (productsBrands != null && productsBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productsBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productsTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var productsTypes = JsonSerializer.Deserialize<List<ProductType>>(productsTypeData);
                    if (productsTypes != null && productsTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productsTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
