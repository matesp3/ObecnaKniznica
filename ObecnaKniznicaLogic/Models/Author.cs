using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Meno musí obsahovať aspoň 2 znaky!")]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(2, ErrorMessage = "Priezvisko musí obsahovať aspoň 2 znaky!")]
        public string LastName { get; set; } = string.Empty;
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }

        public List<Book>? Books { get; set; }
    }
}
