using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartServiceApplication.Commands
{
    public class DeleteSparePartCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public DeleteSparePartCommand(Guid id)
        {
            Id = id;
        }

        public class DeleteSparePartCommandHandler : IRequestHandler<DeleteSparePartCommand, Result<bool>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public DeleteSparePartCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<bool>> Handle(DeleteSparePartCommand request, CancellationToken cancellationToken)
            {
                var deleted = await _sparePartRepository.DeleteSparePart(request.Id);
                return deleted
                    ? Result.Success(deleted)
                    : Result.Failure<bool>(SparePartContextExceptionEnum.ErrorDeletingSparePart.GetErrorMessage());
            }
        }
    }
}
