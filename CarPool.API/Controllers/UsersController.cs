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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();

            return Ok(_mapper.Map<IEnumerable<UserDetailModel>>(users));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDetailModel>(user));
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.Search(email);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDetailModel>(user));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(UserAddModel userToAdd)
        {
            var user = new ApplicationUser
            {
                Email = userToAdd.Email,
                UserName = userToAdd.Email,
                Name = userToAdd.Name,
                Surname = userToAdd.Surname
            };

            var result = await _userManager.CreateAsync(user, userToAdd.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.PasswordHash = null;
            return Created("", user);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(); // Invalid credentials
            }

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
                Issuer = _configuration["Jwt:Issuer"], // Use Issuer from appsettings.json
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        //[HttpPut("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Update(int id, U categoryDto)
        //{
        //    if (id != categoryDto.Id) return BadRequest();

        //    if (!ModelState.IsValid) return BadRequest();

        //    await _categoryService.Update(_mapper.Map<Category>(categoryDto));

        //    return Ok(categoryDto);
        //}

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            var result = await _userService.Remove(user);

            if (!result) return BadRequest();

            return Ok();
        }

    }
}
