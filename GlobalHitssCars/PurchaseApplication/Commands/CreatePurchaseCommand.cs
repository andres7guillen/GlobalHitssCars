using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;

namespace PurchaseApplication.Commands
{
    public class CreatePurchaseCommand : IRequest<Result<Purchase>>
    {

        public Guid ClientId { get; set; }
        public Guid CarId { get; set; }
        public double Amount { get; set; }

        public CreatePurchaseCommand(Guid clientId, Guid carId, double amount)
        {
            ClientId = clientId;
            CarId = carId;
            Amount = amount;
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
                var purchase = Purchase.Build(request.ClientId, request.CarId, request.Amount);
                var purchaseCreated = await _purchaseRepository.Create(purchase.Value);
                return purchaseCreated == null
                    ? Result.Failure<Purchase>(PurchaseContextExceptionEnum.ErrorCreatingPurchase.GetErrorMessage())
                    : Result.Success<Purchase>(purchaseCreated);
            }
        }
    }
}
