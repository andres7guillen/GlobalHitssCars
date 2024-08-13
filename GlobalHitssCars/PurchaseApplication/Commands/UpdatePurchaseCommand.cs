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
    public class UpdatePurchaseCommand : IRequest<Result<bool>>
    {
        public Purchase Purchase { get; set; }

        public UpdatePurchaseCommand(Purchase purchase)
        {
            Purchase = purchase;
        }

        public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, Result<bool>>
        {
            private readonly IPurchaseRepository _purchaseRepository;

            public UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
            {
                _purchaseRepository = purchaseRepository;
            }

            public async Task<Result<bool>> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
            {
                var isUpdated = await _purchaseRepository.Update(request.Purchase);
                return isUpdated
                    ? Result.Success(isUpdated)
                    : Result.Failure<bool>(PurchaseContextExceptionEnum.ErrorUpdatingPurchase.GetErrorMessage());
            }
        }

    }

    
}
