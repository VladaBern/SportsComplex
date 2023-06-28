using SportsComplex.Exceptions;
using SportsComplex.Models;
using System.Linq;
using System;

namespace SportsComplex.Validators
{
    public class CoachValidator : IValidator<Coach>
    {
        private readonly IValidator<int> idValidator;
        private readonly SportsComplexDbContext context;

        public CoachValidator(IValidator<int> idValidator, SportsComplexDbContext context)
        {
            if (idValidator == null)
                throw new ArgumentNullException(nameof(idValidator));
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            this.idValidator = idValidator;
            this.context = context;
        }

        public void Validate(Coach coach)
        {
            if (coach == null)
                throw new InvalidDataException("Coach shouldn't be null");
            if (string.IsNullOrWhiteSpace(coach.Name))
                throw new InvalidDataException("Name cannot be empty");
            if (coach.Name.Length < 3)
                throw new InvalidDataException("Name must be longer than 3 symbols");
            if (coach.Name.Length > 30)
                throw new InvalidDataException("Name must be shorter than 30 symbols");
            if (coach.Surname.Length < 3)
                throw new InvalidDataException("Surname must be longer than 3 symbols");
            if (coach.Surname.Length > 40)
                throw new InvalidDataException("Surname must be shorter than 40 symbols");

            idValidator.Validate(coach.DisciplineId);

            if (!context.Disciplines.Any(x => x.Id == coach.DisciplineId))
                throw new InvalidDataException("There is no discipline with this id");
        }
    }
}
