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
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CreateCarResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Create([FromBody] CarStockModel model)
        {
            var carEntity = _mapper.Map<CarStock>(model);
            var carResult = await _mediator.Send(new CreateCarStockCommand(carEntity.BrandId, model.Model, carEntity.ReferenceId, model.Colour));
            if (carResult.IsFailure)
                return BadRequest(carResult.Error);
            var response = new CreateCarResponse
            {
                Car = _mapper.Map<CarStockModel>(carResult.Value)
            };
            return Ok(CustomResponse<CreateCarResponse>.BuildSuccess(response));
        }


        public class CreateCarResponse 
        {
            public CarStockModel Car { get; set; }
        }

    }
}
