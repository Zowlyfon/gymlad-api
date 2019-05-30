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
    public class SetController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;

        public SetController(GymLadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<ActionResult<Set>> GetSet(long id)
        {
            var @set = await _context.Sets.FindAsync(id);

            if (@set == null)
            {
                return NotFound();
            }

            return @set;
        }

        // PUT: api/Set/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(long id, Set @set)
        {
            if (id != @set.Id)
            {
                return BadRequest();
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
        public async Task<ActionResult<Set>> PostSet(Set @set)
        {
            _context.Sets.Add(@set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = @set.Id }, @set);
        }

        // DELETE: api/Set/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Set>> DeleteSet(long id)
        {
            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(@set);
            await _context.SaveChangesAsync();

            return @set;
        }

        private bool SetExists(long id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
