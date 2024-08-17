using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Queries;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CarServiceAPI.Controllers.CreateCar.CarController;

namespace CarServiceAPI.Controllers.GetAllCars
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
        [Route("{offset}/{limit}/GetAllCars")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetAllCarsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetAllCars(int offset, int limit)
        {
            try
            {
                var query = new GetAllCarsQuery(offset: offset, limit: limit);
                var list = await _mediator.Send(query);
                if (list.IsFailure)
                    return BadRequest(list.Error);
                var response = new GetAllCarsResponse
                {
                    cars = _mapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(list.Value.Item2)
                };
                return Ok(CustomResponse<GetAllCarsResponse>.BuildSuccess(response));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllCarsResponse 
        { 
            public IEnumerable<CarModel> cars { get; set; }
        }


    }
}
