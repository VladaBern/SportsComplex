using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsComplex.Models;
using SportsComplex.Validators;
using System;
using System.Linq;

namespace SportsComplex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachController : Controller
    {
        private readonly SportsComplexDbContext context;
        private readonly IValidator<int> idValidator;
        private readonly IValidator<Coach> coachValidator;

        public CoachController(SportsComplexDbContext context, IValidator<int> idValidator, IValidator<Coach> coachValidator)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (idValidator == null)
                throw new ArgumentNullException(nameof(idValidator));
            if (coachValidator == null)
                throw new ArgumentNullException(nameof(coachValidator));

            this.context = context;
            this.idValidator = idValidator;
            this.coachValidator = coachValidator;
        }

        [HttpGet]
        public IActionResult GetList(bool includeRelation)
        {
            var result = context.Coachs.AsQueryable();

            if (includeRelation)
            {
                result = result.Include(x => x.Clients);
            }

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, bool includeRelation)
        {
            idValidator.Validate(id);

            var query = context.Coachs.AsQueryable();

            if (includeRelation)
            {
                query = query.Include(x => x.Clients);
            }

            var coach = query.FirstOrDefault(x => x.Id == id);

            if (coach == null)
                return NotFound();

            return Ok(coach);
        }


        [HttpPost]
        public IActionResult Create(Coach coach)
        {
            coachValidator.Validate(coach);

            context.Coachs.Add(coach);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = coach.Id }, coach);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Coach coach)
        {
            idValidator.Validate(id);

            coachValidator.Validate(coach);

            var coachFromDb = context.Coachs.FirstOrDefault(x => x.Id == id);

            if (coachFromDb == null)
                return NotFound();

            coachFromDb.Name = coach.Name;
            coachFromDb.Surname = coach.Surname;
            coachFromDb.DisciplineId = coach.DisciplineId;
            context.Coachs.Update(coachFromDb);
            context.SaveChanges();

            return Ok(coachFromDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            idValidator.Validate(id);

            var coach = context.Coachs.FirstOrDefault(x => x.Id == id);

            if (coach == null)
                return NotFound();

            context.Coachs.Remove(coach);
            context.SaveChanges();

            return NoContent();
        }
    }
}
