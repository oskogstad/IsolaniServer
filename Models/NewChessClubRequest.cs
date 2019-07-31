using System.ComponentModel.DataAnnotations;

namespace Isolani.Models
{
    public class NewChessClubRequest
    {
        [Required]
        public string Name { get; set; }   
    }
}