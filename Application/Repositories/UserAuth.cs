using Application.Dto.JWT;
using Application.Dto.User;
using Application.IRepositories;
using Application.Utility;
using Domain.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories
{
    public class UserAuth : IBaseApplicationRepository
    {
        private readonly IConfiguration _config;
        private readonly JWTManager _jwt;
        private readonly IUnitOfWork _uow;
        public UserAuth(IConfiguration config, JWTManager jwt, IUnitOfWork uow)
        {
            _config = config;
            _jwt = jwt;
            _uow = uow;
        }
        public async Task<JWTResponse> LoginUser(UserLoginDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshTokenDto> Refresh(UserRefreshAccessTokenDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<JWTResponse>> SignUpUser(UserSignUpDto request)
        {
            // because this project is not real and has no real business logic or something
            // we actually don't need all kind of validations here 
            var result = request.ValidateUser();
            if (!result.IsSuccessed)
                return result;

            User user = request.Adapt<User>();
            user.Password = PasswordHasher.Hash(request.Password);
            user.Role = Roles.User.ToString();
            await _uow.UserDataAccess.InsertUser(user);
            var res = await _uow.SaveAllChanges();
            if (res)
            {
                UserDto userDto = request.Adapt<UserDto>();
                var token = _jwt.GenerateToken(userDto);
                var response = Response<JWTResponse>.Succeeded();
                response.Item = token;
                return response;
            }
            return Response<JWTResponse>.Failure("SomethingWentWrong");
        }
    }
}
