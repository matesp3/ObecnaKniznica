using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.DataModels
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now; // in system
    }
}
