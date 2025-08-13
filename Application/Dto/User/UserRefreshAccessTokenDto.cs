

using System.Text.Json.Serialization;

namespace Application.Dto.User
{
    public class UserRefreshAccessTokenDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public string AccessToken { get; set; }
    }
}
