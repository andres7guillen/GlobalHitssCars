using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;

namespace PurchaseApplication.Queries
{
    public class GetPurchaseByIdQuery : IRequest<Result<Purchase>>
    {
        public Guid Id { get; set; }

        public GetPurchaseByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, Result<Purchase>>
        {
            private readonly IPurchaseRepository _purchaseRepository;

            public GetPurchaseByIdQueryHandler(IPurchaseRepository purchaseRepository)
            {
                _purchaseRepository = purchaseRepository;
            }
            public async Task<Result<Purchase>> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
            {
                var maybePurchase = await _purchaseRepository.GetById(request.Id);
                return maybePurchase.HasValue
                    ? Result.Success<Purchase>(maybePurchase.Value)
                    : Result.Failure<Purchase>(PurchaseContextExceptionEnum.PurchaseNotFound.GetErrorMessage());
            }
        }
    }


}
