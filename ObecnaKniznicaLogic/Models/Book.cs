
namespace ObecnaKniznicaLogic.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int TotalAmount { get; set; }
        public int ReservedAmount { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now; // in system
    }
}
