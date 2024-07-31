using CarServiceDomain.SharedKernel;

namespace CarServiceDomain.Exceptions;
public class CarContextException : BusinessException
{
    public CarContextException(CarContextExceptionEnum carContextExceptionEnum)
        : base(Detail(carContextExceptionEnum).Item2)
    {
        Code = Detail(carContextExceptionEnum).Item1;
    }

    private static Tuple<int, string> Detail(CarContextExceptionEnum carContextExceptionEnum)
    {
        var code = (int)carContextExceptionEnum;
        var detail = carContextExceptionEnum switch
        {
            CarContextExceptionEnum.CarNotFound => new Tuple<int, string>(code, "Car not found"),
            CarContextExceptionEnum.CarNotFoundByFilter => new Tuple<int, string>(code, "Car not found by filter"),
            CarContextExceptionEnum.ErrorUpdatingCar => new Tuple<int, string> (code, "Error updating car"),
            CarContextExceptionEnum.ErrorCreatingCar => new Tuple<int, string>(code, "Error creating car"),
            CarContextExceptionEnum.ErrorDeleteingCar => new Tuple<int, string>(code, "Error deleteing car"),
            _ => new Tuple<int, string>(code, "Undefined error")
        };
        return detail;
    }


}
public enum CarContextExceptionEnum
{
    //4000
    CarNotFound = 4000,
    CarNotFoundByFilter = 4001,
    ErrorUpdatingCar = 4002,
    ErrorCreatingCar = 4003,
    ErrorDeleteingCar = 4004,
}

public static class CarContextExceptionEnumExtensions
{ 
    public static string GetErrorMessage(this CarContextExceptionEnum error)
    {
        return $"{(int)error}: {error}";
    }
}

