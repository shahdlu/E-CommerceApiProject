using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                    .ForMember(dist => dist.BrandeName, option => option.MapFrom(src => src.ProductBrand.Name))
                    .ForMember(dist => dist.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                    .ForMember(dist => dist.PictureUrl, option => option.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
