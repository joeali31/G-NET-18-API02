using AutoMapper;
using E_Commerce01.Application.DTOs.Products;
using E_Commerce01.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.MappingProfiles.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.ProductType, opt => opt.MapFrom(S => S.Type.Name))
                .ForMember(D => D.ProductBrand, opt => opt.MapFrom(S => S.Brand.Name))
                .ForMember(D => D.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand , ProductBrandDto>();
            CreateMap<ProductType , ProductTypeDto>();
        }
    }
}
