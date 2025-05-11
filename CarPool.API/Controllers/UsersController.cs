using AutoMapper;
using CarPool.BL.Interfaces;
using CarPool.BL.Models;
using CarPool.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(IMapper mapper,
                            IUserService userService,
                            UserManager<ApplicationUser> userManager,
                            IConfiguration configuration)
        {
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDetailModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserDetailModel>>(users));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserDetailModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDetailModel>(user));
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(UserDetailModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.Search(email);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDetailModel>(user));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add([FromBody] UserAddModel userToAdd)
        {
            var user = new ApplicationUser
            {
                Email = userToAdd.Email,
                UserName = userToAdd.Email,
                Name = userToAdd.Name,
                Surname = userToAdd.Surname
            };

            var result = await _userManager.CreateAsync(user, userToAdd.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            user.PasswordHash = null;
            return Created("", user);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new AuthResponse { Token = tokenHandler.WriteToken(token) });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            var result = await _userService.Remove(user);
            if (!result) return BadRequest();

            return Ok();
        }

        public class AuthResponse
        {
            public string Token { get; set; }
        }
    }
}