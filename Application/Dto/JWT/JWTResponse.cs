namespace Application.Dto.JWT
{
    public class JWTResponse
    {
        public string JWT { get; set; }
        public JWTResponse()
        {
            
        }

        public JWTResponse(string jWT)
        {
            JWT = jWT;
        }
    }
}
