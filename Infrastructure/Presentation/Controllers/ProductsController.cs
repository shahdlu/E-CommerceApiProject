using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManger) : ControllerBase
    {
        [HttpGet]
        //GET BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _serviceManger.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        //GET BaseUrl/api/Products/id
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("types")]
        //GET BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types = await _serviceManger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("brands")]
        //GET BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
    }
}
