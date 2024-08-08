using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartServiceApplication.Commands
{
    public class UpdateSparePartCommand : IRequest<Result<bool>>
    {
        public SparePart Spare { get; set; }
    }

    public class UpdateSparePartCommandHandler : IRequestHandler<UpdateSparePartCommand, Result<bool>>
    {
        private readonly ISparePartRepository _sparePartRepository;

        public UpdateSparePartCommandHandler(ISparePartRepository sparePartRepository)
        {
            _sparePartRepository = sparePartRepository;
        }

        public async Task<Result<bool>> Handle(UpdateSparePartCommand request, CancellationToken cancellationToken)
        {
            var updated = await _sparePartRepository.UpdatateSpare(request.Spare);
            return updated
                ? Result.Success(updated)
                : Result.Failure<bool>(SparePartContextExceptionEnum.ErrorUpdatingSparePart.GetErrorMessage());
        }
    }
}
