using System;
using Project.DTOs.Base;

namespace Project.DTOs.ApplicationUsers
{
    public class ApplicationUserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get { return (FirstName + " " + LastName).Trim(); } }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
    }
}
