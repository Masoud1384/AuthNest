using Application.Dto.JWT;
using Application.Dto.User;
using Application.ICommonInterfaces;
using Application.IRepositories;
using Application.Utility;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories
{
    public class UserAuth : IBaseApplicationRepository
    {
        private readonly IConfiguration _config;
        private readonly IJWTManager<UserDto> _jwt;
        private readonly IUnitOfWork _uow;
        public UserAuth(IConfiguration config, IJWTManager<UserDto> jwt, IUnitOfWork uow)
        {
            _config = config;
            _jwt = jwt;
            _uow = uow;
        }
        public async Task<Response<JWTResponse>> SignUpUser(UserSignUpDto request)
        {
            // because this project is not real and has no real business logic or something
            // we actually don't need all kind of validations here 
            // validations such as checking existing valid token or other thnigs i don't have in mind right now

            var existingUser = await _uow.UserDataAccess.GetUserBy(t => t.UserName == request.UserName);
            if (existingUser != null)
                return Response<JWTResponse>.Failure("UserExists");

            var result = request.ValidateUser();
            if (!result.IsSuccessed)
                return result;

            User user = request.Adapt<User>();
            user.Password = PasswordHasher.Hash(request.Password);
            user.Role = Roles.User.ToString();
            await _uow.UserDataAccess.InsertUser(user);
            var saveResult = await _uow.SaveAllChanges();

            if (!saveResult)
                return Response<JWTResponse>.Failure("SomethingWentWrong");

            var jwtConfig = new JWTConfigMapperDto
            {
                Audience = _config["JWTConfig:Audience"] ?? string.Empty,
                Issuer = _config["JWTConfig:Issuer"] ?? string.Empty,
                ExpTime = Convert.ToInt32(_config["JWTConfig:ExpTime"]),
                SECRET_KEY = JWTHelper.GetSecretKey()
            };
            var userDto = request.Adapt<UserDto>();
            userDto.UserId = user.UserId;
            var accessToken = _jwt.GenerateToken(userDto, jwtConfig);

            return Response<JWTResponse>.Succeeded(accessToken);
        }

    }
}
