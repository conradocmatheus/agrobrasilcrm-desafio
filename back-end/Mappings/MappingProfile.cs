using AutoMapper;
using back_end.DTOs;
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
    }
}