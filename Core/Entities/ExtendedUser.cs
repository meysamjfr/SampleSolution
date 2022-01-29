using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities
{
    public class ExtendedUser : IdentityUser
    {
        [StringLength(maximumLength: 100)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string GetNickname => (this.FirstName + " " + this.LastName).Trim();
    }
}