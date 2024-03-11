using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain;
using Domain.Domain.Products;
using Domain.Entities.Products;

namespace Application.Maps;
internal class MappingProfile : Profile {

    public MappingProfile() {

        CreateMap<ProductResponseDto, Product>().ReverseMap();

        CreateMap<ProductsResponseDto, Product>()
            //.ForMember(dest => dest.Data.Price, opt => opt.MapFrom(src => new Money(src.Data.Currency, src.Data.Amount)))
            .ReverseMap();
    }
}
