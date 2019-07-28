using System;
using System.ComponentModel.DataAnnotations;

namespace Isolani.Model 
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
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }
    }
}