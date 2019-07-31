using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isolani.Model 
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ChessClubId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Country { get; set; }

        [Required]
        public int BirthYear { get; set; }

        public int? StandardRating { get; set; }
        
        public int? RapidRating { get; set; }
        
        public int? BlitzRating { get; set; }
        
        [ForeignKey(nameof(ChessClubId))]
        public ChessClub ChessClub { get; set; }
        
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