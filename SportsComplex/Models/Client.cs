using System;

namespace SportsComplex.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Coach Coach { get; set; }
        public int? CoachId { get; set; }
    }
}
