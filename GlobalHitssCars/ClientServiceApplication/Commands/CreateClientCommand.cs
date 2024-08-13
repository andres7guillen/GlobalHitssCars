using ClientServiceDomain.Entities;
using ClientServiceDomain.Exceptions;
using ClientServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceApplication.Commands
{
    public class CreateClientCommand : IRequest<Result<Client>>
    {
        public Client Client { get; set; }

        public CreateClientCommand(Client client)
        {
            Client = client;
        }

        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<Client>>
        {
            private readonly IClientRepository _clientRepository;

            public CreateClientCommandHandler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }

            public async Task<Result<Client>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.Create(request.Client);
                return client == null
                    ? Result.Failure<Client>(ClientContextExceptionEnum.ErrorCreatingClient.GetErrorMessage())
                    : Result.Success(client);
            }
        }
    }
}
