using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserDataAccess : IUserDataAccess
    {
        private Context _dbContext;
        public UserDataAccess(Context db)
        {
            _dbContext = db;
        }

        public async Task<User> GetUserBy(Expression<Func<User, bool>> request)
        {
            var result = await _dbContext.users.FirstOrDefaultAsync(request);
            return result;
        }


        public async Task InsertUser(User user)
        {
            try
            {
                await _dbContext.users.AddAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                _dbContext.users.Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
