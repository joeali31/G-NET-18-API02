using AutoMapper;
using E_Commerce01.Application.DTOs.Baskets;
using E_Commerce01.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.MappingProfiles.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();

            // ReverseMap => it means to (CustomerBasket, CustomerBasketDto) and (CustomerBasketDto , CustomerBasket)
        }

    }
}
