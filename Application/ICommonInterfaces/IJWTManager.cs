using Application.Dto.JWT;

namespace Application.ICommonInterfaces
{
    public interface IJWTManager<T>
    {
        JWTResponse GenerateToken(T dto, JWTConfigMapperDto config);
    }
}
