using ClientServiceDomain.Entities;
using ClientServiceDomain.Exceptions;
using ClientServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceApplication.Commands
{
    public class CreateClientCommand : IRequest<Result<Client>>
    {
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public CreateClientCommand(string name, string surName, string email)
        {
            Name = name;
            SurName = surName;
            Email = email;
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
                var clientResult = Client.Build(request.Name, request.SurName, request.Email);
                var clientSaved = await _clientRepository.Create(clientResult.Value);

                return clientSaved == null
                    ? Result.Failure<Client>(ClientContextExceptionEnum.ErrorCreatingClient.GetErrorMessage())
                    : Result.Success(clientSaved);
            }
        }
    }
}
