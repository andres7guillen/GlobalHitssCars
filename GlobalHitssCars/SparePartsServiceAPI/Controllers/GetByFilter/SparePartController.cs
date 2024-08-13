using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Queries;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers.GetByFilter
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
        [HttpPost]
        [Route("GetSparePartsByFilter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetSparePartByFilterResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetByFilter([FromBody] GetSparePartByFilterModel model) 
        {
            var filterEntity = _mapper.Map<GetSparePartByFilterDTO>(model);
            var query = new GetSparePartsByFilterQuery(filterEntity);
            var listResult = await _mediator.Send(query);
            if (listResult.IsFailure)
                return BadRequest(listResult.Error);
            var response = new GetSparePartByFilterResponse
            {
                Spares = _mapper.Map<IEnumerable<SparePartModel>>(listResult.Value)
            };
            return Ok(CustomResponse<GetSparePartByFilterResponse>.BuildSuccess(response));
        }

        public class GetSparePartByFilterResponse
        {
            public IEnumerable<SparePartModel> Spares { get; set; }
        }


    }
}
