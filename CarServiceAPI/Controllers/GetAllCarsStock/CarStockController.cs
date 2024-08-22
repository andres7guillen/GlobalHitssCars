using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Queries;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.GetAllCars
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStockController : ControllerBase
    {
        private readonly ICarStockService _carService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CarStockController(ICarStockService carService, IMapper mapper, IMediator mediator)
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
                    cars = _mapper.Map<IEnumerable<CarStock>, IEnumerable<CarStockModel>>(list.Value.Item2)
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
            public IEnumerable<CarStockModel> cars { get; set; }
        }


    }
}
