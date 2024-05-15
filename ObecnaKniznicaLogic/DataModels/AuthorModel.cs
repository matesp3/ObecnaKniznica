using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.DataModels
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }
    }
}
