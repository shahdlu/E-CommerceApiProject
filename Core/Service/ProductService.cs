using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specification;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await repo.GetAllAsync(specifications);
            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productCount = products.Count();
            var totalOfCount = await repo.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, productCount, totalOfCount, data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await repo.GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return typesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id); 
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
