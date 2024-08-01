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
    public class GetAllCarsQuery : IRequest<Result<IEnumerable<Car>>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; } 
        public GetAllCarsQuery(int offset, int limit)
        {     
            Offset = offset;
            Limit = limit;
        }

        public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, Result<IEnumerable<Car>>>
        {
            private readonly ICarRepository _carRepository;

            public GetAllCarsQueryHandler(ICarRepository carRepository)
            {
                _carRepository = carRepository;
            }

            public async Task<Result<IEnumerable<Car>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
            {
                var carList = await _carRepository.GetAll(offset: request.Offset, limit: request.Limit);
                return carList.Count() > 0
                    ? Result.Success(carList)
                    : Result.Failure<IEnumerable<Car>>(CarContextExceptionEnum.NoCarsFound.GetErrorMessage());
            }
        }


    }
}
