using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base(p => (!brandId.HasValue || p.BrandId == brandId) && (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
