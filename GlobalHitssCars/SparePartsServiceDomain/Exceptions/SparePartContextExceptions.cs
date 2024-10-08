﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Exceptions
{
    public static class SparePartContextExceptions
    {
        public static readonly Dictionary<SparePartContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { SparePartContextExceptionEnum.SparePartNotFound, new Tuple<int, string>((int)SparePartContextExceptionEnum.SparePartNotFound, "SparePart not found")},
        { SparePartContextExceptionEnum.SparePartNotFoundByFilter, new Tuple<int, string>((int)SparePartContextExceptionEnum.SparePartNotFoundByFilter, "SparePart not found by filter") },
        { SparePartContextExceptionEnum.ErrorUpdatingSparePart, new Tuple<int, string> ((int)SparePartContextExceptionEnum.ErrorUpdatingSparePart, "Error updating SparePart") },
        { SparePartContextExceptionEnum.ErrorCreatingSparePart, new Tuple<int, string>((int)SparePartContextExceptionEnum.ErrorCreatingSparePart, "Error creating SparePart") },
        { SparePartContextExceptionEnum.ErrorDeletingSparePart, new Tuple<int, string>((int)SparePartContextExceptionEnum.ErrorDeletingSparePart, "Error deleting SparePart") },
        { SparePartContextExceptionEnum.NoSparePartsFound, new Tuple<int, string>((int)SparePartContextExceptionEnum.NoSparePartsFound, "No SpareParts found") },
        { SparePartContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)SparePartContextExceptionEnum.UndefinedError, "Undefined error")},
        { SparePartContextExceptionEnum.IsNotEnoughStockToDelete, new Tuple<int, string>((int)SparePartContextExceptionEnum.IsNotEnoughStockToDelete,"The quantity items to delete is more than existing items in stock")},
        { SparePartContextExceptionEnum.QuantityCannotBeLessThanZero, new Tuple<int, string>((int)SparePartContextExceptionEnum.QuantityCannotBeLessThanZero, "Quantity cannot be less or equal than zero." ) },
        { SparePartContextExceptionEnum.ErrorTryingToAddStock, new Tuple<int, string>((int)SparePartContextExceptionEnum.ErrorTryingToAddStock, "Error trying to add stock") }
    };
    }

    public enum SparePartContextExceptionEnum
    {
        //4000
        SparePartNotFound = 4000,
        SparePartNotFoundByFilter = 4001,
        ErrorUpdatingSparePart = 4002,
        ErrorCreatingSparePart = 4003,
        ErrorDeletingSparePart = 4004,
        NoSparePartsFound = 4005,
        IsNotEnoughStockToDelete = 4006,
        ErrorTryingToLessStock = 4007,
        ErrorTryingToAddStock = 4008,
        QuantityCannotBeLessThanZero = 4009,
        UndefinedError = 4010
    }

    public static class SparePartContextExceptionEnumExtensions
    {
        public static string GetErrorMessage(this SparePartContextExceptionEnum error)
        {
            if (SparePartContextExceptions.ErrorMessages.TryGetValue(error, out var message))
            {
                return $"{message.Item1}: {message.Item2}";
            }
            return $"{(int)error}: Unknown error";
        }
    }
}
