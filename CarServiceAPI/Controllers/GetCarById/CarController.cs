using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Queries;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CarServiceAPI.Controllers.CreateCar.CarController;

namespace CarServiceAPI.Controllers.GetCarById
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

        [HttpGet]
        [Route("{id}/GetCarById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetCarByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetById(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var query = new GetCarByIdQuery(idGuid);
            var carResult = await _mediator.Send(query);
            if (carResult.IsFailure)
                return NotFound(carResult.Error);
            var response = new GetCarByIdResponse
            {
                Car = _mapper.Map<CarModel>(carResult.Value)
            };
            return Ok(CustomResponse<GetCarByIdResponse>.BuildSuccess(response));
        }

        public class GetCarByIdResponse 
        {
            public CarModel Car { get; set; }
        }

    }
}
