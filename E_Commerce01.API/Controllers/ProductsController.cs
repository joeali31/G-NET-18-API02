using E_Commerce01.API.Attributes;
using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Products;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce01.API.Controllers
{

    public class ProductsController(IProductService productService) : ApiBaseController
    {
        [HttpGet]
        [RedisCache(100)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery]ProductParams productParams , CancellationToken ct = default)
        {
            var result = await productService.GetAllProductAsync(productParams , ct);
            return ToActionResult(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeDto>>> GetAllTypes(CancellationToken ct = default)
        {
            var result = await productService.GetAllTypeAsync(ct);
            return ToActionResult(result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrandDto>>> GetAllBrands(CancellationToken ct = default)
        {
            var result = await productService.GetAllBrandAsync(ct);
            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id , CancellationToken ct = default)
        {
            var result = await productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }

    }
}
