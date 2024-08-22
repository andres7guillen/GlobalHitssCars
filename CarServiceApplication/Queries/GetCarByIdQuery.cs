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
    public class GetCarByIdQuery : IRequest<Result<CarStock>>
    {
        public Guid Id { get; set; }
        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Result<CarStock>>
        {
            private readonly ICarStockRepository _carRepository;
            public GetCarByIdQueryHandler(ICarStockRepository carRepository)
            {
                _carRepository = carRepository;
            }
            public async Task<Result<CarStock>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
            {
                var maybeCar = await _carRepository.GetById(request.Id);
                if (maybeCar.HasNoValue)
                    return Result.Failure<CarStock>(CarContextExceptionEnum.CarNotFound.GetErrorMessage());
                return Result.Success(maybeCar.Value);
            }
        }
    }
}
