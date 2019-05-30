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
    public class ExerciseController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;

        public ExerciseController(GymLadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<ActionResult<Exercise>> GetExercise(long id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

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
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = exercise.Id }, exercise);
        }

        // DELETE: api/Exercise/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exercise>> DeleteExercise(long id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return exercise;
        }

        private bool ExerciseExists(long id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
