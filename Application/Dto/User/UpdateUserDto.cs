using System.Text.Json.Serialization;

namespace Application.Dto.User
{
    public class UpdateUserDto
    {
        [JsonIgnore]
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
