using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isolani.Models
{
    public enum GameState
    {
        NotStarted,
        Postponed,
        BeingPlayed,
        WhiteWon,
        WhiteWonWalkover,
        BlackWon,
        BlackWonWalkover,
        DrawWalkover,
        Draw
    }
    
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PlayerWhiteId { get; set; }
        
        [Required]
        [ForeignKey(nameof(PlayerWhiteId))]
        public User PlayerWhite { get; set; }
        
        [Required]
        public Guid PlayerBlackId { get; set; }
        
        [Required]
        [ForeignKey(nameof(PlayerBlackId))]
        public User PlayerBlack { get; set; }
        
        [Required]
        public DateTime StartDateUtc { get; set; }
        
        [Required]
        [ForeignKey(nameof(Tournament))]
        public Guid TournamentId { get; set; }

        [Required]
        public GameState GameState { get; set; }
    }
}