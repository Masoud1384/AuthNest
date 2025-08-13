namespace Application.Dto.JWT
{
    public class JWTResponse
    {
        public string JWT { get; set; }
        public string RefreshToken { get; set; }
        public JWTResponse()
        {
            
        }

        public JWTResponse(string jWT, string refreshToken)
        {
            JWT = jWT;
            RefreshToken = refreshToken;
        }
    }
}
