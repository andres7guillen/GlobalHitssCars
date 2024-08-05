using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Commands;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceDomain.Services;

namespace PurchaseServiceAPI.Controllers.Delete
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PurchaseController(IPurchaseService purchaseService, IMapper mapper, IMediator mediator)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpDelete]
        [Route("{id}/DeletePurchase")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _mediator.Send(new DeletePurchaseCommand() { Id = guidId });
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
