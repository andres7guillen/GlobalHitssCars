using AutoMapper;
using CarServiceAPI.Models;
using CarServiceApplication.Commands;
using CarServiceApplication.Queries;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Services;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CarServiceAPI.Controllers
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
        public async Task<IActionResult> Create([FromBody] CarModel model)
        {
            var carEntity = _mapper.Map<Car>(model);
            var carResult = await _mediator.Send(new CreateCarCommand() { Car = carEntity });
            if (carResult.IsFailure)
                return BadRequest(carResult.Error);
            return Ok(_mapper.Map<CarModel>(carResult.Value));
        }

        [HttpGet("{offset}/{limit}")]
        public async Task<IActionResult> GetAllCars(int offset, int limit)
        {
            try
            {
                var query = new GetAllCarsQuery(offset: offset, limit: limit);
                var list = await _mediator.Send(query);
                if (list.IsFailure)
                    return BadRequest(list.Error);
                return Ok(_mapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(list.Value));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var query = new GetCarByIdQuery(idGuid);
            var carResult = await _mediator.Send(query);
            if (carResult.IsFailure)
                return NotFound(carResult.Error);
            return Ok(carResult.Value);
        }

        [HttpPost]
        public async Task<IActionResult> GetCarByFilter([FromBody] CarByFilterModel model)
        {
            var filterEntity = _mapper.Map<CarByFilterDTO>(model);
            var query = new GetCarByFilterQuery(filterEntity);
            var carResult = await _mediator.Send(query);
            if (carResult.IsFailure)
                return NotFound();
            return Ok(_mapper.Map<CarModel>(carResult.Value));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CarModel model)
        {
            var carEntity = _mapper.Map<Car>(model);
            var carUpdated = await _mediator.Send(new UpdateCarCommand() { Car = carEntity });
            
            if(carUpdated.IsFailure)
                return BadRequest(carUpdated.Error);
            return Ok(_mapper.Map<CarModel>(carUpdated.Value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteCompanyCommand() { Id = guidId });
            
            if(result.IsFailure)
                return BadRequest(CarContextExceptionEnum.ErrorDeleteingCar.GetErrorMessage());
            return Ok(result.Value);
        }
    }
}
