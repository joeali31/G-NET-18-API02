using E_Commerce01.Application.Common;
using E_Commerce01.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product , int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductParams productParams) : base
            (
                p => 
                (!productParams.brandId.HasValue || p.BrandId == productParams.brandId)
                &&
                (!productParams.typeId.HasValue || p.TypeId == productParams.typeId)
                &&
                (string.IsNullOrWhiteSpace(productParams.searchValue) || p.Description.Contains(productParams.searchValue))
            )
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (productParams.sort)
            {
                case ProductSorting.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSorting.NameDesc: 
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSorting.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSorting.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPaging(productParams.pageIndex , productParams.pageSize);
        }


        public ProductWithBrandAndTypeSpecifications(int id) : base
            (
                p => p.Id == id
            )
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }


    }
}
