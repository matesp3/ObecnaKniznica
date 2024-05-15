﻿

namespace ObecnaKniznicaLogic.Models
{
    public class Author
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PrefixTitles { get; set; }
        public string? SuffixTitles { get; set; }
    }
}