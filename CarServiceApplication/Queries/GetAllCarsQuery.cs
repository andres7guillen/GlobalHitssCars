using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CarServiceApplication.Queries
{
    public class GetAllCarsQuery : IRequest<Result<Tuple<int, IEnumerable<Car>>>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public GetAllCarsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, Result<Tuple<int, IEnumerable<Car>>>>
        {
            private readonly ICarRepository _carRepository;
            private readonly ILogger _logger;

            public GetAllCarsQueryHandler(ICarRepository carRepository, ILogger logger)
            {
                _carRepository = carRepository;
                _logger = logger;
            }

            public async Task<Result<Tuple<int,IEnumerable<Car>>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
            {
                _logger.Error("Obtener todos los carros, error", new Exception());
                var carList = await _carRepository.GetAll(offset: request.Offset, limit: request.Limit);
                return carList.Item1 > 0
                    ? Result.Success(carList)
                    : Result.Failure<Tuple<int, IEnumerable<Car>>>(CarContextExceptionEnum.NoCarsFound.GetErrorMessage());
            }
        }


    }
}
