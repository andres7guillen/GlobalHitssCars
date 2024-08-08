using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseApplication.Queries;
using PurchaseServiceAPI.Middleware;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Services;

namespace PurchaseServiceAPI.Controllers.GetAll
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
        [Route("{offset}/{limit}/GetAllPurchases")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetAllPurchasesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetAllPurchases(int offset, int limit)
        {
            try
            {
                var query = new GetAllPurchasesQuery() 
                { 
                    Limit = limit,
                    Offset = offset
                };
                var list = await _mediator.Send(query);
                if (list.IsFailure)
                    return BadRequest(list.Error);
                var response = new GetAllPurchasesResponse()
                {
                    Purchases = _mapper.Map<IEnumerable<PurchaseModel>>(list.Value)
                };
                return Ok(CustomResponse<GetAllPurchasesResponse>.BuildSuccess(response));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllPurchasesResponse 
        {
            public IEnumerable<PurchaseModel> Purchases { get; set; }
        }

    }
}
