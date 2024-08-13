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
    public class CreateSparePartCommand : IRequest<Result<SparePart>>
    {
        public SparePart SparePart { get; set; }
        public CreateSparePartCommand(SparePart sparePart)
        {
            SparePart = sparePart;
        }

        public class CreateSparePartCommandHandler : IRequestHandler<CreateSparePartCommand, Result<SparePart>>
        {
            ISparePartRepository _sparePartRepository;

            public CreateSparePartCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }
            public async Task<Result<SparePart>> Handle(CreateSparePartCommand request, CancellationToken cancellationToken)
            {
                var createdSpare = await _sparePartRepository.Create(request.SparePart);
                return createdSpare == null
                    ? Result.Failure<SparePart>(SparePartContextExceptionEnum.ErrorCreatingSparePart.GetErrorMessage())
                    : Result.Success(createdSpare);
            }
        }
    }
}
