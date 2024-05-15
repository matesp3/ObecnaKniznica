namespace ObecnaKniznicaAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } // one-to-many
        public string Title { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now; // in system
    }
}
