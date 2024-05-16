using System.ComponentModel.DataAnnotations;

namespace ObecnaKniznicaLogic.DataModels
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }
    }
}
