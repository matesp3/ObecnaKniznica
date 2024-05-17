using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.DataModels
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Image { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        public int ReservedAmount { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now; // in system
    }
}
