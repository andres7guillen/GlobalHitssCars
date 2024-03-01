using AutoMapper;
using CarServiceAPI.Models;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            // var result = _mediator.Send(new CreateCarCommand() { Car = carEntity });
            var result = await _carService.Create(carEntity);
            return Ok(_mapper.Map<CarModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                //var list = await _service.GetAll();
                //var query = new GetAllCarsQuery();
                //var list = await _mediator.Send(query);
                //return Ok(_mapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(list));
                var result = await _carService.GetAll();
                return Ok(_mapper.Map<IEnumerable<CarModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                Guid idGuid = Guid.Parse(id);
                //var query = new GetCarByIdQuery(idGuid);
                //var carResult = await _mediator.Send(query);
                //if (carResult == null)
                //    return NotFound();

                var result = await _carService.GetById(idGuid);
                if (result == null)
                    return NotFound();
                return Ok(_mapper.Map<CarModel>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CarModel model)
        {
            var carEntity = _mapper.Map<Car>(model);
            //var result = await _mediator.Send(new UpdateCarCommand() { Car = carEntity });
            var result = await _carService.Update(carEntity);
            return Ok(_mapper.Map<CarModel>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) 
        {
            var guidId = Guid.Parse(id);
            //var result = await _mediator.Send(new DeleteCompanyCommand() { Id = guidId });
            var result = await _carService.Delete(guidId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetCarByFilter([FromBody] CarByFilterModel model) 
        {
            var filterEntity = _mapper.Map<CarByFilterDTO>(model);
            //var query = new GetCarByIdQuery(model.Colour);
            //var carResult = await _mediator.Send(query);
            var result = await _carService.GetCarByFilter(filterEntity);
            if (result == null)
                return NotFound();
            return Ok(_mapper.Map<CarModel>(result));
        }

    }
}
