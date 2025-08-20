using System.IdentityModel.Tokens.Jwt;

namespace Application.Utility
{
    public static class JWTHelper
    {
        public static byte[] Bade64UrlDecode(this string str)
        {
            str = str.Replace('-', '+').Replace('_', '/');
            int m = str.Length & 3;
            if (m > 0)
                str += new string('=', 4 - m);
            return Convert.FromBase64String(str);
        }
        // placing the secret key here is intensly anti-pattern
        // but since you may not access my env var and want to use the project 
        // i guess it's ok to do this 
        // this app is not built for production
        public static string GetSecretKey()
            => Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ??
            "e7f456f567e09eee352f29ce95b0d02fe257afa06a6cbdde8db31ef07749dff5";


        public static string? GetClaimFromToken(string token, string claimType)
        {
            token = token.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var result = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            return result;
        }
    }
}
