using Application.Dto.User;
using Domain.Enums;

namespace Application.ICommonInterfaces
{
    public interface IJWTGenerateTokenModel
    {
        string UserName { get;  }
        int UserId { get;  }
        Roles Role { get; }
    }
}
