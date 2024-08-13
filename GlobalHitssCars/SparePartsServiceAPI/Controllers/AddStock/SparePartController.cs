using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Commands;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers.AddStock
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartController : ControllerBase
    {
        private readonly ISparePartService _sparePartService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SparePartController(ISparePartService sparePartService, IMapper mapper, IMediator mediator)
        {
            _sparePartService = sparePartService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut]
        [Route("AddSparePart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> LessSparePart([FromBody] AddStockSparePartModel model)
        {

            var result = await _mediator.Send(new AddSpareStockCommand(Guid.Parse(model.Id), model.Quantity));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
        
    }
}
