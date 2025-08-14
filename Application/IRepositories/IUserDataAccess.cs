using Domain.Entities;
using System.Linq.Expressions;

namespace Application.IRepositories
{
    public interface IUserDataAccess
    {
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task<User> GetUserBy(Expression<Func<User,bool>> request);
    }
}
