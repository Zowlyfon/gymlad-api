using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GymLad.Models;
using AutoMapper;

namespace GymLad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Person> _userManager;

        private readonly IAuthorizationService _authorisationService;

        public ExerciseController(GymLadContext context, IMapper mapper, UserManager<Person> userManager, IAuthorizationService authorisationService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _authorisationService = authorisationService;
        }

        // GET: api/Exercise/Person/
        [HttpGet("Person")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetPersonExercises()
        {
            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var exercises = await _mapper.ProjectTo<ExerciseDTO>(_context.Exercises.Where(e => e.PersonId == person.Id)).ToListAsync();
            return Ok(exercises);
        }

        // GET: api/Exercise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            var exercises = await _mapper.ProjectTo<ExerciseDTO>(_context.Exercises).ToListAsync();
            return Ok(exercises);
        }

        // GET: api/Exercise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExercise(long id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, exercise.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (exercise == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ExerciseDTO>(exercise);

            return Ok(dto);
        }

        // PUT: api/Exercise/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(long id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            var person = await _context.People.FindAsync(exercise.PersonId);

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/Exercise
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> PostExercise(Exercise exercise)
        {
            var person = await _context.People.FindAsync(exercise.PersonId);

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<ExerciseDTO>(exercise);

            return CreatedAtAction("GetExercise", new { id = dto.Id }, dto);
        }

        // DELETE: api/Exercise/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExerciseDTO>> DeleteExercise(long id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, exercise.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<ExerciseDTO>(exercise);

            return dto;
        }

        private bool ExerciseExists(long id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
