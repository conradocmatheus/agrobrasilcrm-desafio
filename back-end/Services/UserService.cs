using AutoMapper;
using back_end.DTOs;
using back_end.Models;
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

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        // Atribui o usuário encontrado por id a uma variável(foundUser)
        var foundUser = await _userRepository.GetUserByIdAsync(id);
        // Retorna o usuário encontrado ou null, no formato de UserDto
        return foundUser == null ? null : _mapper.Map<UserDto>(foundUser);
    }

    public async Task<UserDto?> DeleteUserByIdAsync(Guid id)
    {
        // Atribui o usuário deletado por id a uma variável(deletedUser)
        var deletedUser = await _userRepository.DeleteUserByIdAsync(id);
        // Retorna o usuário deletado ou null, no formato de UserDto
        return deletedUser == null ? null : _mapper.Map<UserDto>(deletedUser);
    }

    // Método que verifica se o usuário é um adulto
    private static bool IsAdult(DateTime birthday)
    {
        var today = DateTime.Today;
        var userAge = today.Year - birthday.Year;

        if (birthday.Date > today.AddYears(-userAge)) userAge--;
        return userAge >= AgeOfMajority;
    }
}