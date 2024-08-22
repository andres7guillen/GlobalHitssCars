using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceApplication.Commands;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.DeleteCar
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

        [HttpDelete]
        [Route("{id}/Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteCarStockCommand(guidId));

            if (result.IsFailure)
                return BadRequest(CarContextExceptionEnum.ErrorDeletingCar.GetErrorMessage());
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
