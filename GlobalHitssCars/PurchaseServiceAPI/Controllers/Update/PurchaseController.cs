using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Commands;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Services;

namespace PurchaseServiceAPI.Controllers.Update
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

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Update([FromBody] PurchaseModel model)
        {
            var purchaseEntity = _mapper.Map<Purchase>(model);
            var result = await _mediator.Send(new UpdatePurchaseCommand(purchaseEntity));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
