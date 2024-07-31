using CarServiceDomain.Entities;
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
    public class CreateCarCommand : IRequest<Car>
    {
        public Car Car { get; set; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;
        public CreateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task <Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            return await _carRepository.Create(request.Car);
        }
    }

}
