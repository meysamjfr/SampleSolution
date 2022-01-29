using System.ComponentModel;

namespace Project.DTOs.ExtendedUser
{
    public class LoginExtendedUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
