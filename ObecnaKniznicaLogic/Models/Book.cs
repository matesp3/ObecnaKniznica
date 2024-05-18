using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.Models
{
    public class Book
    {
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Titul knihy je povinný!")]
        [MinLength(2, ErrorMessage = "Titul knihy musí obsahovať aspoň 2 znaky!")]
        public string Title { get; set; } = String.Empty;
        [Required(ErrorMessage = "Celkové množstvo kníh je povinné!")]
        [Range(1, Double.MaxValue, ErrorMessage = "Celkové množstvo musí byť viac ako 0!")]
        public int TotalAmount { get; set; } = 0;
        [Required]
        public int ReservedAmount { get; set; } = 0;
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now; // in system

        public List<Author>? Authors { get; set; }
    }
}
