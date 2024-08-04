using AutoMapper;
using ClientServiceAPI.Middleware;
using ClientServiceApplication.Commands;
using ClientServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ClientServiceAPI.Controllers.GetAllClients.ClientController;

namespace ClientServiceAPI.Controllers.DeleteClient
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


        [HttpDelete]
        [Route("{id}/DeleteClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteClientCommand() { Id = guidId });
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(result.Value));
        }
    }
}
