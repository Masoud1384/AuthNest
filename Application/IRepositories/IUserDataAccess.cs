using Application.Dto.User;
using Domain.Entities;

namespace Application.IRepositories
{
    public interface IUserDataAccess
    {
        Task InsertUser(User user);
        Task<User> GetUserBy(int userId);
    }
}
