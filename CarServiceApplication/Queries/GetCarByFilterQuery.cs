using CarServiceDomain.DTOs;
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
    public class GetCarByFilterQuery : IRequest<Result<Car>>
    {
        public CarByFilterDTO Filter { get; set; }

        public GetCarByFilterQuery(CarByFilterDTO filter)
        {
            Filter = filter;
        }
    }

    public class GetCarByFilterQueryHandler : IRequestHandler<GetCarByFilterQuery, Result<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByFilterQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Result<Car>> Handle(GetCarByFilterQuery request, CancellationToken cancellationToken)
        {
            var carMaybe = await _carRepository.GetCarByFilter(request.Filter);
            return carMaybe.HasNoValue
                ? Result.Failure<Car>(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage())
                : Result.Success(carMaybe.Value);
        }
    }

}
