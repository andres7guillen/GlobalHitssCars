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

namespace ClientServiceAPI.Controllers.UpdateClient
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

        [HttpPut]
        [Route("UpdateClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> UpdateClient([FromBody] ClientModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);
            var result = await _mediator.Send(new UpdateClientCommand(clientEntity.Name, clientEntity.SurName, clientEntity.Email, clientEntity.Id));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
