using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Exceptions
{
    public static class PurchaseContextExceptions
    {
        public static readonly Dictionary<PurchaseContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { PurchaseContextExceptionEnum.PurchaseNotFound, new Tuple<int, string>((int)PurchaseContextExceptionEnum.PurchaseNotFound, "Purchase not found")},
        { PurchaseContextExceptionEnum.PurchaseNotFoundByFilter, new Tuple<int, string>((int)PurchaseContextExceptionEnum.PurchaseNotFoundByFilter, "Purchase not found by filter") },
        { PurchaseContextExceptionEnum.ErrorUpdatingPurchase, new Tuple<int, string> ((int)PurchaseContextExceptionEnum.ErrorUpdatingPurchase, "Error updating Purchase") },
        { PurchaseContextExceptionEnum.ErrorCreatingPurchase, new Tuple<int, string>((int)PurchaseContextExceptionEnum.ErrorCreatingPurchase, "Error creating Purchase") },
        { PurchaseContextExceptionEnum.ErrorDeletingPurchase, new Tuple<int, string>((int)PurchaseContextExceptionEnum.ErrorDeletingPurchase, "Error deleting Purchase") },
        { PurchaseContextExceptionEnum.NoPurchasesFound, new Tuple<int, string>((int)PurchaseContextExceptionEnum.NoPurchasesFound, "No Purchases found") },
        { PurchaseContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)PurchaseContextExceptionEnum.UndefinedError, "Undefined error") }

    };
    }

    public enum PurchaseContextExceptionEnum
    {
        //4000
        PurchaseNotFound = 4000,
        PurchaseNotFoundByFilter = 4001,
        ErrorUpdatingPurchase = 4002,
        ErrorCreatingPurchase = 4003,
        ErrorDeletingPurchase = 4004,
        NoPurchasesFound = 4005,
        UndefinedError = 4006
    }

    public static class PurchaseContextExceptionEnumExtensions
    {
        public static string GetErrorMessage(this PurchaseContextExceptionEnum error)
        {
            if (PurchaseContextExceptions.ErrorMessages.TryGetValue(error, out var message))
            {
                return $"{message.Item1}: {message.Item2}";
            }
            return $"{(int)error}: Unknown error";
        }
    }
}
