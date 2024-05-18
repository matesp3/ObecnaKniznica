using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(2)]
        public string LastName { get; set; } = string.Empty;
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }

        public List<Book>? Books { get; set; }
    }
}
