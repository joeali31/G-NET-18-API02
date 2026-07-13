using AutoMapper;
using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Baskets;
using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Domain.Contract.Repositories;
using E_Commerce01.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Calsses
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<Result<CustomerBasketDto>> CreateOrUpdateBasketAsync(CustomerBasketDto basketDto, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var result = await _basketRepository.CreateOrUpdateBasketAsync(basket, TimeToLive, ct);

            if (result is null) return Result<CustomerBasketDto>.Fail(Error.Failure("Failed.ToCreateOrUpdateBasket" , "Failed To Create Or Update Basket"));

            return Result<CustomerBasketDto>.Ok(basketDto);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket is null)
                return Result<bool>.Fail(Error.NotFound("Basket.NotFound" , $"Can not find basket with id {id}"));

            var result = await _basketRepository.DeleteBasketAsync(id, cancellationToken);
            return result ? Result<bool>.Ok(result) : Result<bool>.Fail(Error.Failure("Basket.Delete.Failure" , $"Can not delete basket with id {id}"));
        }

        public async Task<Result<CustomerBasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if(basket is null)
                return Result<CustomerBasketDto>.Fail(Error.NotFound("Basket.NotFound", $"Can not find basket with id {id}"));

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);

            return Result<CustomerBasketDto>.Ok(mappedBasket);
        }
    }
}
