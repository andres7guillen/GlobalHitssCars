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
    public class UpdateCarStockCommand : IRequest<Result<bool>>
    {
        public  Guid Id { get; set; }
        public string Colour { get; set; }

        public UpdateCarStockCommand(Guid id, string? colour)
        {
            Id = id;
            Colour = colour;
        }

        

        public class UpdateCarCommandHandler : IRequestHandler<UpdateCarStockCommand, Result<bool>>
        {
            private readonly ICarStockRepository _carRepository;

            public UpdateCarCommandHandler(ICarStockRepository carRepository)
            {
                _carRepository = carRepository;
            }

            public async Task<Result<bool>> Handle(UpdateCarStockCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetById(request.Id);
                if (carToUpdate.HasValue) 
                {
                    carToUpdate.Value.Colour = request.Colour;
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
