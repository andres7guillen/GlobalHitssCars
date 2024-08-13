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

namespace SparePartServiceApplication.Queries
{
    public class GetSparePartByIdQuery : IRequest<Result<SparePart>>
    {
        public Guid Id { get; set; }

        public GetSparePartByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetSparePartByIdQueryHandler : IRequestHandler<GetSparePartByIdQuery, Result<SparePart>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public GetSparePartByIdQueryHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<SparePart>> Handle(GetSparePartByIdQuery request, CancellationToken cancellationToken)
            {
                var maybeSpare = await _sparePartRepository.GetSparePartById(request.Id);
                return maybeSpare.HasNoValue
                    ? Result.Failure<SparePart>(SparePartContextExceptionEnum.SparePartNotFound.GetErrorMessage())
                    : Result.Success<SparePart>(maybeSpare.Value);
            }
        }
    }
}
