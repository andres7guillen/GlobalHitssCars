using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Queries;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers.GetAll
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

        [HttpGet]
        [Route("{offset}/{limit}/GetAllSpares")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetAllSparePartsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetAllPurchases(int offset, int limit)
        {
            var query = new GetAllSparePartsQuery(offset, limit);
            var list = await _mediator.Send(query);
            if (list.IsFailure)
                return BadRequest(list.Error);
            var response = new GetAllSparePartsResponse()
            {
                SpareParts = _mapper.Map<IEnumerable<SparePartModel>>(list.Value)
            };
            return Ok(CustomResponse<GetAllSparePartsResponse>.BuildSuccess(response));
        }

        public class GetAllSparePartsResponse 
        {
            public IEnumerable<SparePartModel> SpareParts { get; set; }
        }

    }
}
