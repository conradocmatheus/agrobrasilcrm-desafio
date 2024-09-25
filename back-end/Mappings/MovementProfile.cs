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
    }
}