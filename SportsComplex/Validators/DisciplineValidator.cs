using SportsComplex.Exceptions;
using SportsComplex.Models;

namespace SportsComplex.Validators
{
    public class DisciplineValidator : IValidator<Discipline>
    {
        public void Validate(Discipline discipline)
        {
            if (discipline == null)
                throw new InvalidDataException("Discipline shouldn't be null");
            if (string.IsNullOrWhiteSpace(discipline.Name))
                throw new InvalidDataException("Name cannot be empty");
            if (discipline.Name.Length < 3)
                throw new InvalidDataException("Name must be longer than 3 symbols");
            if (discipline.Name.Length > 40)
                throw new InvalidDataException("Name must be shorter than 40 symbols");
        }
    }
}
