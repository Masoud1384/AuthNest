namespace Application.Dto.JWT
{
    public class JWTConfigMapperDto
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SECRET_KEY { get; set; }
    }
}
