
namespace CarServiceDomain.Exceptions;
public static class CarContextExceptions
{

    public static readonly Dictionary<CarContextExceptionEnum, Tuple<int, string>> ErrorMessages = new()
    {
        { CarContextExceptionEnum.CarNotFound, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFound, "Car not found")},
        { CarContextExceptionEnum.CarNotFoundByFilter, new Tuple<int, string>((int)CarContextExceptionEnum.CarNotFoundByFilter, "Car not found by filter") },
        { CarContextExceptionEnum.ErrorUpdatingCar, new Tuple<int, string> ((int)CarContextExceptionEnum.ErrorUpdatingCar, "Error updating car") },
        { CarContextExceptionEnum.ErrorCreatingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorCreatingCar, "Error creating car") },
        { CarContextExceptionEnum.ErrorDeletingCar, new Tuple<int, string>((int)CarContextExceptionEnum.ErrorDeletingCar, "Error deleting car") },
        { CarContextExceptionEnum.NoCarsFound, new Tuple<int, string>((int)CarContextExceptionEnum.NoCarsFound, "No cars found") },
        { CarContextExceptionEnum.UndefinedError, new Tuple<int, string>((int)CarContextExceptionEnum.UndefinedError, "Undefined error") }

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
    UndefinedError = 4006
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

