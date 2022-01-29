using System;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.Base;

namespace Project.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public int VerificationCode { get; set; } = 0;

        public string GetNickName()
        {
            return (this.FirstName + " " + this.LastName).Trim();
        }
    }
}