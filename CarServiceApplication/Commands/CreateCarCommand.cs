using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApplication.Commands
{
    public class CreateCarCommand : IRequest<Result<Car>>
    {
        public Car Car { get; set; }

        public CreateCarCommand(Car car)
        {
            Car = car;
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

                var saveResult = await _carRepository.Create(request.Car);
                if (saveResult == null)
                {
                    _logger.Error(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage(), new Exception());
                    return Result.Failure<Car>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage());
                }            
                return Result.Success<Car>(saveResult);
            }
        }
    }
}
