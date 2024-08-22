using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Commands;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Enum;
using PurchaseServiceDomain.Services;

namespace PurchaseServiceAPI.Controllers.Create
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

        [HttpPost]
        [Route("CreatePurchase")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CreatePurchaseResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Create([FromBody] PurchaseModel model)
        {
            if (string.IsNullOrWhiteSpace(model.TypePurchase) || Enum.TryParse(model.TypePurchase, true, out TypePurchaseEnum typePurchase)) 
            {
                ModelState.AddModelError("Error", "'TypePurchase' must be valid.");
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new CreatePurchaseCommand(Guid.Parse(model.ClientId),Guid.Parse(model.CarId), model.Amount, typePurchase, Guid.Parse(model.SpareId), model.Quantity));
            if (result.IsFailure)
                return BadRequest(result.Error);
            var response = new CreatePurchaseResponse
            {
                Purchase = _mapper.Map<PurchaseModel>(result.Value)
            };
            return Ok(CustomResponse<CreatePurchaseResponse>.BuildSuccess(response));
        }

        public class CreatePurchaseResponse
        {
            public PurchaseModel Purchase { get; set; }
        }

    }
}
