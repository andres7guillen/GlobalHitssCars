using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Commands;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Entities;
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
            var purchaseEntity = _mapper.Map<Purchase>(model);
            purchaseEntity.Id = Guid.NewGuid();
            var result = await _mediator.Send(new CreatePurchaseCommand(purchaseEntity));
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
