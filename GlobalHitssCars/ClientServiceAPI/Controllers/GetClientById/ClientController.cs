using AutoMapper;
using Azure;
using ClientServiceAPI.Middleware;
using ClientServiceAPI.Models;
using ClientServiceApplication.Queries;
using ClientServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ClientServiceAPI.Controllers.GetAllClients.ClientController;
using static ClientServiceAPI.Controllers.GetAllClients.ClientController.GetAllClientsResponse;

namespace ClientServiceAPI.Controllers.GetClientById
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

        [HttpGet]
        [Route("{id}/GetClientById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetClientByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomResponse<object>))]

        public async Task<IActionResult> GetClientById(string id)
        {
            try
            {
                Guid idGuid = Guid.Parse(id);
                var query = new GetClientByIdQuery(idGuid);
                var clientResult = await _mediator.Send(query);
                if (clientResult.IsFailure)
                    return NotFound();
                var response = new GetClientByIdResponse
                {
                    Client = _mapper.Map<ClientModel>(clientResult.Value)
                };
                return Ok(CustomResponse<GetClientByIdResponse>.BuildSuccess(response));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetClientByIdResponse
        {
            public ClientModel Client { get; set; }
        }

    }
}
