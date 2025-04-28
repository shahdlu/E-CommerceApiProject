using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;

namespace E_Commerce.Web.Extentions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var objectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectOfDataSeeding.DataSeedAsync();
            await objectOfDataSeeding.IdentityDataSeedAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
