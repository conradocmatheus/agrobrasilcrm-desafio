using AutoMapper;
using back_end.DTOs;
using back_end.DTOs.MovementDTOs;
using back_end.DTOs.ProductDTOs;
using back_end.DTOs.UserDTOs;
using back_end.Models;

namespace back_end.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<User, UserCreatedAtDto>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<UpdateProductDto, ProductDto>().ReverseMap();

        // Mapeamento entre CreateMovementDto e Movement
        CreateMap<CreateMovementDto, Movement>()
            .ForMember(dest => dest.MovementProducts, opt => opt.MapFrom(src =>
                src.Products.Select(p => new MovementProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                })));

        // Mapeamento entre Movement e MovementDto
        CreateMap<Movement, MovementDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                src.MovementProducts.Select(mp => new ProductDto
                {
                    Id = mp.Product.Id, // Acesse o ID do produto
                    Name = mp.Product.Name, // Acesse o nome do produto
                    Quantity = mp.Quantity, // Quantidade do MovementProduct
                    Price = mp.Product.Price // Acesse o preço do produto
                }).ToList()));
    }
}