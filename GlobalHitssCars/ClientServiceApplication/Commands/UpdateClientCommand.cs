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
    public class UpdateClientCommand : IRequest<Result<bool>>
    {
        public Client Client { get; set; }

        public UpdateClientCommand(Client client)
        {
            Client = client;
        }

        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<bool>>
        {
            private readonly IClientRepository _clientRepository;

            public UpdateClientCommandHandler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }

            public async Task<Result<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
            {
                var isUpdated = await _clientRepository.Update(request.Client);
                return isUpdated
                    ? Result.Success(isUpdated)
                    : Result.Failure<bool>(ClientContextExceptionEnum.ErrorUpdatingClient.GetErrorMessage());
            }
        }

    }

    
}
