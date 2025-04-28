using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext, 
                             UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager,
                             StoreIdentityDbContext _identityDbContext) : IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };

                    await _userManager.CreateAsync(user01, "P@ssword123");
                    await _userManager.CreateAsync(user02, "P@ssword123");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
