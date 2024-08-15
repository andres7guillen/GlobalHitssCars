
namespace CarServiceDomain.Exceptions;
public static class CarContextExceptions
{

    public static readonly Dictionary<CarContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { CarContextExceptionEnum.CarNotFound, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFound, "Car not found.")},
        { CarContextExceptionEnum.CarNotFoundByFilter, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFoundByFilter, "Car not found by filter.") },
        { CarContextExceptionEnum.ErrorUpdatingCar, new Tuple<int, string> ((int)CarContextExceptionEnum.ErrorUpdatingCar, "Error updating car.") },
        { CarContextExceptionEnum.ErrorCreatingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorCreatingCar, "Error creating car.") },
        { CarContextExceptionEnum.ErrorDeletingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorDeletingCar, "Error deleting car.") },
        { CarContextExceptionEnum.NoCarsFound, new Tuple<int, string>((int)CarContextExceptionEnum.NoCarsFound, "No cars found.") },
        { CarContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)CarContextExceptionEnum.UndefinedError, "Undefined error.") },
        { CarContextExceptionEnum.LicensePlateError, new Tuple<int, string>((int) CarContextExceptionEnum.LicensePlateError, "License plate cannot be longer than 6 characters.") },
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
    LicensePlateError = 4006,
    InvalidModel = 4007,
    UndefinedError = 0
}

public static class CarContextExceptionEnumExtensions
{
    public static string GetErrorMessage(this CarContextExceptionEnum error)
    {
        if (CarContextExceptions.ErrorMessages.TryGetValue(error, out var message))
        {
            return $"{message.Item1}: {message.Item2}";
        }
        return $"{(int)error}: Unknown error";
    }
}

