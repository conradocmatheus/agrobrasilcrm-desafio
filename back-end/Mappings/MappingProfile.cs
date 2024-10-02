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
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.UtcDateTime));
        CreateMap<User, UserCreatedAtDto>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<UpdateProductDto, ProductDto>().ReverseMap();
    }
}