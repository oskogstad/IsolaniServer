using System.ComponentModel.DataAnnotations;

namespace Isolani.Model
{
    public class NewChessClubRequest
    {
        [Required]
        public string Name { get; set; }   
    }
}