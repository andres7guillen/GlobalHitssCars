﻿
namespace CarServiceDomain.Exceptions;
public static class CarStockContextExceptions
{

    public static readonly Dictionary<CarContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { CarContextExceptionEnum.CarNotFound, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFound, "CarStock not found.")},
        { CarContextExceptionEnum.CarNotFoundByFilter, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFoundByFilter, "CarStock not found by filter.") },
        { CarContextExceptionEnum.ErrorUpdatingCar, new Tuple<int, string> ((int)CarContextExceptionEnum.ErrorUpdatingCar, "Error updating carStock.") },
        { CarContextExceptionEnum.ErrorCreatingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorCreatingCar, "Error creating carStock.") },
        { CarContextExceptionEnum.ErrorDeletingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorDeletingCar, "Error deleting carStock.") },
        { CarContextExceptionEnum.NoCarsFound, new Tuple<int, string>((int)CarContextExceptionEnum.NoCarsFound, "No cars stock found.") },
        { CarContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)CarContextExceptionEnum.UndefinedError, "Undefined error.") },        
        { CarContextExceptionEnum.InvalidModel, new Tuple<int, string>((int)CarContextExceptionEnum.InvalidModel, "Year of the model invalid, it cannot be greater than current year.") }

    };
}


public enum CarContextExceptionEnum
{
    //4000
    CarNotFound = 4000,
    CarNotFoundByFilter = 4001,
    ErrorUpdatingCar = 4002,
    ErrorCreatingCar = 4003,
    ErrorDeletingCar = 4004,
    NoCarsFound = 4005,
    InvalidModel = 4006,
    UndefinedError = 0
}

public static class CarStockContextExceptionEnumExtensions
{
    public static string GetErrorMessage(this CarContextExceptionEnum error)
    {
        if (CarStockContextExceptions.ErrorMessages.TryGetValue(error, out var message))
        {
            return $"{message.Item1}: {message.Item2}";
        }
        return $"{(int)error}: Unknown error";
    }
}

