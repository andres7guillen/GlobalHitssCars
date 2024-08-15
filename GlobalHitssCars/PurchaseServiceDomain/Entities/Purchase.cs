using CSharpFunctionalExtensions;
using PurchaseServiceDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid CarId { get; set; }
        public double Amount { get; set; }

        public Purchase(Guid id, Guid clientId, Guid carId, double amount)
        {
            Id = id;
            ClientId = clientId;
            CarId = carId;
            Amount = amount;
        }

        public static Result<Purchase> Build(Guid withClientId, Guid withCarId, double withAmount)
        {
            if (withAmount < 0)
                return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());
            return new Purchase(Guid.NewGuid(), withClientId, withCarId, withAmount);
        }

        public static Result<Purchase> Load(Guid withClientId, Guid withCarId, double withAmount)
        {
            if (withAmount < 0)
                return Result.Failure<Purchase>(PurchaseContextExceptionEnum.AmountLessThanZero.GetErrorMessage());
            return new Purchase(withCarId, withClientId, withCarId, withAmount);
        }


    }
}
