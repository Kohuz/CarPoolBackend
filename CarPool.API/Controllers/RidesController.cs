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
    public class RidesController : ControllerBase
    {
        private readonly IRideService _rideService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RidesController(IMapper mapper, IRideService rideService, IUserService userService)
        {
            _mapper = mapper;
            _rideService = rideService;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RideResultModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var rides = await _rideService.GetAll();
            return Ok(_mapper.Map<IEnumerable<RideResultModel>>(rides));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RideResultModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ride = await _rideService.GetById(id);
            if (ride == null) return NotFound();
            return Ok(_mapper.Map<RideResultModel>(ride));
        }

        [HttpGet("driver/{driverId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<RideResultModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRidesByDriver(Guid driverId)
        {
            var user = await _userService.GetById(driverId);
            if (user == null) return NotFound();

            var rides = await _rideService.GetRidesByDriver(driverId);
            return Ok(_mapper.Map<IEnumerable<RideResultModel>>(rides));
        }

        [HttpGet("passenger/{passengerId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<RideResultModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRidesByPassenger(Guid passengerId)
        {
            var user = await _userService.GetById(passengerId);
            if (user == null) return NotFound();

            var rides = await _rideService.GetRidesByPassenger(passengerId);
            return Ok(_mapper.Map<IEnumerable<RideResultModel>>(rides));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(typeof(IEnumerable<RideResultModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilteredRides(
            [FromQuery] string startLocation,
            [FromQuery] string endLocation,
            [FromQuery] DateTime startTime)
        {
            var rides = await _rideService.GetRidesByLocationsAndStartTime(startLocation, endLocation, startTime);
            if (rides == null || !rides.Any()) return NotFound();
            return Ok(_mapper.Map<IEnumerable<RideResultModel>>(rides));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RideResultModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] RideAddModel rideToAdd)
        {
            if (!ModelState.IsValid) return BadRequest();

            var ride = _mapper.Map<Ride>(rideToAdd);
            var rideResult = await _rideService.Add(ride);

            if (rideResult == null) return BadRequest();
            return Ok(_mapper.Map<RideResultModel>(rideResult));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var ride = await _rideService.GetById(id);
            if (ride == null) return NotFound();

            var result = await _rideService.Remove(ride);
            if (!result) return BadRequest();

            return Ok();
        }
    }
}