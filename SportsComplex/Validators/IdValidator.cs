using SportsComplex.Exceptions;

namespace SportsComplex.Validators
{
    public class IdValidator : IValidator<int>
    {
        public void Validate(int id)
        {
            if (id < 1)
                throw new InvalidDataException("Id must be greater than zero");
        }
    }
}
