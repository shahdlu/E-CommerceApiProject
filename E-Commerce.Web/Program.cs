
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;
using E_Commerce.Web.Extentions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;


namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddIfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWeApplicationServices();

            #endregion

            var app = builder.Build();

            await app.SeedDataBaseAsync();

            #region Configure the HTTP request pipeline

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
