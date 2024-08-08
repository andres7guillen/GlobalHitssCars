using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseApplication.Queries
{
    public class GetPurchaseByIdQuery : IRequest<Result<Purchase>>
    {
        public Guid Id { get; set; }
    }

    public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery,Result<Purchase>>
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
