using E_Commerce01.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Domain.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        // ==== RelationShips ====
        public ProductType Type { get; set; } = default!;
        public int TypeId { get; set; }

        public ProductBrand Brand { get; set; } = default!;
        public int BrandId { get; set; }
    }
}
