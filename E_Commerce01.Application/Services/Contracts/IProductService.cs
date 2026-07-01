using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(ProductParams productParams ,CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(int id ,  CancellationToken ct = default);
        Task<Result<IReadOnlyList<ProductBrandDto>>> GetAllBrandAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<ProductTypeDto>>> GetAllTypeAsync(CancellationToken ct = default);

    }
}
