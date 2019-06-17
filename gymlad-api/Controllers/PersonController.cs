using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GymLad.Models;
using AutoMapper;

namespace GymLad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly GymLadContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly TokenManagement _tokenManagement;
        private readonly IAuthorizationService _authorisationService;

        public PersonController(GymLadContext context, 
                                IMapper mapper, 
                                UserManager<Person> userManager, 
                                SignInManager<Person> signInManager, 
                                IOptions<TokenManagement> tokenManagement, 
                                IAuthorizationService authorisationService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManagement = tokenManagement.Value;
            _authorisationService = authorisationService;
        }

        // POST: api/Person/Login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var claim = new[]
                {
                    new Claim(ClaimTypes.Name, login.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claim,
                    expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return Ok(token);
            }

            return BadRequest();
        }

        // GET: api/Person/Me
        [HttpGet("me")]
        public async Task<ActionResult<PersonDTO>> GetMe()
        {
            var person = await _userManager.FindByNameAsync(User.Identity.Name);

            if (person == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<PersonDTO>(person);

            return Ok(dto);
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

            var authResult = await _authorisationService.AuthorizeAsync(User, person, "SamePerson");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

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
            if (id != person.Id)
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostPerson(CreatePersonDTO person)
        {
            var user = _mapper.Map<Person>(person);
            var result = await _userManager.CreateAsync(user, person.Password);

            if (result.Succeeded)
            {
                var newUser =  await _userManager.FindByNameAsync(person.UserName);
                return CreatedAtAction("GetPerson", new { id = newUser.Id }, person);
            }
            return BadRequest();
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
            return _context.People.Any(e => e.Id == id);
        }
    }
}
