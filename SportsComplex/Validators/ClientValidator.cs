using SportsComplex.Exceptions;
using SportsComplex.Models;
using System.Linq;
using System;

namespace SportsComplex.Validators
{
    public class ClientValidator : IValidator<Client>
    {
        private readonly IValidator<int> idValidator;
        private readonly SportsComplexDbContext context;

        public ClientValidator(IValidator<int> idValidator, SportsComplexDbContext context)
        {
            if (idValidator == null)
                throw new ArgumentNullException(nameof(idValidator));
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            this.idValidator = idValidator;
            this.context = context;
        }

        public void Validate(Client client)
        {
            if (client == null)
                throw new InvalidDataException("Client shouldn't be null");
            if (string.IsNullOrWhiteSpace(client.Name))
                throw new InvalidDataException("Name cannot be empty");
            if (client.Name.Length < 3)
                throw new InvalidDataException("Name must be longer than 3 symbols");
            if (client.Name.Length > 30)
                throw new InvalidDataException("Name must be shorter than 30 symbols");
            if (client.Surname.Length < 3)
                throw new InvalidDataException("Surname must be longer than 3 symbols");
            if (client.Surname.Length > 40)
                throw new InvalidDataException("Surname must be shorter than 40 symbols");
            if (client.DateOfBirth.Year < 1930)
                throw new InvalidDataException("Year of birth must be greater than 1930");
            if (client.DateOfBirth.Year > DateTime.Now.Year)
                throw new InvalidDataException("Year of birth must be less than " + DateTime.Now.Year);

            if (client.CoachId.HasValue)
            {
                idValidator.Validate(client.CoachId.Value);

                if (!context.Coachs.Any(x => x.Id == client.CoachId))
                    throw new InvalidDataException("There is no coach with this id");
            }
        }
    }
}
