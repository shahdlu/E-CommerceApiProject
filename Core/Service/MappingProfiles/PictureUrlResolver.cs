using AutoMapper;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                //var Url = $"https://localhost:7047/{source.PictureUrl}";
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url ;
            }
        }
    }
}
