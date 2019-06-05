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
    public class WorkoutController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;

        public WorkoutController(GymLadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Workout/{id}/Sets
        [HttpGet("Sets/{id}")]
        public async Task<ActionResult<IEnumerable<SetDTO>>> GetSets(long id)
        {
            var workout = await _context.Workouts.FindAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var sets = _context.Sets
                .Where(s => s.WorkoutId == workout.Id);

            if (sets == null) {
                return NoContent();
            }
            var dto = await _mapper.ProjectTo<SetDTO>(sets).ToListAsync();
            return Ok(dto);
        }

        // GET: api/Workout
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts()
        {
            var workouts = await _mapper.ProjectTo<WorkoutDTO>(_context.Workouts).ToListAsync();
            return Ok(workouts);
        }

        // GET: api/Workout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(long id)
        {
            var workout = await _context.Workouts.FindAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<WorkoutDTO>(workout);

            return Ok(dto);
        }

        // PUT: api/Workout/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(long id, Workout workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        // POST: api/Workout
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = workout.Id }, workout);
        }

        // DELETE: api/Workout/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Workout>> DeleteWorkout(long id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<WorkoutDTO>(workout);

            return Ok(dto);
        }

        private bool WorkoutExists(long id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
