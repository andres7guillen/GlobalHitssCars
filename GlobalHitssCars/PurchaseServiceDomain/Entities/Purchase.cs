using CSharpFunctionalExtensions;
using PurchaseServiceDomain.Enum;
using PurchaseServiceDomain.Events;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Entities
{
    //public record Purchase : EntityBase
    //{
    //    public Guid Id { get; set; }
    //    public Guid ClientId { get; set; }
    //    public Guid? CarId { get; set; }
    //    public Guid? SparePartId { get; set; } = null;
    //    public double Amount { get; set; }
    //    public int Quantity { get; set; }
    //    public TypePurchaseEnum TypePurchase { get; set; } = TypePurchaseEnum.Car;

    //    public Purchase(Guid id, Guid clientId, Guid? carId, Guid? spareId, int quantity, double amount, TypePurchaseEnum typePurchase)
    //    {
    //        Id = id;
    //        ClientId = clientId;
    //        CarId = carId;
    //        Amount = amount;
    //        TypePurchase = typePurchase;
    //        SparePartId = spareId;
    //        Quantity = quantity;
    //    }

    //    public static Result<Purchase> Build(Guid withClientId, Guid? withCarId,Guid? withSpareId,int withQuantity, double withAmount, 
    //        TypePurchaseEnum withType)
    //    {
    //        if (withAmount < 0)
    //            return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());

    //        return new Purchase(Guid.NewGuid(), withClientId, withCarId, withSpareId,withQuantity, withAmount, withType);
    //    }

    //    public static Result<Purchase> Load(Guid withClientId, Guid withCarId, Guid withSpareId, int withQuantity, double withAmount, TypePurchaseEnum withType)
    //    {
    //        if (withAmount < 0)
    //            return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());
    //        return new Purchase(withCarId, withClientId, withCarId, withSpareId, withQuantity, withAmount, withType);
    //    }

    //    public void UpdatePurchase(double? amount = null)
    //    {
    //        if (amount.HasValue)
    //            Amount = amount.Value;
    //    }

    //    public void MakePurchaseCar() 
    //    {
    //            LessStockCarEvent();
    //    }

    //    public void MakePurchaseSpare()
    //    {
    //        LessStockSparePartEvent();
    //    }


    //    #region EVENTS
    //    private void LessStockSparePartEvent()
    //    {
    //        RaiseDomainEvent(new Event<LessStockSparePartEvent>
    //        {
    //            Body = new LessStockSparePartEvent
    //            {
    //                SparePartId = SparePartId.Value,
    //                Quantity = Quantity
    //            },
    //            Header = GetEventHeader(nameof(LessStockSparePartEvent))
    //        });
    //    }
    //    private void LessStockCarEvent()
    //    {
    //        RaiseDomainEvent(new Event<LessStockCarEvent>
    //        {
    //            Body = new LessStockCarEvent
    //            {
    //                CarId = Id,
    //                Quantity = Quantity
    //            },
    //            Header = GetEventHeader(nameof(LessStockCarEvent))
    //        });
    //    }
    //    private Header GetEventHeader(string eventName)
    //    {
    //        return new Header
    //        {
    //            AggregateId = Id,
    //            AggregateName = "Purchase Spare Part",
    //            EventDate = DateTime.UtcNow,
    //            EventType = eventName,
    //            MessageId = Guid.NewGuid().ToString(),
    //            TenantId = "Colombia",
    //            UserId = "system"
    //        };
    //    } 
    //    #endregion

    //}

    public record Purchase : EntityBase
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? SparePartId { get; set; } = null;
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public TypePurchaseEnum TypePurchase { get; set; } = TypePurchaseEnum.Car;

        private Purchase(Guid id, Guid clientId, Guid? carId, Guid? spareId, int quantity, double amount, TypePurchaseEnum typePurchase)
        {
            Id = id;
            ClientId = clientId;
            CarId = carId;
            Amount = amount;
            TypePurchase = typePurchase;
            SparePartId = spareId;
            Quantity = quantity;
        }
        
        // Constructor público para EF Core
        public Purchase() { }

        public static Result<Purchase> Build(Guid withClientId, Guid? withCarId, Guid? withSpareId, int withQuantity, double withAmount,
            TypePurchaseEnum withType)
        {
            if (withAmount < 0)
                return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());

            return new Purchase(Guid.NewGuid(), withClientId, withCarId, withSpareId, withQuantity, withAmount, withType);
        }

        public static Result<Purchase> Load(Guid withClientId, Guid withCarId, Guid withSpareId, int withQuantity, double withAmount, TypePurchaseEnum withType)
        {
            if (withAmount < 0)
                return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());
            return new Purchase(withCarId, withClientId, withCarId, withSpareId, withQuantity, withAmount, withType);
        }

        public void UpdatePurchase(double? amount = null)
        {
            if (amount.HasValue)
                Amount = amount.Value;
        }

        public void MakePurchaseCar()
        {
            LessStockCarEvent();
        }

        public void MakePurchaseSpare()
        {
            LessStockSparePartEvent();
        }


        #region EVENTS
        private void LessStockSparePartEvent()
        {
            RaiseDomainEvent(new Event<LessStockSparePartEvent>
            {
                Body = new LessStockSparePartEvent
                {
                    SparePartId = SparePartId.Value,
                    Quantity = Quantity
                },
                Header = GetEventHeader(nameof(LessStockSparePartEvent))
            });
        }
        private void LessStockCarEvent()
        {
            RaiseDomainEvent(new Event<LessStockCarEvent>
            {
                Body = new LessStockCarEvent
                {
                    CarId = Id,
                    Quantity = Quantity
                },
                Header = GetEventHeader(nameof(LessStockCarEvent))
            });
        }
        private Header GetEventHeader(string eventName)
        {
            return new Header
            {
                AggregateId = Id,
                AggregateName = "Purchase Spare Part",
                EventDate = DateTime.UtcNow,
                EventType = eventName,
                MessageId = Guid.NewGuid().ToString(),
                TenantId = "Colombia",
                UserId = "system"
            };
        }
        #endregion

    }

}
