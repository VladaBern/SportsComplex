using Microsoft.AspNetCore.Mvc;
using SportsComplex.Models;
using SportsComplex.Validators;
using System;
using System.Linq;

namespace SportsComplex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private SportsComplexDbContext context;
        private IValidator<int> idValidator;
        private IValidator<Client> clientValidator;

        public ClientController(SportsComplexDbContext context, IValidator<int> idValidator, IValidator<Client> clientValidator)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (idValidator == null)
                throw new ArgumentNullException(nameof(idValidator));
            if (clientValidator == null)
                throw new ArgumentNullException(nameof(clientValidator));

            this.context = context;
            this.idValidator = idValidator;
            this.clientValidator = clientValidator;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(context.Clients.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            idValidator.Validate(id);

            var client = context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }


        [HttpPost]
        public IActionResult Create(Client client)
        {
            clientValidator.Validate(client);

            context.Clients.Add(client);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Client client)
        {
            idValidator.Validate(id);

            clientValidator.Validate(client);

            var clientFromDb = context.Clients.FirstOrDefault(x => x.Id == id);

            if (clientFromDb == null)
                return NotFound();

            clientFromDb.Name = client.Name;
            clientFromDb.Surname = client.Surname;
            clientFromDb.DateOfBirth = client.DateOfBirth;
            clientFromDb.CoachId = client.CoachId;
            context.Clients.Update(clientFromDb);
            context.SaveChanges();

            return Ok(clientFromDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            idValidator.Validate(id);

            var client = context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
                return NotFound();

            context.Clients.Remove(client);
            context.SaveChanges();

            return NoContent();
        }
    }
}
