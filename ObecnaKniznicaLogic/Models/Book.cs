
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace ObecnaKniznicaLogic.Models
{
    public class Book
    {
        public int Id { get; set; } = 0;
        [Required]
        [MinLength(2)]
        public required string Title { get; set; }
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public int TotalAmount { get; set; }
        [Required]
        public int ReservedAmount { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now; // in system

        public List<Author>? Authors { get; set; }
    }
}
