using AutoMapper;
using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Products;
using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Application.Specifications;
using E_Commerce01.Domain.Contract;
using E_Commerce01.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Calsses
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        public async Task<Result<IReadOnlyList<ProductBrandDto>>> GetAllBrandAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync(ct);
            var brandsDto = mapper.Map<IReadOnlyList<ProductBrandDto>>(brands);

            return Result<IReadOnlyList<ProductBrandDto>>.Ok(brandsDto);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(ProductParams productParams, CancellationToken ct = default)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(productParams);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs , ct);
            var productsDto = mapper.Map<IReadOnlyList<ProductDto>>(products);

            return Result<IReadOnlyList<ProductDto>>.Ok(productsDto);
        }

        public async Task<Result<IReadOnlyList<ProductTypeDto>>> GetAllTypeAsync(CancellationToken ct = default)
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);
            var typesDto = mapper.Map<IReadOnlyList<ProductTypeDto>>(types);

            return Result<IReadOnlyList<ProductTypeDto>>.Ok(typesDto);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(id);

            var product = await unitOfWork.GetRepository<Product , int>().GetByIdAsync(specs, ct);

            if (product is null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound" , $"Product with id {id} is not found !"));
            
            var producDto = mapper.Map<ProductDto>(product);

            return Result<ProductDto>.Ok(producDto);
        }
    }
}
