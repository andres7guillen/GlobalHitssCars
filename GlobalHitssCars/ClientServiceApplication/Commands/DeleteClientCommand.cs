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
    public class DeleteClientCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }        
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<bool>>
    {
        private readonly IClientRepository _clientRepository;

        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _clientRepository.Delete(request.Id);
            return isDeleted
                ? Result.Success(isDeleted)
                : Result.Failure<bool>(ClientContextExceptionEnum.ErrorDeleteingClient.GetErrorMessage());
        }
    }
}
