using AutoMapper;
using back_end.DTOs;
using back_end.Repositories;

namespace back_end.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    private const int AgeOfMajority = 18;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        // Verifica se o usuário é maior de idade
        if (!IsAdult(createUserDto.Birthday))
        {
            throw new Exception("Usuário deve ser maior de 18 anos");
        }

        // Mapeia o CreateUserDto para User
        var user = _mapper.Map<Models.User>(createUserDto);

        // Chama o repositório que salva o usuário no banco de dados
        await _userRepository.AddUserAsync(user);

        // Retorna o UserDto mapeado de user
        return _mapper.Map<UserDto>(user);

    }
    
    public async Task<List<UserCreatedAtDto>> GetUsersByCreatedAtAsync()
    {   
        // Coloca a lista que o método do repository retorna em uma var users
        var users = await _userRepository.GetUsersByCreatedAtAsync();

        // E dps retorna a lista dos objetos user mapeados para Dto
        return _mapper.Map<List<UserCreatedAtDto>>(users);
    }

    // Método que verifica se o usuário é um adulto
    private bool IsAdult(DateTime birthday)
    {
        var today = DateTime.Today;
        var userAge = today.Year - birthday.Year;

        if (birthday.Date > today.AddYears(-userAge)) userAge--;
        return userAge >= AgeOfMajority;
    }
}