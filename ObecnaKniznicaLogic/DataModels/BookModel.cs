using ObecnaKniznicaLogic.Models;
using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.DataModels
{
    public class BookModel
    {
        [Required]
        public string? Title { get; set; }
        public string? Image { get; set; }
 
        public int? AvailableAmount { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public List<Author>? Authors { get; set; }
    }
}
