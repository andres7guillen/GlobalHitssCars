using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceApplication.Commands
{
    public class UpdateCarCommand : IRequest<Result<bool>>
    {
        public  Guid Id { get; set; }
        public string? Brand { get; set; }
        public int? Model { get; set; }
        public string? Reference { get; set; }
        public string? Colour { get; set; }
        public string? LicensePlate { get; set; }

        public UpdateCarCommand(Guid id, string? brand, int? model, string? reference, string? colour, string? licensePlate)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Reference = reference;
            Colour = colour;
            LicensePlate = licensePlate;
        }

        

        public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Result<bool>>
        {
            private readonly ICarRepository _carRepository;

            public UpdateCarCommandHandler(ICarRepository carRepository)
            {
                _carRepository = carRepository;
            }

            public async Task<Result<bool>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetById(request.Id);
                if (carToUpdate.HasValue) 
                {
                    carToUpdate.Value.LicensePlate = request.LicensePlate;
                    carToUpdate.Value.Model = request.Model.Value;
                    carToUpdate.Value.Reference = request.Reference;
                    carToUpdate.Value.Brand = request.Brand;
                    carToUpdate.Value.UpdateCar();
                    var result = await _carRepository.Update(carToUpdate.Value);
                    if (result)
                        return Result.Success(result);
                }
                    
                return Result.Failure<bool>(CarContextExceptionEnum.ErrorUpdatingCar.GetErrorMessage());
            }
        }

    }

    
}
