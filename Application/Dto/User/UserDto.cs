namespace Application.Dto.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
    public enum Roles
    {
        User,
        Admin,
    }
}
