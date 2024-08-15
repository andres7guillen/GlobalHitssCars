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
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UpdateClientCommand(string name, string surName, string email, Guid id)
        {
            Name = name;
            SurName = surName;
            Email = email;
            Id = id;
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
                var clientToUpdate = Client.Load(request.Id, request.Name, request.SurName, request.Email);

                var isUpdated = await _clientRepository.Update(clientToUpdate.Value);
                return isUpdated
                    ? Result.Success(isUpdated)
                    : Result.Failure<bool>(ClientContextExceptionEnum.ErrorUpdatingClient.GetErrorMessage());
            }
        }

    }

    
}
