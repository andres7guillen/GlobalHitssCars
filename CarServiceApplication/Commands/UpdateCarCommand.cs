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
    public class UpdateCarCommand : IRequest<Result<Car>>
    {
        public Car Car { get; set; }
    }

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Result<Car>>
    {
        private readonly ICarRepository _carRepository;

        public UpdateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Result<Car>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.Update(request.Car);
            if (result)
                return Result.Success(request.Car);
            return Result.Failure<Car>(CarContextExceptionEnum.ErrorUpdatingCar.GetErrorMessage());
        }
    }
}
