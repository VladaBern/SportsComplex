using Microsoft.AspNetCore.Mvc;
using SportsComplex.Models;
using SportsComplex.Validators;
using System;
using System.Linq;

namespace SportsComplex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplineController : Controller
    {
        private readonly SportsComplexDbContext context;
        private readonly IValidator<Discipline> disciplineValidator;
        private readonly IValidator<int> idValidator;

        public DisciplineController(SportsComplexDbContext context, IValidator<Discipline> disciplineValidator, IValidator<int> idValidator)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (disciplineValidator == null)
                throw new ArgumentNullException(nameof(disciplineValidator));
            if (idValidator == null)
                throw new ArgumentNullException(nameof(idValidator));

            this.context = context;
            this.disciplineValidator = disciplineValidator;
            this.idValidator = idValidator;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(context.Disciplines.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            idValidator.Validate(id);

            var discipline = context.Disciplines.FirstOrDefault(x => x.Id == id);

            if (discipline == null)
                return NotFound();

            return Ok(discipline);
        }

        [HttpPost]
        public IActionResult Create(Discipline discipline)
        {
            disciplineValidator.Validate(discipline);

            context.Disciplines.Add(discipline);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new {id = discipline.Id}, discipline);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Discipline discipline)
        {
            idValidator.Validate(id);

            disciplineValidator.Validate(discipline);

            var discFromDb = context.Disciplines.FirstOrDefault(x => x.Id == id);

            if (discFromDb == null)
                return NotFound();

            discFromDb.Name = discipline.Name;
            context.Disciplines.Update(discFromDb);
            context.SaveChanges();

            return Ok(discFromDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            idValidator.Validate(id);

            var discipline = context.Disciplines.FirstOrDefault(x => x.Id == id);

            if (discipline == null)
                return NotFound();

            context.Disciplines.Remove(discipline);
            context.SaveChanges();

            return NoContent();
        }
    }
}
