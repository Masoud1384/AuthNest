using Application.Dto.JWT;
using Application.Dto.User;
using Domain.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Utility
{
    public class JWTManager : IBaseApplicationRepository
    {
        private readonly JWTConfigMapperDto _config;
        public JWTManager(IConfiguration configuration)
        {
            _config = configuration.GetSection("JWTConfig").Adapt<JWTConfigMapperDto>();
            _config.SECRET_KEY = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new Exception();
        }
        public JWTResponse GenerateToken(UserDto userDto)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(30);

            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDto.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userDto.UserName),
                new Claim("Role", userDto.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), 
                ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SECRET_KEY));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.Issuer ?? throw new InvalidOperationException("Issuer_not_found"),
                audience: _config.Audience ?? throw new InvalidOperationException("audience"),
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshToken = GenerateRefreshToken();
            return new(tokenString, refreshToken);
        }

        public static string GenerateRefreshToken()
        {
            var bytes = new byte[32];
            string token = string.Empty;
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }
            token = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Trim('=');
            return token;
        }
    }

}
