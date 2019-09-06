using System;
using System.ComponentModel.DataAnnotations;

namespace Isolani.Models 
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedDateUtc { get; set; }

        [Required]
        public DateTime LastLoginDateUtc { get; set; }
    }
}