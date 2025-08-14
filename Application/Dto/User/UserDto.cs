using Application.ICommonInterfaces;
using Domain.Enums;

namespace Application.Dto.User
{
    public class UserDto : IJWTGenerateTokenModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
