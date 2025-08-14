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
        public static string GetSecretKey()
            => Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? string.Empty;
    }
}
