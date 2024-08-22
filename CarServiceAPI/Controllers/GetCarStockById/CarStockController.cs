using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Queries;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.GetCarById
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
        [Route("{id}/GetCarStockById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetCarStockByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetById(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var query = new GetCarByIdQuery(idGuid);
            var carResult = await _mediator.Send(query);
            if (carResult.IsFailure)
                return NotFound(carResult.Error);
            var response = new GetCarStockByIdResponse
            {
                Car = _mapper.Map<CarStockModel>(carResult.Value)
            };
            return Ok(CustomResponse<GetCarStockByIdResponse>.BuildSuccess(response));
        }

        public class GetCarStockByIdResponse
        {
            public CarStockModel Car { get; set; }
        }

    }
}
