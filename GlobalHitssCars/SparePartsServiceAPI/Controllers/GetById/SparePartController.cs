using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Queries;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers.GetById
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
        [Route("{id}/GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetSparePartByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetById(string id)
        {
            Guid guidId = Guid.Parse(id);
            var query = new GetSparePartByIdQuery()
            {
                Id = guidId
            };
            var maybeSpare = await _mediator.Send(query);
            if (maybeSpare.IsFailure)
                return BadRequest(maybeSpare.Error);
            var response = new GetSparePartByIdResponse()
            {
                Spare = _mapper.Map<SparePartModel>(maybeSpare.Value)
            };
            return Ok(CustomResponse<GetSparePartByIdResponse>.BuildSuccess(response));
        }

        public class GetSparePartByIdResponse 
        {
            public SparePartModel Spare { get; set; }
        }

    }
}
