using AutoMapper;
using CarPool.BL.Interfaces;
using CarPool.BL.Models;
using CarPool.BL.Services;
using CarPool.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CarsController(IMapper mapper,
                                    ICarService carService, IUserService userService)
        {
            _mapper = mapper;
            _carService = carService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carService.GetAll();

            return Ok(_mapper.Map<IEnumerable<CarDetailModel>>(cars));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var car = await _carService.GetById(id);

            if (car == null) return NotFound();

            return Ok(_mapper.Map<CarDetailModel>(car));
        }

        [HttpGet("owner/{ownerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarsByOwner(Guid ownerId)
        {
            var user = await _userService.GetById(ownerId);
            if (user == null) return NotFound();

            var cars = await _carService.GetCarsByOwner(ownerId);

            return Ok(_mapper.Map<IEnumerable<CarDetailModel>>(cars));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CarAddModel carToAdd)
        {
            if (!ModelState.IsValid) return BadRequest();

            var car = _mapper.Map<Car>(carToAdd);
            var carResult = await _carService.Add(car);

            if (carResult == null) return BadRequest();

            return Ok(_mapper.Map<CarDetailModel>(carResult));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var car = await _carService.GetById(id);
            if (car == null) return NotFound();

            var result = await _carService.Remove(car);

            if (!result) return BadRequest();

            return Ok();
        }
    }
}

