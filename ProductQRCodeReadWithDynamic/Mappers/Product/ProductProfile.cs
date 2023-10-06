using AutoMapper;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Models.Product;

namespace ProductQRCodeReadWithDynamic.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, AddProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ReverseMap();
        }
    }
}
