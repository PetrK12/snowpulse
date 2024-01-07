using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.BusinessEntities;
using Domain.Entities.BusinessEntities.Identity;

namespace Application.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Product, ProductClientDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
    }
}