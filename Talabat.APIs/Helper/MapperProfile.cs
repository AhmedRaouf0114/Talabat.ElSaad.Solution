using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductToReturnDtos>()
                .ForMember(D => D.Brand , O =>O.MapFrom(S => S.Brand.Name ))
                .ForMember(D => D.Category , O => O.MapFrom(S => S.Category.Name ))
                .ForMember(D => D.PictureUrl , O => O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDtos, CustomerBasket>();
            CreateMap<BasketItemDtos, BasketItem>();
        }
    }
}
