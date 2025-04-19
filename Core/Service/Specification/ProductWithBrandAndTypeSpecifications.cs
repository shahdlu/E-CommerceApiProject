using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId, ProductSortingOptions sortingOption) : base(p => (!brandId.HasValue || p.BrandId == brandId) && (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch(sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
