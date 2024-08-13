using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Commands;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers.UpdateStock
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
        [Route("{id}/{newStock}/UpdateSpareStock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> UpdateSpareStock(string id, int newStock)
        {
            Guid idGuid = Guid.Parse(id);
            var result = await _mediator.Send(new LessStockSparePartCommand(idGuid, newStock));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
