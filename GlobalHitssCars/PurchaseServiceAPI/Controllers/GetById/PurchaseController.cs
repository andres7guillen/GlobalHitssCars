using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Queries;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Services;

namespace PurchaseServiceAPI.Controllers.GetById
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

        [HttpGet]
        [Route("{id}/GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetPurchaseByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetById(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var query = new GetPurchaseByIdQuery(idGuid);
            var purchaseResult = await _mediator.Send(query);
            if (purchaseResult.IsFailure)
                return BadRequest(purchaseResult.Error);
            var response = new GetPurchaseByIdResponse()
            {
                Purchase = _mapper.Map<PurchaseModel>(purchaseResult.Value)
            };
            return Ok(CustomResponse<GetPurchaseByIdResponse>.BuildSuccess(response));
        }

        public class GetPurchaseByIdResponse
        {
            public PurchaseModel Purchase { get; set; }
        }
    }
}
