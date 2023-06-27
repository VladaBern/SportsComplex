using System.Collections.Generic;

namespace SportsComplex.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Coach> Coaches { get; set; }
    }
}
