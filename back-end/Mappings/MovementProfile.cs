using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Models;

namespace back_end.Mappings;

public class MovementProfile : Profile
{
    public MovementProfile()
    {
        // Mapeamento de CreateMovementDto para Movement
        CreateMap<CreateMovementDto, Movement>()
            .ForMember(dest => dest.MovementProducts, opt => opt.Ignore()); // Produtos serão mapeados separadamente

        // Mapeamento de Movement para MovementDto
        CreateMap<Movement, MovementDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                src.MovementProducts.Select(mp => new MovementProductDto
                {
                    Id = mp.ProductId,
                    Quantity = mp.Quantity
                }).ToList()));
        
        // Mapeamento de MovementProduct para MovementProductDto
        CreateMap<MovementProduct, MovementProductDto>();
        
        // Mapping pro GetAllMovements
        CreateMap<Movement, GetAllMovementsDto>()
            .ForMember(dest => dest.MovementProductIds, opt => opt.MapFrom(src => src.MovementProducts.Select(mp => mp.ProductId)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        
        // Teste
        CreateMap<Movement, GetAllMovementsWithUserInfoDto>()
            .ForMember(dest => dest.MovementProductIds, opt => opt.MapFrom(src => src.MovementProducts.Select(mp => mp.ProductId)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)); // Mapeia o User
        
        CreateMap<User, UserInfoDto>(); // Mapeia as informações do usuário
    }
}