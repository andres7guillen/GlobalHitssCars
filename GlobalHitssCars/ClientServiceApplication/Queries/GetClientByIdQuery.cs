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

namespace ClientServiceApplication.Queries
{
    public class GetClientByIdQuery : IRequest<Result<Client>>
    {
        public Guid Id { get; set; }
        public GetClientByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result<Client>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var clientMaybe = await _clientRepository.GetById(request.Id);
            return clientMaybe.HasNoValue
                ? Result.Failure<Client>(ClientContextExceptionEnum.ClientNotFound.GetErrorMessage())
                : Result.Success(clientMaybe.Value);
        }
    }

}
