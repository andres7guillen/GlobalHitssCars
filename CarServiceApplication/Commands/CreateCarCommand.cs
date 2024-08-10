using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CarServiceApplication.Commands
{
    public class CreateCarCommand : IRequest<Result<Car>>
    {
        public Car Car { get; set; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Result<Car>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger _logger;
        public CreateCarCommandHandler(ICarRepository carRepository, ILogger logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }
        public async Task<Result<Car>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            _logger.Info("Create car command started");
            var car = await _carRepository.Create(request.Car);
            if (car == null)
            {
                _logger.Error($"Create car command finished with: {CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage()}",new Exception() {  });
                return Result.Failure<Car>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage());
            }
            _logger.Info($"Create car command finished.");
            return Result.Success<Car>(car);
        }
    }

}
