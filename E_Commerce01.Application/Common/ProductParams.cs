using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Common
{
    public class ProductParams 
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? searchValue { get; set; }
        public ProductSorting? sort { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;
    }

    public enum ProductSorting
    {
        None =0,
        NameAsc =1,
        NameDesc =2,
        PriceAsc =3, 
        PriceDesc =4,
    }
}
