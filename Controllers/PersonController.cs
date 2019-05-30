using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymLad.Models;
using AutoMapper;

namespace GymLad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;

        public PersonController(GymLadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPeople()
        {
            var people = await _mapper.ProjectTo<PersonDTO>(_context.People).ToListAsync();
            return Ok(people);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(long id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<PersonDTO>(person);

            return Ok(dto);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, Person person)
        {
            if (id != person.id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Person
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(long id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(long id)
        {
            return _context.People.Any(e => e.id == id);
        }
    }
}
