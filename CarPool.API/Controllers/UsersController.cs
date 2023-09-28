using AutoMapper;
using CarPool.BL.Interfaces;
using CarPool.BL.Models;
using CarPool.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper,
                                    IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
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
        public async Task<IActionResult> GetById(int id)
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
        public async Task<IActionResult> Add(UserAddModel userToAdd)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = _mapper.Map<User>(userToAdd);
            var userResult = await _userService.Add(user);

            if (userResult == null) return BadRequest();

            return Ok(_mapper.Map<UserDetailModel>(userResult));
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
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            var result = await _userService.Remove(user);

            if (!result) return BadRequest();

            return Ok();
        }

    }
}
