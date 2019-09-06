using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isolani.Models
{
    public class Tournament
    {
        [Key]
        public Guid Id{ get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(ChessClub))]
        public Guid ArrangingChessClubId { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}