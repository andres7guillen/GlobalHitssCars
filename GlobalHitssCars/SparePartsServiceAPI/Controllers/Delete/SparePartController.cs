using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Commands;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Services;
using static SparePartsServiceAPI.Controllers.SparePartController;

namespace SparePartsServiceAPI.Controllers.Delete
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
        [HttpDelete]
        [Route("{id}/Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Delete(string id) 
        {
            Guid idGuid = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteSparePartCommand(idGuid));
            if (result.IsFailure)
                BadRequest(result.Error);            
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
