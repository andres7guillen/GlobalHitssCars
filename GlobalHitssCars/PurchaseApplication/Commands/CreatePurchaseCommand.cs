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

namespace PurchaseApplication.Commands
{
    public class CreatePurchaseCommand : IRequest<Result<Purchase>>
    {
        public Purchase Purchase { get; set; }

        public CreatePurchaseCommand(Purchase purchase)
        {
            Purchase = purchase;
        }

        public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, Result<Purchase>>
        {
            private readonly IPurchaseRepository _purchaseRepository;

            public CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
            {
                _purchaseRepository = purchaseRepository;
            }

            public async Task<Result<Purchase>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
            {
                var purchaseCreated = await _purchaseRepository.Create(request.Purchase);
                return purchaseCreated == null
                    ? Result.Failure<Purchase>(PurchaseContextExceptionEnum.ErrorCreatingPurchase.GetErrorMessage())
                    : Result.Success<Purchase>(purchaseCreated);
            }
        }
    }
}
