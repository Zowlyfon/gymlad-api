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
    public class WorkoutTemplateController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorisationService;

        public WorkoutTemplateController(GymLadContext context, IMapper mapper, IAuthorizationService authorisationService)
        {
            _context = context;
            _mapper = mapper;
            _authorisationService = authorisationService;
        }

        // GET: api/WorkoutTemplate/5/Sets
        [HttpGet("{id}/Sets")]
        public async Task<ActionResult<IEnumerable<SetTemplateDTO>>> GetSets(long id)
        {
            var workoutTemplate = await _context.WorkoutTemplates.FindAsync(id);

            if (workoutTemplate == null)
            {
                return NotFound();
            }

            var setTemplates = _context.SetTemplates
                .Where(s => s.WorkoutTemplateId == workoutTemplate.Id);

            if (setTemplates == null) {
                return NoContent();
            }
            
            var dto = await _mapper.ProjectTo<SetTemplateDTO>(setTemplates).ToListAsync();
            return Ok(dto);
        }

        // GET: api/WorkoutTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutTemplateDTO>>> GetWorkoutTemplates()
        {
            var workoutTemplates = await _mapper.ProjectTo<WorkoutTemplateDTO>(_context.WorkoutTemplates).ToListAsync();
            return Ok(workoutTemplates);
        }

        // GET: api/WorkoutTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutTemplateDTO>> GetWorkoutTemplate(long id)
        {
            var workoutTemplate = await _context.WorkoutTemplates.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, workoutTemplate.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (workoutTemplate == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<WorkoutTemplateDTO>(workoutTemplate);

            return dto;
        }

        // PUT: api/WorkoutTemplate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutTemplate(long id, WorkoutTemplate workoutTemplate)
        {
            if (id != workoutTemplate.Id)
            {
                return BadRequest();
            }

            var authResult = await _authorisationService.AuthorizeAsync(User, workoutTemplate.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            _context.Entry(workoutTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutTemplateExists(id))
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

        // POST: api/WorkoutTemplate
        [HttpPost]
        public async Task<ActionResult<WorkoutTemplateDTO>> PostWorkoutTemplate(WorkoutTemplate workoutTemplate)
        {
            var person = await _context.People.FindAsync(workoutTemplate.PersonId);

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }
            
            _context.WorkoutTemplates.Add(workoutTemplate);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<WorkoutTemplateDTO>(workoutTemplate);

            return CreatedAtAction("GetWorkoutTemplate", new { id = workoutTemplate.Id }, dto);
        }

        // DELETE: api/WorkoutTemplate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkoutTemplateDTO>> DeleteWorkoutTemplate(long id)
        {
            var workoutTemplate = await _context.WorkoutTemplates.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, workoutTemplate.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (workoutTemplate == null)
            {
                return NotFound();
            }

            _context.WorkoutTemplates.Remove(workoutTemplate);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<WorkoutTemplateDTO>(workoutTemplate);

            return dto;
        }

        private bool WorkoutTemplateExists(long id)
        {
            return _context.WorkoutTemplates.Any(e => e.Id == id);
        }
    }
}
