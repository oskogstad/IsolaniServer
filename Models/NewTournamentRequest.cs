using System.ComponentModel.DataAnnotations;

namespace Isolani.Models
{
    public class NewTournamentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}