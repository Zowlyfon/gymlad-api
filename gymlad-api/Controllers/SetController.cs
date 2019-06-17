using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymLad.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace GymLad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SetController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorisationService;

        public SetController(GymLadContext context, IMapper mapper, IAuthorizationService authorisationService)
        {
            _context = context;
            _mapper = mapper;
            _authorisationService = authorisationService;
        }

        // GET: api/Set
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetDTO>>> GetSets()
        {
            var sets = await _mapper.ProjectTo<SetDTO>(_context.Sets).ToListAsync();
            return Ok(sets);
        }

        // GET: api/Set/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetDTO>> GetSet(long id)
        {
            var @set = await _context.Sets.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, @set.Workout.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (@set == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<SetDTO>(@set);

            return Ok(dto);
        }

        // PUT: api/Set/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(long id, Set @set)
        {
            if (id != @set.Id)
            {
                return BadRequest();
            }

            var workout = await _context.Workouts.FindAsync(@set.WorkoutId);
            if (workout == null) {
                return BadRequest();
            }
            var person = await _context.People.FindAsync(workout.PersonId);

            var exercise = await _context.Exercises.FindAsync(@set.ExerciseId);
            if (exercise == null) {
                return BadRequest();
            }
            var person2 = await _context.People.FindAsync(exercise.Person);

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");
            var authResult2 = await _authorisationService.AuthorizeAsync(User, person2, "SamePerson");

            if ((!authResult.Succeeded) || (!authResult2.Succeeded))
            {
                return new ForbidResult();
            }

            _context.Entry(@set).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Set
        [HttpPost]
        public async Task<ActionResult<SetDTO>> PostSet(Set @set)
        {
            var workout = await _context.Workouts.FindAsync(@set.WorkoutId);
            if (workout == null) {
                return BadRequest();
            }
            var person = await _context.People.FindAsync(workout.PersonId);

            var exercise = await _context.Exercises.FindAsync(@set.ExerciseId);
            if (exercise == null) {
                return BadRequest();
            }
            var person2 = await _context.People.FindAsync(exercise.PersonId);

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");
            var authResult2 = await _authorisationService.AuthorizeAsync(User, person2, "SamePerson");

            if ((!authResult.Succeeded) || (!authResult2.Succeeded))
            {
                return new ForbidResult();
            }

            _context.Sets.Add(@set);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SetDTO>(@set);

            return CreatedAtAction("GetSet", new { id = @set.Id }, dto);
        }

        // DELETE: api/Set/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SetDTO>> DeleteSet(long id)
        {
            var @set = await _context.Sets.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, @set.Workout.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (@set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(@set);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SetDTO>(@set);

            return Ok(dto);
        }

        private bool SetExists(long id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
