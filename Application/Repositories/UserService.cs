using Application.IRepositories;
using Domain.Entities;

namespace Application.Repositories
{
    public class UserService : IBaseApplicationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
