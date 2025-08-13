using Application.IRepositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;
        private readonly IUserDataAccess _userDataAccess;

        public IUserDataAccess UserDataAccess => _userDataAccess;

        public UnitOfWork(Context context, IUserDataAccess userDataAccess)
        {
            _context = context;
            _userDataAccess = userDataAccess;
        }

        public async Task<bool> SaveAllChanges()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
