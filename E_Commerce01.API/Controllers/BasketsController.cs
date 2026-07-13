using E_Commerce01.Application.DTOs.Baskets;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce01.API.Controllers
{

    public class BasketsController(IBasketService _basketService) : ApiBaseController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id, CancellationToken ct = default)
        {
            var result = await _basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto basket, CancellationToken ct = default)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket, TimeSpan.FromMinutes(60), ct);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id , CancellationToken ct = default)
        {
            var result = await _basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        } 
    }
}
