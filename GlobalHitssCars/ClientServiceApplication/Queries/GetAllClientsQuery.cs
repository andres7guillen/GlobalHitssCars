﻿using ClientServiceDomain.Entities;
using ClientServiceDomain.Exceptions;
using ClientServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;

namespace ClientServiceApplication.Queries
{
    public class GetAllClientsQuery : IRequest<Result<Tuple<int, IEnumerable<Client>>>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public GetAllClientsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
        public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Result<Tuple<int, IEnumerable<Client>>>>
        {
            private readonly IClientRepository _clientRepository;

            public GetAllClientsQueryHandler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }

            public async Task<Result<Tuple<int, IEnumerable<Client>>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var listClient = await _clientRepository.GetAll(offset: request.Offset, limit: request.Limit);
                return listClient.Item1 > 0
                    ? Result.Success(listClient)
                    : Result.Failure<Tuple<int,IEnumerable<Client>>>(ClientContextExceptionEnum.NoClientsFound.GetErrorMessage());
            }
        }
    }
}
