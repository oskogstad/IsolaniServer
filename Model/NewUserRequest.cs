using System;
using System.ComponentModel.DataAnnotations;

namespace Isolani.Model
{
    public class NewUserRequest : TokenRequest
    {
        public Guid? ChessClubId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Country { get; set; }

        [Required]
        public int BirthYear { get; set; }

        public int? StandardRating { get; set; }
        
        public int? RapidRating { get; set; }
        
        public int? BlitzRating { get; set; }
    }
}