using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparePartsServiceAPI.Models;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Services;
using SparePartsServiceInfrastructure.Services;

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
        public async Task<IActionResult> Create([FromBody] SparePartModel model)
        {
            var sparePartEntity = _mapper.Map<SparePart>(model);
            // var result = _mediator.Send(new CreateSparePartCommand() { SparePart = sparePartEntity });
            var result = await _sparePartService.Create(sparePartEntity);
            return Ok(_mapper.Map<SparePartModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases(SparePartByFilterModel model)
        {
            try
            {
                //var query = new GetAllSparePartQuery();
                //var list = await _mediator.Send(query);
                //return Ok(_mapper.Map<IEnumerable<SparePart>, IEnumerable<SparePartModel>>(list));
                var filter = _mapper.Map<SparePartByFilterModel, SparePartByFilter>(model);
                var result = await _sparePartService.GetSparePartsByFilter(filter);
                return Ok(_mapper.Map<IEnumerable<SparePartByFilterModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SparePartByFilterModel model)
        {
            var sparePartEntity = _mapper.Map<SparePart>(model);
            //var result = await _mediator.Send(new UpdateSparePartCommand() { SparePart = sparePartEntity });
            var result = await _sparePartService.UpdatateSpareInStock(sparePartEntity);
            return Ok(_mapper.Map<SparePartModel>(result));
        }
    }
}
