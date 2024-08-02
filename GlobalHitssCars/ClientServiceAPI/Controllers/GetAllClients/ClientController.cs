using AutoMapper;
using ClientServiceAPI.Middleware;
using ClientServiceAPI.Models;
using ClientServiceApplication.Queries;
using ClientServiceDomain.Exceptions;
using ClientServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientServiceAPI.Controllers.GetAllClients
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

        [HttpGet("{offset}/{limit}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetAllClientsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> GetAllClients(int offset, int limit)
        {
            try
            {
                var query = new GetAllClientsQuery(offset, limit);
                var list = await _mediator.Send(query);
                if (list.IsFailure)
                    return BadRequest(list.Error);
                var response = new GetAllClientsResponse
                {
                    ListClients = _mapper.Map<IEnumerable<ClientModel>>(list.Value)
                };
                return Ok(CustomResponse<GetAllClientsResponse>.BuildSuccess(response));
            }
            catch (Exception e)
            {
                return BadRequest($"{ClientContextExceptionEnum.NoClientsFound.GetErrorMessage()}-{e.Message}");
            }
        }
        public class GetAllClientsResponse
        {
            public IEnumerable<ClientModel> ListClients { get; set; }
        }


    }
}
