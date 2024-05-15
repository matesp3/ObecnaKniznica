using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObecnaKniznicaLogic.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now; // in system
    }
}
