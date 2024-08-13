using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;

namespace SparePartServiceApplication.Queries
{
    public class GetSparePartsByFilterQuery : IRequest<Result<IEnumerable<SparePart>>>
    {
        public GetSparePartByFilterDTO Filter { get; set; }

        public GetSparePartsByFilterQuery(GetSparePartByFilterDTO filter)
        {
            Filter = filter;
        }

        public class GetSparePartsByFilterHandler : IRequestHandler<GetSparePartsByFilterQuery, Result<IEnumerable<SparePart>>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public GetSparePartsByFilterHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<IEnumerable<SparePart>>> Handle(GetSparePartsByFilterQuery request, CancellationToken cancellationToken)
            {
                var list = await _sparePartRepository.GetSparePartsByFilter(request.Filter);
                return list.Count() > 0
                    ? Result.Success(list)
                    : Result.Failure<IEnumerable<SparePart>>(SparePartContextExceptionEnum.SparePartNotFoundByFilter.GetErrorMessage());
            }
        }
    }
}
