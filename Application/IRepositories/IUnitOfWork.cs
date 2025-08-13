namespace Application.IRepositories
{
    public interface IUnitOfWork
    {
        IUserDataAccess UserDataAccess { get; }
        Task<bool> SaveAllChanges();
    }
}
