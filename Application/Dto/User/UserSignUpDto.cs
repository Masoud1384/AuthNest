using System.Text.Json.Serialization;

namespace Application.Dto.User
{
    public class UserSignUpDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
