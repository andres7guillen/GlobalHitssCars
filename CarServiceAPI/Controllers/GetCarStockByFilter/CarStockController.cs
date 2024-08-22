using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Queries;
using CarServiceDomain.DTOs;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.GetByFilter
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


        [HttpPost]
        [Route("GetByFilter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetCarByFilterResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetByFilter([FromBody] CarStockByFilterModel model)
        {
            var filterEntity = _mapper.Map<CarStockByFilterDTO>(model);
            var query = new GetCarByFilterQuery(filterEntity);
            var carResult = await _mediator.Send(query);
            if (carResult.IsFailure)
                return NotFound();
            var response = new GetCarByFilterResponse
            {
                Cars = _mapper.Map<IEnumerable<CarStockModel>>(carResult.Value)
            };

            return Ok(CustomResponse<GetCarByFilterResponse>.BuildSuccess(response));
        }

        public class GetCarByFilterResponse 
        {
            public IEnumerable<CarStockModel> Cars { get; set; }
        }

    }
}
