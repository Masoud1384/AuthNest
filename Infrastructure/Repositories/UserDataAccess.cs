using Application.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserDataAccess : IUserDataAccess
    {
        private Context _dbContext;
        public UserDataAccess(Context db)
        {
            _dbContext = db;
        }

        public async Task<User> GetUserBy(int userId)
        {
            var result = await _dbContext.users.FindAsync(userId);
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
    }
}
