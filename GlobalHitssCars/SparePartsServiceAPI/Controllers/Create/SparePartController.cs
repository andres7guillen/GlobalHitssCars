using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparePartServiceApplication.Commands;
using SparePartServiceApplication.Queries;
using SparePartsServiceAPI.Middleware;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Services;

namespace SparePartsServiceAPI.Controllers
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
        [Route("CreateSparePart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CreateSparePartResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Create([FromBody] SparePartModel model)
        {
            var sparePartEntity = _mapper.Map<SparePart>(model);
            var result = await _mediator.Send(new CreateSparePartCommand(sparePartEntity));
            if (result.IsFailure)
                BadRequest(result.Error);
            var response = new CreateSparePartResponse()
            {
                SparePart = _mapper.Map<SparePartModel>(result.Value)
            };
            return Ok(CustomResponse<CreateSparePartResponse>.BuildSuccess(response));
        }

        public class CreateSparePartResponse
        {
            public SparePartModel SparePart { get; set; }
        }

    }
}
