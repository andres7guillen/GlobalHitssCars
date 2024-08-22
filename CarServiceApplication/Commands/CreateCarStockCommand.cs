using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApplication.Commands
{
    public class CreateCarStockCommand : IRequest<Result<CarStock>>
    {
        public Guid BrandId { get; set; }
        public int Model { get; set; }
        public Guid ReferenceId { get; set; }
        public string Colour { get; set; }

        public CreateCarStockCommand(Guid brandId, int model, Guid referenceId, string colour)
        {
            BrandId = brandId;
            Model = model;
            ReferenceId = referenceId;
            Colour = colour;
        }        
        

        public class CreateCarCommandHandler : IRequestHandler<CreateCarStockCommand, Result<CarStock>>
        {
            private readonly ICarStockRepository _carRepository;
            private readonly ILogger _logger;
            public CreateCarCommandHandler(ICarStockRepository carStockRepository, ILogger logger)
            {
                _carRepository = carStockRepository;
                _logger = logger;
            }
            public async Task<Result<CarStock>> Handle(CreateCarStockCommand request, CancellationToken cancellationToken)
            {
                _logger.Info("Create car command started");
                var carToCreate = CarStock.Build(request.BrandId, request.Model, request.ReferenceId, request.Colour);

                var saveResult = await _carRepository.Create(carToCreate.Value);
                if (saveResult == null)
                {
                    _logger.Error(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage(), new Exception());
                    return Result.Failure<CarStock>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage());
                }            
                return Result.Success<CarStock>(saveResult);
            }
        }
    }
}
