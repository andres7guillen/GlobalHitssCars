using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceApplication.MessageBus.Interfaces;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Enum;
using PurchaseServiceDomain.Events;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;
using PurchaseServiceDomain.SharedKernel;

namespace PurchaseApplication.Commands
{
    public class CreatePurchaseCommand : IRequest<Result<Purchase>>
    {

        public Guid ClientId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? SpareId { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public TypePurchaseEnum TypepUrchase { get; set; }

        public CreatePurchaseCommand(Guid clientId, Guid carId, double amount, TypePurchaseEnum typepUrchase, Guid? spareId, int quantity)
        {
            ClientId = clientId;
            CarId = carId;
            Amount = amount;
            TypepUrchase = typepUrchase;
            SpareId = spareId;
            Quantity = quantity;
        }



        public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, Result<Purchase>>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            //private readonly IMessageProducer _messageProducer;

            public CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
            {
                _purchaseRepository = purchaseRepository;
                //_messageProducer = messageProducer;
            }

            public async Task<Result<Purchase>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
            {
                var purchase = Purchase.Build(request.ClientId, request.CarId,request.SpareId, request.Quantity, request.Amount, request.TypepUrchase);
                if (request.TypepUrchase == TypePurchaseEnum.Car)
                {
                    purchase.Value.MakePurchaseCar();
                    var domainEvent = purchase.Value.DomainEvents.OfType<Event<LessStockCarEvent>>().FirstOrDefault();
                    if (domainEvent != null)
                    {
                        //_messageProducer.SendingMessage(domainEvent, "LessStockCarEvent");
                    }
                }
                else if (request.TypepUrchase == TypePurchaseEnum.Spare) 
                {
                    purchase.Value.MakePurchaseCar();
                    var domainEvent = purchase.Value.DomainEvents.OfType<Event<LessStockSparePartEvent>>().FirstOrDefault();
                    if (domainEvent != null)
                    {
                        //_messageProducer.SendingMessage(domainEvent, "LessStockSparePartEvent");
                    }
                }
                
                var purchaseCreated = await _purchaseRepository.Create(purchase.Value);
                
                return purchaseCreated == null
                    ? Result.Failure<Purchase>(PurchaseContextExceptionEnum.ErrorCreatingPurchase.GetErrorMessage())
                    : Result.Success<Purchase>(purchaseCreated);
            }
        }
    }
}
