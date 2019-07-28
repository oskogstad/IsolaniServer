using System.ComponentModel.DataAnnotations;

namespace Isolani.Model
{
    public class TokenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; } 
    }
}