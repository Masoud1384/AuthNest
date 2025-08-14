using Application.Dto.User;
using Application.IRepositories;
using Application.Utility;

namespace Application.Repositories
{
    public class UserService : IBaseApplicationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // just implement a simple logic 
        public async Task<Response<bool>> UpdateUser(UpdateUserDto data)
        {
            var user = await _unitOfWork.UserDataAccess
                .GetUserBy(t => t.UserName == data.UserName);

            if (user == null)
                return Response<bool>.Failure("UserNotFound");

            // there are better ways but this is the simplest one right now 
            user.PhoneNumber = data.PhoneNumber;
            user.Email = data.Email;

            await _unitOfWork.UserDataAccess.UpdateUser(user);
            var result = await _unitOfWork.SaveAllChanges();
            return result ? Response<bool>.Succeeded() : Response<bool>.Failure("SomethingWentWrong");
        }
    }
}
