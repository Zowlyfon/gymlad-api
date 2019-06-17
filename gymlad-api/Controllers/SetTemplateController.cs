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
    public class SetTemplateController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorisationService;

        public SetTemplateController(GymLadContext context, IMapper mapper, IAuthorizationService authorisationService)
        {
            _context = context;
            _mapper = mapper;
            _authorisationService = authorisationService;
        }

        // GET: api/SetTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetTemplateDTO>>> GetSetTemplates()
        {
            var setTemplates = await _mapper.ProjectTo<SetTemplateDTO>(_context.SetTemplates).ToListAsync();
            return Ok(setTemplates);
        }

        // GET: api/SetTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetTemplateDTO>> GetSetTemplate(long id)
        {
            var setTemplate = await _context.SetTemplates.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, setTemplate.WorkoutTemplate.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (setTemplate == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<SetTemplateDTO>(setTemplate);

            return Ok(dto);
        }

        // PUT: api/SetTemplate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetTemplate(long id, SetTemplate setTemplate)
        {
            if (id != setTemplate.Id)
            {
                return BadRequest();
            }

            var workout = await _context.WorkoutTemplates.FindAsync(setTemplate.WorkoutTemplateId);
            if (workout == null) {
                return BadRequest();
            }
            var person = await _context.People.FindAsync(workout.PersonId);

            var exercise = await _context.Exercises.FindAsync(setTemplate.ExerciseId);
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

            _context.Entry(setTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetTemplateExists(id))
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

        // POST: api/SetTemplate
        [HttpPost]
        public async Task<ActionResult<SetTemplateDTO>> PostSetTemplate(SetTemplate setTemplate)
        {
            var workout = await _context.WorkoutTemplates.FindAsync(setTemplate.WorkoutTemplateId);
            if (workout == null) {
                return BadRequest();
            }
            var person = await _context.People.FindAsync(workout.PersonId);

            var exercise = await _context.Exercises.FindAsync(setTemplate.ExerciseId);
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

            _context.SetTemplates.Add(setTemplate);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SetTemplateDTO>(setTemplate);

            return CreatedAtAction("GetSetTemplate", new { id = setTemplate.Id }, dto);
        }

        // DELETE: api/SetTemplate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SetTemplateDTO>> DeleteSetTemplate(long id)
        {
            var setTemplate = await _context.SetTemplates.FindAsync(id);

            var authResult = await _authorisationService.AuthorizeAsync(User, setTemplate.WorkoutTemplate.Person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            if (setTemplate == null)
            {
                return NotFound();
            }

            _context.SetTemplates.Remove(setTemplate);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<SetTemplateDTO>(setTemplate);

            return dto;
        }

        private bool SetTemplateExists(long id)
        {
            return _context.SetTemplates.Any(e => e.Id == id);
        }
    }
}
