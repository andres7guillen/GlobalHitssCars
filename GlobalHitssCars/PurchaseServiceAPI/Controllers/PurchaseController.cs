using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseServiceAPI.Models;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Services;
using PurchaseServiceInfrastructure.Services;

namespace PurchaseServiceAPI.Controllers
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
        public async Task<IActionResult> Create([FromBody] PurchaseModel model)
        {
            var purchaseEntity = _mapper.Map<Purchase>(model);
            // var result = _mediator.Send(new CreatePurchaseCommand() { Purchase = purchaseEntity });
            var result = await _purchaseService.Create(purchaseEntity);
            return Ok(_mapper.Map<PurchaseModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            try
            {
                //var query = new GetAllPurchasesQuery();
                //var list = await _mediator.Send(query);
                //return Ok(_mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseModel>>(list));
                var result = await _purchaseService.GetAll();
                return Ok(_mapper.Map<IEnumerable<PurchaseModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                Guid idGuid = Guid.Parse(id);
                //var query = new GetPurchaseByIdQuery(idGuid);
                //var purchaseResult = await _mediator.Send(query);
                //if (purchaseResult == null)
                //    return NotFound();

                var result = await _purchaseService.GetById(idGuid);
                if (result == null)
                    return NotFound();
                return Ok(_mapper.Map<PurchaseModel>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PurchaseModel model)
        {
            var purchaseEntity = _mapper.Map<Purchase>(model);
            //var result = await _mediator.Send(new UpdatePurchaseCommand() { Purchase = purchaseEntity });
            var result = await _purchaseService.Update(purchaseEntity);
            return Ok(_mapper.Map<PurchaseModel>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            //var result = await _mediator.Send(new DeletePurchaseCommand() { Id = guidId });
            var result = await _purchaseService.Delete(guidId);
            return Ok(result);
        }

    }
}
