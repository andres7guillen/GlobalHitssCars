using AutoMapper;
using ClientServiceAPI.Middleware;
using ClientServiceAPI.Models;
using ClientServiceApplication.Commands;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ClientServiceAPI.Controllers.GetAllClients.ClientController;

namespace ClientServiceAPI.Controllers.CreateController
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
        [Route("CreateClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CreateClientResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> CreateClient([FromBody] ClientModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);
            var clientCreated = await _mediator.Send(new CreateClientCommand() { Client = clientEntity });
            if (clientCreated.IsFailure)
                return BadRequest(clientCreated.Error);
            var response = new CreateClientResponse
            {
                Client = _mapper.Map<ClientModel>(clientCreated.Value)
            };
            return Ok(CustomResponse<CreateClientResponse>.BuildSuccess(response));
        }

        public class CreateClientResponse 
        {
            public ClientModel Client { get; set; }
        }
    }
}
