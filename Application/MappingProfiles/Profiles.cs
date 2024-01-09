using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.BusinessEntities;
using Domain.Entities.OrderAggregate;

namespace Application.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Product, ProductClientDto>()
            .ForMember(d => d.ProductBrand,
                o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType,
                o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl,
                o => o.MapFrom<ProductUrlResolver>());
        CreateMap<Domain.Entities.BusinessEntities.Identity.Address, AddressDto>().ReverseMap();
        CreateMap<AddressDto, Address>();
        CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, 
                o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice,
                o => o.MapFrom(s => s.DeliveryMethod.Price));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId,
                o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName,
                o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl,
                o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(d => d.PictureUrl,
                o => o.MapFrom<OrderItemUrlResolver>());
    }
}