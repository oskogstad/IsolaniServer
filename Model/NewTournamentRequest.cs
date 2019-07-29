using System.ComponentModel.DataAnnotations;

namespace Isolani.Model
{
    public class NewTournamentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}