using back_end.Models.Entities;

namespace back_end.Repositories.Interfaces;

public interface IUserRepository
{
    List<User> ListAll();

    void Save(User user);

}