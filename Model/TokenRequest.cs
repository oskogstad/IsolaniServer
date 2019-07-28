using System.ComponentModel.DataAnnotations;

namespace foo_chess_server.Model
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