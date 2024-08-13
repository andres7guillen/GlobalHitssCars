using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.UpdateCar
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CarController(ICarService carService, IMapper mapper, IMediator mediator)
        {
            _carService = carService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut]
        [Route("UpdateCar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> UpdateCar([FromBody] CarModel model)
        {
            var carEntity = _mapper.Map<Car>(model);
            var carUpdated = await _mediator.Send(new UpdateCarCommand(carEntity));

            if (carUpdated.IsFailure)
                return BadRequest(carUpdated.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(carUpdated.Value));
        }
    }
}
