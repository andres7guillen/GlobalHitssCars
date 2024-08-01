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

namespace CarServiceApplication.Queries
{
    public class GetCarByIdQuery : IRequest<Result<Car>>
    {
        public Guid Id { get; set; }
        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Result<Car>>
        {
            private readonly ICarRepository _carRepository;
            public GetCarByIdQueryHandler(ICarRepository carRepository)
            {
                _carRepository = carRepository;
            }
            public async Task<Result<Car>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
            {
                var maybeCar = await _carRepository.GetById(request.Id);
                if (maybeCar.HasNoValue)
                    return Result.Failure<Car>(CarContextExceptionEnum.CarNotFound.GetErrorMessage());
                return Result.Success(maybeCar.Value);
            }
        }

    }
}
