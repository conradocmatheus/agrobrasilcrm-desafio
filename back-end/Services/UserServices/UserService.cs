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
        // Verifica se o usuário é maior de idade
        if (!IsAdult(createUserDto.Birthday))
        {
            throw new ArgumentException("Usuário deve ser maior de 18 anos", nameof(createUserDto));
        }

        // Mapeia o CreateUserDto para User
        var user = mapper.Map<User>(createUserDto);

        try
        {
            // Chama o repositório que salva o usuário no banco de dados
            await userRepository.CreateUserAsync(user);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao criar usuário.", e);
        }

        // Retorna o UserDto mapeado de user
        return mapper.Map<UserDto>(user);
    }

    // Atualizar usuário por ID
    public async Task<UserDto?> UpdateUserAsync(CreateUserDto createUserDto, Guid id)
    {
        try
        {
            // Mapeia de createUserDto pra User
            var toUpdateUser = mapper.Map<User>(createUserDto);

            // Atualiza toUpdateUser com o id fornecido
            var updatedUser = await userRepository.UpdateUserAsync(toUpdateUser, id);

            // Verifica se o usuário foi atualizado e retorna
            return updatedUser == null ? null : mapper.Map<UserDto>(updatedUser);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao atualizar usuário.", e);
        }
    }
    
    // Deletar usuário por ID
    public async Task<UserDto?> DeleteUserByIdAsync(Guid id)
    {
        try
        {
            // Atribui o usuário encontrado por id na variável user
            var user = await userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }
            
            if (user.Movements == null || user.Movements.Count > 0)
            {
                throw new InvalidOperationException("Usuário não pode ser deletado porque possui movimentações.");
            }

            // Atribui o usuário que passou pelas verificações uma variável(deletedUser)
            var deletedUser = await userRepository.DeleteUserByIdAsync(id);
            // Retorna o usuário deletado ou null, no formato de UserDto
            return mapper.Map<UserDto>(deletedUser);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // Mudar retorno para UserDto dps, ou criar outra DTO
    // Listar por data de criação
    public async Task<List<UserCreatedAtDto>> GetUsersByCreatedAtAsync()
    {
        try
        {
            // Atribui a lista que o método do repository retorna em uma var users
            var users = await userRepository.GetUsersByCreatedAtAsync();
            // E dps retorna a lista dos objetos user mapeados para Dto
            return mapper.Map<List<UserCreatedAtDto>>(users);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao obter lista de usuários", e);
        }
    }

    // Encontrar usuário por ID
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        try
        {
            // Atribui o usuário encontrado por id a uma variável(foundUser)
            var foundUser = await userRepository.GetUserByIdAsync(id);
            // Retorna o usuário encontrado ou null, no formato de UserDto
            return foundUser == null ? null : mapper.Map<UserDto>(foundUser);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao obter usuário.", e);
        }
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