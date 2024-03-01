using AutoMapper;
using ClientServiceAPI.Models;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ClientController(IClientService clientService, IMapper mapper, IMediator mediator) 
        { 
            _clientService = clientService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);
            // var result = _mediator.Send(new CreateClientCommand() { Client = clienteEntity });
            var result = await _clientService.Create(clientEntity);
            return Ok(_mapper.Map<ClientModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                //var query = new GetAllClientsQuery();
                //var list = await _mediator.Send(query);
                //return Ok(_mapper.Map<IEnumerable<Client>, IEnumerable<ClientModel>>(list));
                var result = await _clientService.GetAll();
                return Ok(_mapper.Map<IEnumerable<ClientModel>>(result));
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
                //var query = new GetClientByIdQuery(idGuid);
                //var clientResult = await _mediator.Send(query);
                //if (clientResult == null)
                //    return NotFound();

                var result = await _clientService.GetById(idGuid);
                if (result == null)
                    return NotFound();
                return Ok(_mapper.Map<ClientModel>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);
            //var result = await _mediator.Send(new UpdateClientCommand() { Client = clientEntity });
            var result = await _clientService.Update(clientEntity);
            return Ok(_mapper.Map<ClientModel>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            //var result = await _mediator.Send(new DeleteClientCommand() { Id = guidId });
            var result = await _clientService.Delete(guidId);
            return Ok(result);
        }
    }
}
