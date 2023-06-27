using System.Collections.Generic;

namespace SportsComplex.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}
