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
    public class GetCarByFilterQuery : IRequest<Result<IEnumerable<CarStock>>>
    {
        public CarStockByFilterDTO Filter { get; set; }

        public GetCarByFilterQuery(CarStockByFilterDTO filter)
        {
            Filter = filter;
        }

        public class GetCarByFilterQueryHandler : IRequestHandler<GetCarByFilterQuery, Result<IEnumerable<CarStock>>>
        {
            private readonly ICarStockRepository _carRepository;

            public GetCarByFilterQueryHandler(ICarStockRepository carRepository)
            {
                _carRepository = carRepository;
            }

            public async Task<Result<IEnumerable<CarStock>>> Handle(GetCarByFilterQuery request, CancellationToken cancellationToken)
            {
                var cars = await _carRepository.GetCarByFilter(request.Filter);
                return cars.Count() > 0
                    ? Result.Success(cars)
                    : Result.Failure<IEnumerable<CarStock>>(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage());
            }
        }

    }
}
