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
        public string Brand { get; set; }
        public int Model { get; set; }
        public string Reference { get; set; }
        public string Colour { get; set; }
        public string LicensePlate { get; set; }

        public CreateCarCommand(string brand, int model, string reference, string colour, string licensePlate)
        {
            Brand = brand;
            Model = model;
            Reference = reference;
            Colour = colour;
            LicensePlate = licensePlate;
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
                var carToCreate = Car.Build(request.Brand, request.Model, request.Reference, request.Colour, request.LicensePlate);

                var saveResult = await _carRepository.Create(carToCreate.Value);
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
