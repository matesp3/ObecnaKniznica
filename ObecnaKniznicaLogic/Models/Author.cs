namespace ObecnaKniznicaLogic.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }

        public List<Book>? Books { get; }
    }
}
