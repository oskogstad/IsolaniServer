using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isolani.Models 
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(ChessClub))]
        public Guid? ChessClubId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Country { get; set; }

        [Required]
        public int BirthYear { get; set; }

        public string Title { get; set; }
        public int? StandardRating { get; set; }
        
        public int? RapidRating { get; set; }
        
        public int? BlitzRating { get; set; }
        
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