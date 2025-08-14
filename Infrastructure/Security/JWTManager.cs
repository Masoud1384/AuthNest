using Application.Dto.JWT;
using Application.ICommonInterfaces;
using Application.Utility;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Security
{
    // this class is supposed to generate all the users a token such as admins , users , customers and etc...
    // that is why it's generic
    public class JWTManager<T> : IJWTManager<T> where T : class, IJWTGenerateTokenModel
    {
        public JWTResponse GenerateToken(T dto, JWTConfigMapperDto config)
        {
            var bytes = config.SECRET_KEY.Bade64UrlDecode();
            if (bytes.Length < 32)
                throw new InvalidOperationException("InvalidException");

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(config.ExpTime);

            var jwtClaims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, dto.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, dto.UserName),
            new Claim("Role", dto.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(bytes);

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new JWTResponse(tokenString);
        }
        // some day i may use this and add refresh token to the project
        public string GenerateRefreshToken()
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
