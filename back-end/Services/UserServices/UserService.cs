using AutoMapper;
using back_end.DTOs.UserDTOs;
using back_end.Models;
using back_end.Repositories.UserRepositories;

namespace back_end.Services.UserServices;

public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
{
    private const int AgeOfMajority = 18;

    // Criar usuário
    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (!IsAdult(createUserDto.Birthday))
        {
            throw new ArgumentException("Usuário deve ser maior de 18 anos.");
        }

        var user = mapper.Map<User>(createUserDto);
        await userRepository.CreateUserAsync(user);
        return mapper.Map<UserDto>(user);
    }

    // Atualizar usuário por ID
    public async Task<UserDto?> UpdateUserAsync(CreateUserDto createUserDto, Guid id)
    {
        if (!IsAdult(createUserDto.Birthday))
        {
            throw new ArgumentException("Usuário deve ser maior de 18 anos.");
        }

        var toUpdateUser = mapper.Map<User>(createUserDto);
        var updatedUser = await userRepository.UpdateUserAsync(toUpdateUser, id);
        return updatedUser == null ? null : mapper.Map<UserDto>(updatedUser);
    }

    // Deletar usuário por ID
    public async Task<UserDto?> DeleteUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new InvalidOperationException("Usuário não encontrado.");
        }

        // Se o Usuário tiver feito alguma movimentação, ele não poderá ser deletado
        if (user.Movements == null || user.Movements.Count > 0)
        {
            throw new InvalidOperationException("Usuário não pode ser deletado porque possui movimentações.");
        }
        
        var deletedUser = await userRepository.DeleteUserByIdAsync(id);
        return mapper.Map<UserDto>(deletedUser);
    }

    // Mudar retorno para UserDto dps, ou criar outra DTO
    // Listar por data de criação
    public async Task<List<UserCreatedAtDto>> GetUsersByCreatedAtAsync()
    {
        var users = await userRepository.GetUsersByCreatedAtAsync();
        return mapper.Map<List<UserCreatedAtDto>>(users);
    }

    // Encontrar usuário por ID
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var foundUser = await userRepository.GetUserByIdAsync(id);
        return foundUser == null ? null : mapper.Map<UserDto>(foundUser);
    }

    // Método que verifica se o usuário é um adulto
    private static bool IsAdult(DateTimeOffset birthday)
    {
        var today = DateTime.Today;
        var userAge = today.Year - birthday.Year;

        if (birthday.Date > today.AddYears(-userAge)) userAge--;
        return userAge >= AgeOfMajority;
    }
}