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
        public Car Car { get; set; }

        public UpdateCarCommand(Car car)
        {
            Car = car;
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
                var result = await _carRepository.Update(request.Car);
                if (result)
                    return Result.Success(result);
                return Result.Failure<bool>(CarContextExceptionEnum.ErrorUpdatingCar.GetErrorMessage());
            }
        }

    }

    
}
