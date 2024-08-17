using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;

namespace PurchaseApplication.Queries
{
    public class GetAllPurchasesQuery : IRequest<Result<Tuple<int, IEnumerable<Purchase>>>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public GetAllPurchasesQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public class GetAllPurchasesQueryHandler : IRequestHandler<GetAllPurchasesQuery, Result<Tuple<int, IEnumerable<Purchase>>>>
        {
            private readonly IPurchaseRepository _purchaseRepository;

            public GetAllPurchasesQueryHandler(IPurchaseRepository purchaseRepository)
            {
                _purchaseRepository = purchaseRepository;
            }

            public async Task<Result<Tuple<int, IEnumerable<Purchase>>>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
            {
                var list = await _purchaseRepository.GetAll(request.Offset, request.Limit);
                return list.Item1 > 0
                    ? Result.Success(list)
                    : Result.Failure<Tuple<int, IEnumerable<Purchase>>>(PurchaseContextExceptionEnum.NoPurchasesFound.GetErrorMessage());
            }
        }
    }

}
