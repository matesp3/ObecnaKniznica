
using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.Models
{
    public class Right
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int BookId { get; set; }
        public Author Author { get; set; } // .Include( e => e.Name)
        public Book Book { get; set; }    //  .ThenInclude( d => d.Title)
    }
}
