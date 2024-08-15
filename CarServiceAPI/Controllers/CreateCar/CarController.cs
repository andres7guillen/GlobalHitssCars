using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.CreateCar
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



        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CreateCarResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Create([FromBody] CarModel model)
        {
            var carEntity = _mapper.Map<Car>(model);
            var carResult = await _mediator.Send(new CreateCarCommand(carEntity));
            if (carResult.IsFailure)
                return BadRequest(carResult.Error);
            var response = new CreateCarResponse
            {
                Car = _mapper.Map<CarModel>(carResult.Value)
            };
            return Ok(CustomResponse<CreateCarResponse>.BuildSuccess(response));
        }


        public class CreateCarResponse 
        {
            public CarModel Car { get; set; }
        }

    }
}
