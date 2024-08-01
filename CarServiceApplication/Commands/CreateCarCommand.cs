using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceApplication.Commands
{
    public class CreateCarCommand : IRequest<Result<Car>>
    {
        public Car Car { get; set; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand,Result<Car>>
    {
        private readonly ICarRepository _carRepository;
        public CreateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<Result<Car>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.Create(request.Car);
            if (car == null)
                return Result.Failure<Car>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage());
            return Result.Success<Car>(car);
        }
    }

}
