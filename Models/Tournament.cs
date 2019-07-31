using System;
using System.ComponentModel.DataAnnotations;

namespace Isolani.Models
{
    public class Tournament
    {
        [Key]
        public Guid Id{ get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}