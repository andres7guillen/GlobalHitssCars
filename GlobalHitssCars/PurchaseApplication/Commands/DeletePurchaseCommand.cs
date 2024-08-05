using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseApplication.Commands
{
    public class DeletePurchaseCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, Result<bool>>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Result<bool>> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _purchaseRepository.Delete(request.Id);
            return isDeleted
                ? Result.Success(isDeleted)
                : Result.Failure<bool>(PurchaseContextExceptionEnum.ErrorDeleteingPurchase.GetErrorMessage());
        }
    }
}
